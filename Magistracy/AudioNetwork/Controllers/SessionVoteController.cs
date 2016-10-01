using System;
using System.Web.Mvc;
using AudioNetwork.Web.Hubs;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace AudioNetwork.Web.Controllers
{
    public class SessionVoteController : Controller
    {
        private readonly IHubContext _hubContext;
        private readonly ILevelVoteService _levelVoteService;
        private readonly ISuggestionVoteService _suggestionVoteService;
        private readonly IHistoryService _historyService;

        public SessionVoteController(
            IHistoryService historyService,
            ISuggestionVoteService suggestionVoteService,
            ILevelVoteService levelVoteService)
        {
            _historyService = historyService;
            _suggestionVoteService = suggestionVoteService;
            _levelVoteService = levelVoteService;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<KnowledgeSessionHub>();
        }

        public ActionResult VoteUsersModal()
        {
            return View();
        }

        //public JsonResult LevelVote(LevelVoteViewModel levelVoteModel)
        //{
        //    var result = _levelVoteService.AddLevelVote(levelVoteModel);
        //    var identifyModel = new NodeIdentifyModel
        //    {
        //        ParentId = levelVoteModel.ParentId,
        //        SessionId = levelVoteModel.SessionId
        //    };
        //    var winner = _levelVoteService.CheckLevelVoteFinished(identifyModel, levelVoteModel.Type);

        //    if (string.IsNullOrEmpty(winner) == false)
        //    {
        //        _historyService.UpdateHistoryWithWinner(levelVoteModel.SessionId, levelVoteModel.Level, winner);
        //        _hubContext.Clients.All.levelVoteFinished(levelVoteModel.SessionId, levelVoteModel.Level);
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult CheckVoteFinished(NodeIdentifyModel nodeIdentifyModeltify, LevelVoteType levelVoteType)
        //{
        //    var result = _levelVoteService.CheckLevelVoteFinished(nodeIdentifyModeltify, levelVoteType);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult CheckUserLevelVote(NodeIdentifyModel nodeIdentifyModeltify, string userId, LevelVoteType levelVoteType)
        //{
        //    var result = _levelVoteService.CheckUserForLevelVote(nodeIdentifyModeltify, userId, levelVoteType);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SuggestionVote(int sessionId, SuggestionVoteViewModel voteViewModel)
        //{
        //    var result = _suggestionVoteService.AddSuggestionVote(voteViewModel, sessionId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        protected override void Dispose(bool disposing)
        {
            _suggestionVoteService.Dispose();
            _levelVoteService.Dispose();
            base.Dispose(disposing);
        }
    }
}