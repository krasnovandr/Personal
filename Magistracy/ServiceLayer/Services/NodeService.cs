//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using DataLayer.Interfaces;
//using DataLayer.Models;
//using ServiceLayer.Interfaces;
//using ServiceLayer.Models.KnowledgeSession;
//using ServiceLayer.Models.KnowledgeSession.Enums;

//namespace ServiceLayer.Services
//{
//    public class NodeService : INodeService
//    {
//        private readonly IUnitOfWork _db;

//        public NodeService(IUnitOfWork db)
//        {
//            this._db = db;
//        }

//        public int AddNodeToSession(NodeViewModel node, int sessionId, string userId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            throw new NotImplementedException();
//            //var newNode = new Node
//            //{
//            //    CreatedBy = userId,
//            //    DateCreation = DateTime.Now,
//            //    Level = node.Level,
//            //    Name = node.Name,
//            //    ParentId = node.ParentId
//            //};

//            //if (session != null)
//            //{
//            //    session.Nodes.Add(newNode);
//            //    _db.KnowledgeSessions.Update(session);
//            //    _db.Save();
//            //}

//            //return newNode.Id;
//        }


//        public List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            if (session == null) return null;
//                   throw new NotImplementedException();
//        //    var levelNodes = session.Nodes.Where(m => m.Level == level);
//        //    var levelNodesViewModel = Mapper.Map<IEnumerable<Node>, List<NodeViewModel>>(levelNodes);

//        //    return levelNodesViewModel;
//        }

//        public NodeViewModel GetNode(int sessionId, int nodeId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);

//            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);

//            return  node != null ? Mapper.Map<SessionNodes, NodeViewModel>(node) : null;
//        }

//        public bool SaveSuggestedNodes(List<NodeViewModel> nodes, string userId, int sessionId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);

//            var nodesList = Mapper.Map<List<NodeViewModel>, List<SessionNodes>>(nodes);
//            foreach (var node in nodesList)
//            {
//                node.DateCreation = DateTime.Now;
//                node.SuggestedBy = userId;
//                session.NodesSuggestions.Add(node);

//            }
//            var firstNode = nodes.FirstOrDefault();

//            if (firstNode != null)
//            {
//                var result = CheckSessionSuggestions(sessionId, firstNode.Level);
//                if (result)
//                {
//                    session.SessionState = (int)SessionState.FirstRoundMainBoard;
//                }
//            }
        
//            return _db.Save();
//        }

//        public bool CheckSessionSuggestions(int sessionId, int? level)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            level = level ?? 1;

//            var usersWithSuggestions = session.NodesSuggestions.Where(m => m.Level == level).Select(m => m.SuggestedBy).Distinct();
//            var sessionUsers = session.Users.Select(m => m.Id).Distinct();

//            return usersWithSuggestions.Count() == sessionUsers.Count();
//        }
//    }
//}
