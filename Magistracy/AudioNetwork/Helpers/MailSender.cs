using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DataLayer.Models;
//Request.Url.Authority 
namespace AudioNetwork.Helpers
{
    public static class MailSender
    {
        public static void SendEmailMessage(ApplicationUser user, string url)
        {
            var from = new MailAddress("krasnovandr@mail.ru", "Web Registration");
            var to = new MailAddress(user.Email);
            var mailMessage = new MailMessage(@from, to)
            {
                Subject = "Подтверждение регистрации по Email",
                Body = string.Format("Для завершения регистрации перейдите по ссылке:" + " " +
                                     "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                    url + "#/Register?" + "Id=" + user.Id + "&email=" + user.Email),
                IsBodyHtml = true
            };

            var smtp = new SmtpClient("smtp.mail.ru", 25)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("krasnovandr@mail.ru", "3323876may1993")
            };
            smtp.Send(mailMessage);
        }
    }
}