using System.Collections.Generic;
using System.Web.Mvc;
using AudioNetwork.Web.Hubs;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class KnowledgeSessionController : Controller
    {
        // GET: KnowledgeSession
        private readonly IKnowledgeSessionService _knowledgeSessionService;

        public KnowledgeSessionController(IKnowledgeSessionService knowledgeSessionService)
        {
            _knowledgeSessionService = knowledgeSessionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FirstRound()
        {
            return View();
        }


        public ActionResult FirstRoundMainBoard()
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
            _knowledgeSessionService.AddmembersToSession(members, sessionId);
            return new EmptyResult();
        }

        public JsonResult AddNode(NodeViewModel node, int sessionId)
        {
            var result = _knowledgeSessionService.AddNodeToSession(node, sessionId, User.Identity.GetUserId());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSession(int sessionId)
        {
            var result = _knowledgeSessionService.GetSession(sessionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserSessions(string userId)
        {
            var result = _knowledgeSessionService.GetUserSessions(userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMembers(int sessionId)
        {
            var result = _knowledgeSessionService.GetMembers(sessionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckUserSuggestion(int sessionId)
        {
            var result = _knowledgeSessionService.CheckUserSuggestion(sessionId,User.Identity.GetUserId(),null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetMembersWithSuggestion(int sessionId)
        //{
        //    var result = _knowledgeSessionService.GetMembers(sessionId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        
        public JsonResult GetSessionNodeByLevel(int sessionId, int level)
        {
            var result = _knowledgeSessionService.GetSessionNodeByLevel(sessionId, level);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSuggestedNodes(List<NodeViewModel> nodes, int sessionId)
        {
            bool result = _knowledgeSessionService.SaveSuggestedNodes(nodes, User.Identity.GetUserId(), sessionId);
            
            var context = GlobalHost.ConnectionManager.GetHubContext<KnowledgeSessionHub>();
            context.Clients.All.userAddSuggestion(User.Identity.GetUserId());
         
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            _knowledgeSessionService.Dispose();
            base.Dispose(disposing);
        }
    }
}