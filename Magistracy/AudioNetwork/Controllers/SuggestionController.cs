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
        private readonly ISuggestionService _suggestionService;

        public SuggestionController(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }


        //public ActionResult MakeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel)
        //{
        //    return Json(_suggestionService.MakeNodeSuggestion(nodeSuggestionViewModel),
        //        JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult AddComment(int sessionId,string comment,int nodeId)
        //{
        //    var result = _suggestionService.AddComment(sessionId, comment, nodeId, User.Identity.GetUserId());
        //    if (result)
        //    {
        //        return Json(_suggestionService.GetComments(sessionId, nodeId), JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(false,JsonRequestBehavior.AllowGet);
        //}

    }
}