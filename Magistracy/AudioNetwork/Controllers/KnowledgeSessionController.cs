using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class KnowledgeSessionController : Controller
    {
        // GET: KnowledgeSession
        private readonly IKnowledgeSessionService _knowledgeSessionService;
        private readonly IKnowledgeSessionMemberService _knowledgeSessionMemberService;

        public KnowledgeSessionController(
            IKnowledgeSessionService knowledgeSessionService,
            IKnowledgeSessionMemberService knowledgeSessionMemberService)
        {
            _knowledgeSessionService = knowledgeSessionService;
            _knowledgeSessionMemberService = knowledgeSessionMemberService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Round()
        {
            return View();
        }

        public ActionResult RoundLevelVote()
        {
            return View();
        }

        public ActionResult RoundWinnerVote()
        {
            return View();
        }


        public JsonResult Create(KnowledgeSessionViewModel knowledgeSessionViewModel)
        {
            var result = _knowledgeSessionService.CreateSession(knowledgeSessionViewModel, User.Identity.GetUserId());

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMembers(List<ApplicationUser> members, int sessionId)
        {
            _knowledgeSessionMemberService.AddmembersToSession(members, sessionId);
            return new EmptyResult();
        }



        public JsonResult GetSession(int sessionId)
        {
            var result = _knowledgeSessionService.GetSession(sessionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserSessions(string userId)
        {
            var result = _knowledgeSessionMemberService.GetUserSessions(userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMembers(int sessionId, int parentId)
        {
            var result = _knowledgeSessionMemberService.GetMembers(new NodeIdentifyModel
            {
                SessionId = sessionId,
                ParentId = parentId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderedMembers(int sessionId, int parentId)
        {
            var result = _knowledgeSessionMemberService.GetOrderedMembers(new NodeIdentifyModel
            {
                SessionId = sessionId,
                ParentId = parentId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWinner(int sessionId, int parentId)
        {
            var result = _knowledgeSessionMemberService.GetWinner(new NodeIdentifyModel
            {
                SessionId = sessionId,
                ParentId = parentId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckUserSuggestion(NodeIdentifyModel identifyModel)
        {
            var result = _knowledgeSessionMemberService.CheckUserSuggestion(identifyModel, User.Identity.GetUserId());
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetMembersWithSuggestion(int sessionId)
        //{
        //    var result = _knowledgeSessionService.GetMembers(sessionId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}




        protected override void Dispose(bool disposing)
        {
            _knowledgeSessionService.Dispose();
            base.Dispose(disposing);
        }
    }
}