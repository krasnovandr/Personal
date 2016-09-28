using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AudioNetwork.Web.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class NodeController : Controller
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        //public JsonResult GetSessionNodeByLevel(int sessionId, int level)
        //{
        //    var result = _nodeService.GetSessionNodeByLevel(sessionId, level);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SaveSuggestedNodes(List<NodeViewModel> nodes, int sessionId)
        //{
        //    bool result = _nodeService.SaveSuggestedNodes(nodes, User.Identity.GetUserId(), sessionId);

        //    var context = GlobalHost.ConnectionManager.GetHubContext<KnowledgeSessionHub>();
        //    context.Clients.All.userAddSuggestion(User.Identity.GetUserId());

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddNode(NodeViewModel node, int sessionId)
        //{
        //    var result = _nodeService.AddNodeToSession(node, sessionId, User.Identity.GetUserId());
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult GetNode(int sessionId,int nodeId)
        //{
        //    var result = _nodeService.GetNode(sessionId,nodeId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}