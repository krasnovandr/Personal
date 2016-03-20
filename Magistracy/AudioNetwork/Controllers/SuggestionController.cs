using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ISessionSuggestionService _sessionSuggestionService;

        public SuggestionController(ISessionSuggestionService sessionSuggestionService)
        {
            _sessionSuggestionService = sessionSuggestionService;
        }

        public ActionResult SuggestionModal()
        {
            return View();
        }

        public ActionResult MakeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel)
        {
            return Json(_sessionSuggestionService.MakeNodeSuggestion(nodeSuggestionViewModel),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddComment(int sessionId,string comment,int nodeId)
        {
            var result = _sessionSuggestionService.AddComment(sessionId, comment, nodeId, User.Identity.GetUserId());
            if (result)
            {
                return Json(_sessionSuggestionService.GetComments(sessionId, nodeId), JsonRequestBehavior.AllowGet);
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }

    }
}