using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

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

        public JsonResult GetSessionNodeByLevel(int sessionId, int level)
        {
            var result = _knowledgeSessionService.GetSessionNodeByLevel(sessionId, level);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            _knowledgeSessionService.Dispose();
            base.Dispose(disposing);
        }
    }
}