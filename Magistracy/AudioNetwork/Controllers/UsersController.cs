using System.Web.Mvc;
using AudioNetwork.Models;
using AudioNetwork.Services;
using Microsoft.AspNet.Identity;

namespace AudioNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult ViewUsers()
        {
            return View("Users");
        }

        [HttpGet]
        public ActionResult ViewUsersModal()
        {
            return View("UserModal");
        }


        [HttpGet]
        public ActionResult ViewUserPictureModal()
        {
            return View("UserPictureModal");
        }

        [HttpGet]
        public ActionResult ViewUser()
        {
            return View("User");
        }


        [HttpGet]
        public ActionResult ViewFriends()
        {
            return View("Friends");
        }

        public ActionResult Index()
        {
            return View("HomePage");
        }


        [HttpGet]
        public JsonResult GetUsers()
        {
            var userId = User.Identity.GetUserId();
            return Json(_userService.GetUsers(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(string id)
        {
            return Json(_userService.GetUser(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyInfo()
        {
            var userId = User.Identity.GetUserId();
            return Json(_userService.GetUser(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(UserViewModel userInfo)
        {
            _userService.UpdateUser(userInfo);
            return Json(new { Succes = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchUsers(UserSearchModel searchModel)
        {
            return Json(_userService.SearchUsers(searchModel, User.Identity.GetUserId()), JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateUserVkInfo(VkUserModel userInfo)
        {
            var userId = User.Identity.GetUserId();
            _userService.UpdateUserVkInfo(userId, userInfo.Login, userInfo.Password);

            return Json(new { Succes = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserCurrentSong(string songId)
        {
            var userId = User.Identity.GetUserId();
            _userService.UpdateUserCurrentSong(userId, songId);

            return Json(new { Succes = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddFriend(UserViewModel friend)
        {
            var userId = User.Identity.GetUserId();
            _userService.AddFriend(userId, friend.Id);

            return Json(null);
        }

        public ActionResult ConfirmFriend(UserViewModel friend)
        {
            var userId = User.Identity.GetUserId();
            _userService.ConfirmFriend(userId, friend.Id);

            return Json(null);
        }
        
        public ActionResult RemoveFriend(UserViewModel friend)
        {
            var userId = User.Identity.GetUserId();
            _userService.RemoveFriend(userId, friend.Id);

            return Json(null);
        }

        public ActionResult GetFriends()
        {
            var userId = User.Identity.GetUserId();
            return Json(_userService.GetFriends(userId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncomingRequests()
        {
            var userId = User.Identity.GetUserId();
            return Json(_userService.GetIncomingRequests(userId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOutgoingRequests()
        {
            var userId = User.Identity.GetUserId();
            return Json(_userService.GetOutgoingRequests(userId), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUserFriends(string id)
        {
            return Json(_userService.GetFriends(id), JsonRequestBehavior.AllowGet);
        }

        
    }
}
