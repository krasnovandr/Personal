using System.Web.Mvc;
using AudioNetwork.Web.Hubs;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class SessionVoteController : Controller
    {
        private readonly IHubContext _hubContext;
        private readonly ISessionVoteService _sessionVoteService;

        public SessionVoteController(ISessionVoteService sessionVoteService)
        {
            _sessionVoteService = sessionVoteService;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<KnowledgeSessionHub>();
        }



        public JsonResult LevelVote(LevelVoteViewModel levelVoteModel)
        {
            var result = _sessionVoteService.AddLevelVote(levelVoteModel);
            var finished = _sessionVoteService.CheckLevelVoteFinished(levelVoteModel.SessionId, levelVoteModel.Level);

            if (finished)
            {
                _hubContext.Clients.All.levelVoteFinished(levelVoteModel.SessionId, levelVoteModel.Level);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckVoteFinished(int session, int level)
        {
            var result = _sessionVoteService.CheckLevelVoteFinished(session, level);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetVoteResults(int session, int level)
        //{
        //    var result = _sessionVoteService.GetVoteResults(session, level);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult CheckUserLevelVote(int session, int level, string userId)
        {
            var result = _sessionVoteService.CheckUserForLevelVote(session, level, userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SuggestionVote(int sessionId, VoteViewModel voteViewModel)
        {
            var result = _sessionVoteService.AddSuggestionVote(voteViewModel, sessionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _sessionVoteService.Dispose();
            base.Dispose(disposing);
        }
    }
}