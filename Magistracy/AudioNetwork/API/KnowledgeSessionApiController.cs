using System;
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
using Shared;

namespace AudioNetwork.Web.API
{
    //[Authorize]
    public class KnowledgeSessionApiController : ApiController
    {
        private readonly IKnowledgeSessionService _knowledgeSessionService;
        private readonly IKnowledgeSessionMemberService _knowledgeSessionMemberService;
        private readonly INodeService _nodeService;
        private readonly ISuggestionService _suggestionService;
        private readonly INodeModificationService _nodeModificationService;
        private readonly ICommentsService _commentsService;
        private readonly INodeResourceService _nodeResourceService;
        private readonly ITextMergeSuggestionService _textMergeSuggestionService;

        public KnowledgeSessionApiController(
            IKnowledgeSessionService knowledgeSessionService,
            IKnowledgeSessionMemberService knowledgeSessionMemberService,
            INodeService nodeService,
            ISuggestionService suggestionService,
            INodeModificationService nodeModificationService,
            ICommentsService commentsService,
            INodeResourceService nodeResourceService,
            ITextMergeSuggestionService textMergeSuggestionService)
        {
            _knowledgeSessionService = knowledgeSessionService;
            _knowledgeSessionMemberService = knowledgeSessionMemberService;
            _nodeService = nodeService;
            _suggestionService = suggestionService;
            _nodeModificationService = nodeModificationService;
            _commentsService = commentsService;
            _nodeResourceService = nodeResourceService;
            _textMergeSuggestionService = textMergeSuggestionService;
        }

        [HttpPost]
        public void MakeTextMergeSuggestion(TextMergeSuggestionAddViewModel suggestionAddViewModel)
        {
            _textMergeSuggestionService.MakeSuggestion(suggestionAddViewModel);
        }

        [HttpGet]
        public int? CheckUserTextMergeSuggestion(int nodeId, int clusterId, string userId)
        {
            return _textMergeSuggestionService.CheckUserSuggestion(nodeId, clusterId, userId);
        }

        [HttpGet]
        public List<TextMergeSuggestionViewModel> GetTextMergeSuggestions(int nodeId, int clusterId)
        {
            return _textMergeSuggestionService.GetSuggestions(nodeId, clusterId);
        }

        [HttpPost]
        public void EditTextMergeSuggestion(TextMergeSuggestionEditViewModel suggestionEditViewModel)
        {
            _textMergeSuggestionService.EditSuggestion(suggestionEditViewModel);
        }
        [HttpPost]
        public bool VoteTextMergeSuggestion(TextMergeSuggestionVoteViewModel voteViewModel)
        {
            return _textMergeSuggestionService.VoteSuggestion(voteViewModel);
        }

        [HttpGet]
        public List<NodeResourceViewModel> GetNodeResources(int nodeId)
        {
            return _nodeResourceService.GetNodeResources(nodeId);
        }

        [HttpPost]
        public void AddResourceToNode(NodeResourceViewModel resourceViewModel)
        {
            _nodeResourceService.AddResourceToNode(resourceViewModel);
        }

        [HttpPost]
        public List<CommentViewModel> CreateCommentToNode(CommentViewModel commentView)
        {
            _commentsService.Create(commentView);
            return _commentsService.Get(commentView.CommentTo);
        }
        [HttpGet]
        public List<CommentViewModel> GetNodeComments(int nodeid)
        {
            return _commentsService.Get(nodeid);
        }

        [HttpPost]
        public void CreateNodeModificationSuggestion(NodeModificationViewModel nodeModificationViewModel)
        {
            _nodeModificationService.CreateNodeModificationSuggestion(nodeModificationViewModel);
        }

        [HttpPost]
        public void VoteNodeModificationSuggestion(NodeModificationVoteViewModel nodeModificationVoteViewModel)
        {
            _nodeModificationService.VoteNodeModificationSuggestion(nodeModificationVoteViewModel);
        }

        [HttpGet]
        public IEnumerable<SuggestionSessionUserViewModel> GetSuggestions(int sessionId, int nodeId)
        {
            return _suggestionService.GetSuggestions(sessionId, nodeId);
        }
        [HttpGet]
        public SuggestionSessionUserViewModel GetNodeStructureSuggestionWinner(int nodeId)
        {
            return _suggestionService.GetNodeStructureSuggestionWinner(nodeId);
        }

        [HttpPost]
        public IEnumerable<SuggestionSessionUserViewModel> VoteNodeStructureSuggestion(NodeStructureSuggestionVoteViewModel suggestion)
        {
            _suggestionService.VoteNodeStructureSuggestion(suggestion);
            return _suggestionService.GetSuggestions(suggestion.SessionId, suggestion.NodeId);
        }

        [HttpGet]
        public bool CheckStructureSuggestionVoteDone(int sessionId, int nodeId, NodeStructureVoteTypes voteType)
        {
            return _suggestionService.CheckStructureSuggestionVoteDone(sessionId, nodeId, voteType);
        }

        [HttpGet]
        public int? CheckUserStructureSuggestionVote(string userId, int nodeId)
        {
            return _suggestionService.CheckUserStructureSuggestionVote(userId, nodeId);
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
