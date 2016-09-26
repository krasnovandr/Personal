using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Services;

namespace AudioNetwork.Web.API
{
    public class KnowledgeSessionApiController : ApiController
    {
        private readonly IKnowledgeSessionService _knowledgeSessionService;
        private readonly IKnowledgeSessionMemberService _knowledgeSessionMemberService;

        public KnowledgeSessionApiController(
            IKnowledgeSessionService knowledgeSessionService,
            IKnowledgeSessionMemberService knowledgeSessionMemberService)
        {
            _knowledgeSessionService = knowledgeSessionService;
            _knowledgeSessionMemberService = knowledgeSessionMemberService;
        }

        [HttpPost]
        public int Create(KnowledgeSessionViewModel knowledgeSessionViewModel)
        {
            var result = _knowledgeSessionService.CreateSession(knowledgeSessionViewModel, User.Identity.GetUserId());

            return result;
        }

        //[HttpGet]
        //public KnowledgeSessionViewModel GetSession(int sessionId)
        //{
        //    var result = _knowledgeSessionService.GetSession(sessionId);
        //    return result;
        //}

        public IEnumerable<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var result = _knowledgeSessionMemberService.GetUserSessions(userId);
            return result;
        }

        [HttpPost]
        public IHttpActionResult AddMembers(List<ApplicationUser> members, int sessionId)
        {
            _knowledgeSessionMemberService.AddmembersToSession(members, sessionId);
            return Ok();
        }

        public List<TreeNodeViewModel> GetTree(int sessionId)
        {
            var tree = _knowledgeSessionMemberService.GetTree(sessionId);
            return tree;
        }

        //public List<UserViewModel> GetMembers(int sessionId, int parentId)
        //{
        //    var result = _knowledgeSessionMemberService.GetMembers(new NodeIdentifyModel
        //    {
        //        SessionId = sessionId,
        //        ParentId = parentId
        //    });
        //    return result;
        //}

        //public List<UserViewModel> GetOrderedMembers(int sessionId, int parentId)
        //{
        //    var result = _knowledgeSessionMemberService.GetOrderedMembers(new NodeIdentifyModel
        //    {
        //        SessionId = sessionId,
        //        ParentId = parentId
        //    });
        //    return result;
        //}

        //public UserViewModel GetWinner(int sessionId, int parentId)
        //{
        //    var result = _knowledgeSessionMemberService.GetWinner(new NodeIdentifyModel
        //    {
        //        SessionId = sessionId,
        //        ParentId = parentId
        //    });
        //    return result;
        //}

        //public bool CheckUserSuggestion(NodeIdentifyModel identifyModel)
        //{
        //    var result = _knowledgeSessionMemberService.CheckUserSuggestion(identifyModel, User.Identity.GetUserId());
        //    return result;
        //}


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
