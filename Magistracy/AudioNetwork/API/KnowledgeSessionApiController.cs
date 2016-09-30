﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using AudioNetwork.Web.Hubs;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Services;

namespace AudioNetwork.Web.API
{
    //[Authorize]
    public class KnowledgeSessionApiController : ApiController
    {
        private readonly IKnowledgeSessionService _knowledgeSessionService;
        private readonly IKnowledgeSessionMemberService _knowledgeSessionMemberService;
        private readonly INodeService _nodeService;
        private readonly ISuggestionService _suggestionService;

        public KnowledgeSessionApiController(
            IKnowledgeSessionService knowledgeSessionService,
            IKnowledgeSessionMemberService knowledgeSessionMemberService,
            INodeService nodeService,
            ISuggestionService suggestionService)
        {
            _knowledgeSessionService = knowledgeSessionService;
            _knowledgeSessionMemberService = knowledgeSessionMemberService;
            _nodeService = nodeService;
            _suggestionService = suggestionService;
        }

        [HttpGet]
        public IEnumerable<SuggestionSessionUserViewModel> GetSuggestions(int sessionId, int nodeId)
        {
            return _suggestionService.GetSuggestions(sessionId, nodeId);
        }

        [HttpPost]
        public int Create(KnowledgeSessionViewModel knowledgeSessionViewModel)
        {
            var result = _knowledgeSessionService.CreateSession(knowledgeSessionViewModel, User.Identity.GetUserId());

            return result;
        }

        [HttpGet]
        public KnowledgeSessionViewModel GetSession(int sessionId)
        {
            var result = _knowledgeSessionService.GetSession(sessionId);
            return result;
        }

        [HttpGet]
        public NodeViewModel GetSessionRoot(int sessionId)
        {
            var result = _knowledgeSessionService.GetSessionRoot(sessionId);
            return result;
        }

        [HttpGet]
        public NodeViewModel GetNode(int nodeId)
        {
            var result = _nodeService.GetNode(nodeId);
            return result;
        }

        [HttpGet]
        public IEnumerable<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var result = _knowledgeSessionMemberService.GetUserSessions(userId);
            return result;
        }

        [HttpPost]
        public IHttpActionResult AddMembers(AddMembersViewModel addMembersViewModel)
        {
            _knowledgeSessionMemberService.AddMembersToSession(addMembersViewModel);
            return Ok();
        }

        [HttpGet]
        public List<TreeNodeViewModel> GetTree(int sessionId)
        {
            var tree = _knowledgeSessionMemberService.GetTree(sessionId, User.Identity.GetUserId());
            return tree;
        }

        [HttpPost]
        public void SaveSuggestedNodes(SuggestedNodesViewModel suggestedNodesViewModel)
        {
            _nodeService.SaveSuggestedNodes(suggestedNodesViewModel, User.Identity.GetUserId());

            //var context = GlobalHost.ConnectionManager.GetHubContext<KnowledgeSessionHub>();
            //context.Clients.All.userAddSuggestion(User.Identity.GetUserId());
        }


        public List<SessionUserViewModel> GetMembersExtended(int sessionId, int nodeId)
        {
            var result = _knowledgeSessionMemberService.GetMembersExtended(sessionId, nodeId);
            return result;
        }

        public List<SessionUserViewModel> GetMembers(int sessionId)
        {
            var result = _knowledgeSessionMemberService.GetMembers(sessionId);
            return result;
        }

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
