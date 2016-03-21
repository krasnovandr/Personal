using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Interfaces;

namespace AudioNetwork.Web.Controllers
{
    public class NodeHistoryController : Controller
    {
        // GET: NodeHistory
        private readonly IHistoryService _historyService;

        public NodeHistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetHistory(int sessionId, int nodeId)
        {

            return Json(_historyService.Get(sessionId, nodeId), JsonRequestBehavior.AllowGet);
        }
    }
}