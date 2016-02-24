using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AudioNetwork
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //ExpireTimeSpan = new TimeSpan(1,0,0,0),
                CookieName = "AuthCoockie",
             //   ExpireTimeSpan = new TimeSpan(1)
                //CookieSecure = CookieSecureOption.Always,
                

            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseVkontakteAuthentication("4832199", "AmfedoaOETi6tvuyri7u", "friends,audio");
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }

        //public class EmailService : IIdentityMessageService
        //{
        //    public Task SendAsync(IdentityMessage message)
        //    {
        //        // настройка логина, пароля отправителя
        //        var from = "krasnovandr@mail.ru";
        //        var pass = "3323876may1993";

        //        // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
        //        SmtpClient client = new SmtpClient("smtp.mail.ru", 25);

        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new System.Net.NetworkCredential(from, pass);
        //        client.EnableSsl = true;

        //        // создаем письмо: message.Destination - адрес получателя
        //        var mail = new MailMessage(from, message.Destination);
        //        mail.Subject = message.Subject;
        //        mail.Body = message.Body;
        //        mail.IsBodyHtml = true;

        //        return client.SendMailAsync(mail);
        //    }
        //}
    }
}