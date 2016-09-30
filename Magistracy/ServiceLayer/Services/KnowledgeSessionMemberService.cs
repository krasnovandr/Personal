using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Services
{
    public class KnowledgeSessionMemberService : IKnowledgeSessionMemberService
    {
        private readonly IUnitOfWork _db;
        private readonly INodeService _nodeService;
        //private readonly ILevelVoteService _levelVoteService;
        //private readonly ISuggestionVoteService _suggestionVoteService;
        //private readonly ISuggestionService _suggestionService;

        public KnowledgeSessionMemberService(
            IUnitOfWork db, INodeService nodeService)
        //ILevelVoteService sessionVoteService,
        //ISuggestionService SuggestionService,
        //ILevelVoteService levelVoteService,
        //ISuggestionVoteService suggestionVoteService)
        {
            this._db = db;
            _nodeService = nodeService;
            //_suggestionService = SuggestionService;
            //_levelVoteService = levelVoteService;
            //_suggestionVoteService = suggestionVoteService;
        }


        public void AddMembersToSession(AddMembersViewModel addMembersViewModel)
        {
            var session = _db.KnowledgeSessions.Get(addMembersViewModel.SessionId);

            if (session == null)
                throw new Exception("Session was not found");

            foreach (var member in addMembersViewModel.Members)
            {
                var user = _db.Users.Get(member.Id);
                session.Users.Add(user);
            }

            //_db.KnowledgeSessions.Update(session);
            _db.Save();
        }

        public List<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var user = _db.Users.Get(userId);

            if (user != null)
            {
                var userSessionsViewModel =
                    Mapper.Map<ICollection<KnowledgeSession>, List<KnowledgeSessionViewModel>>(user.KnowledgeSessions);
                return userSessionsViewModel;
            }


            return null;
        }

        public List<TreeNodeViewModel> GetTree(int sessionId, string userId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            if (session == null)
                throw new Exception("Session was not found");

            var root = session.SessionNodes.FirstOrDefault(m => m.ParentId.HasValue == false);

            var groups = session.SessionNodes
                .Where(m => m.Type == NodeType.Configurator)
                .OrderBy(m => m.ParentId)
                .GroupBy(m => m.ParentId);

            if (root == null)
                throw new Exception("Root was not found");


            var rootNode = TreeNodeViewModelMapper(root, userId);

            foreach (var group in groups)
            {
                if (group.Key == null) continue;

                TreeNodeViewModel node = GetNodeToAdd(rootNode, group.Key);
                if (node != null)
                {
                    node.nodes.AddRange(group.Select(m => TreeNodeViewModelMapper(m, userId)));
                }
            }
            return new List<TreeNodeViewModel> { rootNode };
        }

        public TreeNodeViewModel TreeNodeViewModelMapper(SessionNode sessionNode, string userId)
        {
            var treeNode = new TreeNodeViewModel
            {
                Id = sessionNode.Id,
                text = sessionNode.Name,
                nodes = new List<TreeNodeViewModel>(),
                State = _nodeService.GetNodeState(userId, sessionNode.Id)
            };

            DetermineNodeColor(treeNode);
            return treeNode;
        }

        private void DetermineNodeColor(TreeNodeViewModel node)
        {
            switch (node.State)
            {
                case NodeStates.StructureSuggestion:
                    node.color = "#FFFFFF";
                    node.backColor = "#000000";
                    //backColor: "#FFFFFF""; 
                    break;
                case NodeStates.StructureSuggestionWait:
                    //node.color = "#FFD700";
                    node.backColor = "#FFD700";
                    break;
                case NodeStates.StructureSuggestionVote:
                    //node.color = "#006400";
                    node.backColor = "#006400";
                    break;
                case NodeStates.UpdatesAndComments: break;
                case NodeStates.Leaf: break;
            }
        }

        //       color: "#000000",
        //backColor: "#FFFFFF",

        private TreeNodeViewModel GetNodeToAdd(TreeNodeViewModel nodeToCheck, int? parentId)
        {
            if (nodeToCheck.Id == parentId)
            {
                return nodeToCheck;
            }

            foreach (var node in nodeToCheck.nodes)
            {
                var result = GetNodeToAdd(node, parentId);
                if (result != null)
                    return result;
            }

            return null;
        }

        //void AddMembersToSession(List<ApplicationUser> members, int sessionId);
        //List<KnowledgeSessionViewModel> GetUserSessions(string userId);
        //List<UserViewModel> GetMembers(NodeIdentifyModel nodeIdentifyModel);
        //List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel);
        //bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid);
        //UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel);


        public List<SessionUserViewModel> GetMembersExtended(int sessionId, int nodeId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            var members = Mapper.Map<ICollection<ApplicationUser>, List<SessionUserViewModel>>(session.Users);
          
            foreach (var member in members)
            {
                //    FillMemberViewModel(sessionId, member);
                member.NodeStructureSuggestionDone = _db.Nodes.GetAll()
                    .Any(m => m.ParentId == nodeId && m.SuggestedBy.Id == member.Id);
               var suggestedToNode = member.SessionNodes.Where(m => (m.ParentId ?? 0) == nodeId);
               member.SessionNodes = new List<NodeViewModel>(suggestedToNode);
                //    ValueResolver 
                //_levelVoteService.CheckUserForLevelVote(sessionId, member.Id, 0);

                //member.SuggestedNodes = 
            }

            return members;
        }

        public List<SessionUserViewModel> GetMembers(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            var members = Mapper.Map<ICollection<ApplicationUser>, List<SessionUserViewModel>>(session.Users);

            //foreach (var member in members)
            //{
            //    FillMemberViewModel(sessionId, member);
            //    member.LevelSuggestion = _levelVoteService.CheckUserForLevelVote(sessionId, member.Id, 0);
            //}

            return members;
        }
        ////REFACT
        //private void FillMemberViewModel(NodeIdentifyModel nodeIdentifyModel, UserViewModel member)
        //{
        //    var session = _db.KnowledgeSessions.Get(nodeIdentifyModel.SessionId);
        //    var suggestions = Mapper.Map<ICollection<SessionNode>, List<NodeViewModel>>(session.SessionNodes);
        //    member.NodeStructureSuggestionDone = CheckUserSuggestion(nodeIdentifyModel, member.Id);
        //    member.SuggestedNodes = suggestions.Where(m => m.SuggestedBy.Id == member.Id && m.ParentId == nodeIdentifyModel.ParentId).ToList(); ;
        //    member.VotesResults = _levelVoteService.GetVoteResults(nodeIdentifyModel, member.Id);
        //}

        //public List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel)
        //{
        //    var members = this.GetMembers(nodeIdentifyModel);

        //    return members.OrderByDescending(m => m.VotesResults.Count()).ToList();
        //}


        //public UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel)
        //{
        //    var winner = this.GetOrderedMembers(nodeIdentifyModel).FirstOrDefault();

        //    if (winner == null) throw new Exception("No members");

        //    foreach (var winnerNode in winner.SuggestedNodes)
        //    {
        //        _suggestionService.UpdateNodeWithSuggestions(nodeIdentifyModel.SessionId, nodeIdentifyModel.ParentId, winnerNode);
        //        _suggestionVoteService.UpdateSuggestionsWithVotes(nodeIdentifyModel.SessionId, nodeIdentifyModel.ParentId, winnerNode);
        //    }

        //    return winner;
        //}

        //public bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid)
        //{
        //    var session = _db.KnowledgeSessions.Get(nodeIdentifyModel.SessionId);

        //    foreach (var nodeSuggestion in session.SessionNodes)
        //    {
        //        if (nodeSuggestion.SuggestedBy.Id == userid
        //            && nodeSuggestion.ParentId == nodeIdentifyModel.ParentId)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}




        public void Dispose()
        {
            _db.Dispose();
        }


    }

    public class TreeNodeViewModel
    {
        public string backColor;
        public string color;
        public string text { get; set; }
        public int Id { get; set; }
        public List<TreeNodeViewModel> nodes { get; set; }
        public NodeStates State { get; set; }
    }
}
