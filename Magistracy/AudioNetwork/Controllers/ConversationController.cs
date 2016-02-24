using System.Collections.Generic;
using System.Web.Mvc;
using AudioNetwork.Models;
using AudioNetwork.Services;
using DataLayer.Models;
using Microsoft.AspNet.Identity;

namespace AudioNetwork.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;

        public ConversationController(
            IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public ActionResult ViewConversations()
        {
            return View("Conversations");
        }

        public ActionResult ViewEditConversations()
        {
            return View("ConversationsEdit");
        }

        public ActionResult ViewConversation()
        {
            return View("ViewConversation");
        }
        
        public ActionResult ChangeAvatarModal()
        {
            return View("ChangeAvatarModal");
        }

        public JsonResult GetMyConversations(int type)
        {
            var userId = User.Identity.GetUserId();
            return Json(_conversationService.GetConversations(type, userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMusicConversations()
        {
            var userId = User.Identity.GetUserId();
            return Json(_conversationService.GetMusicConversations(userId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddConversation(ConversationViewModel conversationViewModel)
        {
            var userId = User.Identity.GetUserId();
            _conversationService.AddConversation(userId, conversationViewModel);
            return new EmptyResult();
        }

        public JsonResult AddOrGetDialog(string userId)
        {
            var myId = User.Identity.GetUserId();
            return Json(_conversationService.AddOrGetDialog(userId, myId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveConversation(ConversationViewModel conversationViewModel)
        {
            var userId = User.Identity.GetUserId();
            _conversationService.RemoveConversation(userId, conversationViewModel);
            return new EmptyResult();
        }

        public JsonResult GetConversationPeople(ConversationViewModel conversationViewModel)
        {
            return Json(_conversationService.GetConversationPeople(conversationViewModel.ConversationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConversationMessages(ConversationViewModel conversationViewModel)
        {
            var userId = User.Identity.GetUserId();
            return Json(_conversationService.GetConversationMessages(conversationViewModel, userId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMessageToConversation(string text, string conversationId, List<Song> songs)
        {
            var myId = User.Identity.GetUserId();
            _conversationService.AddMessageToConversation(myId, text, conversationId, songs);
            return new EmptyResult();
        }

        public ActionResult RemoveMessageFromConversation(string messageId, string conversationId)
        {
            var myId = User.Identity.GetUserId();
            _conversationService.RemoveMessageFromConversation(myId, messageId, conversationId);
            return new EmptyResult();
        }

        public ActionResult AddUserToConversation(string userId, string conversationId)
        {
            _conversationService.AddUserToConversation(userId, conversationId);
            return new EmptyResult();
        }

        public ActionResult RemoveUserFromConversation(UserViewModel user, string conversationId)
        {
            _conversationService.RemoveUserFromConversation(user.Id, conversationId);
            return new EmptyResult();
        }

        public JsonResult GetMyNotReadMessagesCount()
        {
            var myId = User.Identity.GetUserId();
            return Json(_conversationService.GetMyNotReadMessagesCount(myId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadConversationMessages(string conversationId)
        {
            var myId = User.Identity.GetUserId();
            return Json(_conversationService.ReadConversationMessages(myId, conversationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateConversationCurrentSong(string conversationId, string songId)
        {
            _conversationService.UpdateConversationCurrentSong(conversationId, songId);
            return Json(new { Succes = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConversation(string conversationId)
        {
            var userId = User.Identity.GetUserId();
            return Json(_conversationService.GetConversation(userId, conversationId), JsonRequestBehavior.AllowGet);
        }
    }
}