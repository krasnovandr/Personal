using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Services
{
    public class NodeService : INodeService
    {
        private readonly IUnitOfWork _db;
        private readonly ISuggestionService _suggestionService;

        public NodeService(
            IUnitOfWork db,
            ISuggestionService suggestionService)
        {
            this._db = db;
            _suggestionService = suggestionService;
        }

        //public int AddNodeToSession(NodeViewModel node, int sessionId, string userId)
        //{
        //    var session = _db.KnowledgeSessions.Get(sessionId);
        //    throw new NotImplementedException();
        //    //var newNode = new Node
        //    //{
        //    //    CreatedBy = userId,
        //    //    DateCreation = DateTime.Now,
        //    //    Level = node.Level,
        //    //    Name = node.Name,
        //    //    ParentId = node.ParentId
        //    //};

        //    //if (session != null)
        //    //{
        //    //    session.Nodes.Add(newNode);
        //    //    _db.KnowledgeSessions.Update(session);
        //    //    _db.Save();
        //    //}

        //    //return newNode.Id;
        //}


        //public List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level)
        //{
        //    var session = _db.KnowledgeSessions.Get(sessionId);
        //    if (session == null) return null;
        //    throw new NotImplementedException();
        //    //    var levelNodes = session.Nodes.Where(m => m.Level == level);
        //    //    var levelNodesViewModel = Mapper.Map<IEnumerable<Node>, List<NodeViewModel>>(levelNodes);

        //    //    return levelNodesViewModel;
        //}

        public NodeViewModel GetNode(int nodeId)
        {
            var node = _db.Nodes.Get(nodeId);
            if (node == null)
                throw new Exception("node was not found");

            return Mapper.Map<SessionNode, NodeViewModel>(node);
        }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public virtual ApplicationUser SuggestedBy { get; set; }
        //public virtual KnowledgeSession Session { get; set; }
        //public DateTime Date { get; set; }
        //public int? ParentId { get; set; }
        //public NodeType Type { get; set; }
        //public NodeStates State { get; set; }

        //public virtual ICollection<NodeStructureSuggestionVote> StructureVotes { get; set; }
        //public virtual ICollection<NodeModification> NodeModifications { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }

        public void SaveSuggestedNodes(SuggestedNodesViewModel model, string userId)
        {
            var session = _db.KnowledgeSessions.Get(model.SessionId);
            var user = _db.Users.Get(userId);

            var nodesList = Mapper.Map<List<NodeViewModel>, ICollection<SessionNode>>(model.Nodes);

            var suggestionId = _suggestionService.CreateSuggestion(userId, model.ParentId);
            foreach (var node in nodesList)
            {
                node.Date = DateTime.Now;
                node.SuggestedBy = user;
                node.Session = session;
                node.ParentId = model.ParentId;
                node.State = NodeStates.StructureSuggestion;
                node.Type = NodeType.Suggested;
                node.NodeStructureSuggestion = _db.NodeStructureSuggestions.Get(suggestionId);
                session.SessionNodes.Add(node);
                user.SessionNodes.Add(node);
            }

            var parentNode = _db.Nodes.Get(model.ParentId);

            var suggestionComplete = ParentNodeSuggestionsComplete(model.SessionId, model.ParentId);

            if (suggestionComplete)
            {
                parentNode.State = NodeStates.StructureSuggestionVote;
            }

            //var firstNode = nodes.FirstOrDefault();

            //if (firstNode != null)
            //{
            //    var result = ParentNodeSuggestionsComplete(sessionId, firstNode.Level);
            //    if (result)
            //    {
            //        session.SessionState = (int)SessionState.FirstRoundMainBoard;
            //    }
            //}

            _db.Save();
        }

        public NodeStates GetNodeState(string userId, int nodeId)
        {
            var user = _db.Users.Get(userId);
            var nodes = _db.Nodes.GetAll();
            var node = _db.Nodes.Get(nodeId);

            if (node.State == NodeStates.StructureSuggestionVote)
            {
                return NodeStates.StructureSuggestionVote;
            }

            if (node.State == NodeStates.StructureSuggestionWinner)
            {
                return NodeStates.StructureSuggestionWinner;
            }

            if (node.State == NodeStates.WinAndNotLeaf)
            {
                return NodeStates.WinAndNotLeaf;
            }

            var haveSuggestedNotes = nodes.Any(m => m.ParentId == nodeId && m.SuggestedBy == user);
            if (haveSuggestedNotes)
            {
                return NodeStates.StructureSuggestionWait;
            }


            return NodeStates.StructureSuggestion;
        }

        public bool ParentNodeSuggestionsComplete(int sessionId, int nodeId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            var usersWithSuggestions = session.SessionNodes.Where(m => m.ParentId == nodeId)
                .Select(m => m.SuggestedBy).Distinct();
            var sessionUsers = session.Users.Select(m => m.Id);

            return usersWithSuggestions.Count() == sessionUsers.Count();
        }
    }
}
