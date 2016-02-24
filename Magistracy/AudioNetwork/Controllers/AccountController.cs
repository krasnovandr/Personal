using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AudioNetwork.Helpers;
using AudioNetwork.Services;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AudioNetwork.Models;


namespace AudioNetwork.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.EmailConfirmed == false)
                    {
                        return Json(new LoginResult { EmailNotConfirmed = true, Message = "Регистрация по почте не подтверждена" });
                    }
                    await SignInAsync(user, model.RememberMe);
                    return Json(new LoginResult { Success = true });
                }
                else
                {
                    return Json(new LoginResult { Success = false, Message = "Неверный логин или пароль" });
                }
            }
            return Json(new LoginResult { Success = false, Message = ModelState.Values.ToString() });
        }

        [HttpPost]
        public ActionResult CheckLogin()
        {
            if (User.Identity.IsAuthenticated && string.IsNullOrEmpty(User.Identity.Name) == false)
            {
                var userView = ModelConverters.ToUserViewModel(_userRepository.GetUser(User.Identity.GetUserId()));
                userView.LoggedIn = true;

                return Json(userView);
            }

            return Json(new { LogedIn = false });
        }
        [HttpPost]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    LastActivity = DateTime.Now,
                    Email = model.Email,
                    AvatarFilePath = FilePathContainer.ImagePathRelative + "DefaultAvatar.png",
                    EmailConfirmed = false,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                try
                {
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        User.Identity.GetUserId();
                        if (Request.Url != null) MailSender.SendEmailMessage(user,Request.Url.Authority);

                        return Json(new LoginResult { Success = true, EmailSended = true });
                    }
                    return Json(new LoginResult { Success = false, Message = "Ошибка при регистрации" });
                }
                catch (Exception exp)
                {
                    return Json(new LoginResult { Success = false, Message = exp.Message });
                }
            }
            return Json(new LoginResult { Success = false, Message = ModelState.Values.ToString() });
        }

        [AllowAnonymous]
        public async Task<JsonResult> RepeatMail(string userName)
        {
            ApplicationUser user = this.UserManager.FindByName(userName);
            if (user != null && Request.Url != null)
            {
                MailSender.SendEmailMessage(user, Request.Url.Authority);
            }
            return Json(new { Success = false, Message = "Такой пользователь не найден" });
        }

    
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmEmail(string token, string email)
        {
            ApplicationUser user = this.UserManager.FindById(token);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    await SignInAsync(user, isPersistent: false);
                    return Json(new { Success = true });
                }
                return Json(new { Success = false, Message = "Почтовые адреса не совпадают" });
            }
            return Json(new { Success = false, Message = "Такой пользователь не найден" });
        }


        public JsonResult GetLoginProviders()
        {
            var providers = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
            return Json(providers, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return Json(true);
        }


        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }



        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}