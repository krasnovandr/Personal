using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Services
{
    public class KnowledgeSessionService : IKnowledgeSessionService
    {
        private readonly IUnitOfWork _db;
        private readonly ISessionVoteService _sessionVoteService;
        private readonly ISessionSuggestionService _sessionSuggestionService;

        public KnowledgeSessionService(
            IUnitOfWork db, 
            ISessionVoteService sessionVoteService, 
            ISessionSuggestionService sessionSuggestionService)
        {
            this._db = db;
            _sessionVoteService = sessionVoteService;
            _sessionSuggestionService = sessionSuggestionService;
        }

        public int CreateSession(KnowledgeSessionViewModel knowledgeSession, string userId)
        {
            var session = new KnowledgeSession
            {
                Theme = knowledgeSession.Theme,
                CreationDate = DateTime.Now,
                CreatorId = userId
            };
            var user = _db.Users.Get(userId);

            if (user != null)
            {
                session.Users.Add(user);
                session.Nodes.Add(new Node
                {
                    CreatedBy = userId,
                    DateCreation = DateTime.Now,
                    Level = 0,
                    Name = knowledgeSession.Theme,
                });
            }
            _db.KnowledgeSessions.Create(session);
            _db.Save();

            return session.Id;
        }

        public void AddmembersToSession(List<ApplicationUser> members, int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            if (session != null)
            {
                foreach (var member in members)
                {
                    var user = _db.Users.Get(member.Id);
                    session.Users.Add(user);
                }
            }

            _db.KnowledgeSessions.Update(session);
            _db.Save();
        }

        public int AddNodeToSession(NodeViewModel node, int sessionId, string userId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            var newNode = new Node
            {
                CreatedBy = userId,
                DateCreation = DateTime.Now,
                Level = node.Level,
                Name = node.Name,
                ParentId = node.ParentId
            };

            if (session != null)
            {
                session.Nodes.Add(newNode);
                _db.KnowledgeSessions.Update(session);
                _db.Save();
            }

            return newNode.Id;
        }

        public KnowledgeSessionViewModel GetSession(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            var sessionViewModel = Mapper.Map<KnowledgeSession, KnowledgeSessionViewModel>(session);
            sessionViewModel.Nodes = Mapper.Map<ICollection<Node>, List<NodeViewModel>>(session.Nodes);
            sessionViewModel.Users = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);
            sessionViewModel.NodesSuggestions = Mapper.Map<ICollection<SessionNodeSuggestions>, List<NodeViewModel>>(session.NodesSuggestions);

            return sessionViewModel;
        }

        public NodeViewModel GetSessionRoot(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            if (session == null) return null;

            var root = session.Nodes.FirstOrDefault(m => m.ParentId == null);
            var rootViewModel = Mapper.Map<Node, NodeViewModel>(root);

            return rootViewModel;
        }

        public List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            if (session == null) return null;

            var levelNodes = session.Nodes.Where(m => m.Level == level);
            var levelNodesViewModel = Mapper.Map<IEnumerable<Node>, List<NodeViewModel>>(levelNodes);

            return levelNodesViewModel;
        }

        public List<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var sessions = _db.KnowledgeSessions.GetAll().ToList();
            var userSessions = sessions.Where(session => session.Users.Any(m => m.Id == userId)).ToList();

            var userSessionsViewModel =
                Mapper.Map<List<KnowledgeSession>, List<KnowledgeSessionViewModel>>(userSessions);

            return userSessionsViewModel;
        }

        public bool SaveSuggestedNodes(List<NodeViewModel> nodes, string userId, int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            var nodesList = Mapper.Map<List<NodeViewModel>, List<SessionNodeSuggestions>>(nodes);
            foreach (var node in nodesList)
            {
                node.DateCreation = DateTime.Now;
                node.SuggestedBy = userId;
                session.NodesSuggestions.Add(node);

            }
            var firstNode = nodes.FirstOrDefault();

            if (firstNode != null)
            {
                var result = CheckSessionSuggestions(sessionId, firstNode.Level);
                if (result)
                {
                    session.SessionState = (int)SessionState.FirstRoundMainBoard;
                }
            }

            try
            {
                _db.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<UserViewModel> GetMembers(int sessionId,int? level)
        {
            level = level ?? 1;
            var session = _db.KnowledgeSessions.Get(sessionId);
            var members = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);

            foreach (var member in members)
            {
                FillMemberViewModel(sessionId, level.Value, member);
            }

            return members;
        }

        private void FillMemberViewModel(int sessionId, int level, UserViewModel member)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            var suggestions = Mapper.Map<ICollection<SessionNodeSuggestions>, List<NodeViewModel>>(session.NodesSuggestions);

            member.SessionSuggestion = CheckUserSuggestion(sessionId, member.Id, null);
            member.SuggestedNodes = suggestions.Where(m => m.CreatedBy == member.Id && m.Level == level).ToList(); ;
            member.LevelSuggestion = _sessionVoteService.CheckUserForLevelVote(sessionId, level, member.Id);
            member.VotesResults = _sessionVoteService.GetVoteResults(sessionId, level, member.Id);
        }

        public List<UserViewModel> GetOrderedMembers(int sessionId, int? level)
        {
            var members = this.GetMembers(sessionId, level);

            return members.OrderByDescending(m => m.VotesResults.Count()).ToList();
        }


        public UserViewModel GetWinner(int sessionId, int? level)
        {
            var winner = this.GetOrderedMembers(sessionId, level).FirstOrDefault();

            if (winner == null)throw new Exception("No members");

            foreach (var winnerNode in winner.SuggestedNodes)
            {
                _sessionSuggestionService.UpdateNodeWithSuggestions(sessionId, level, winnerNode);
            }
            
            return winner;
        }

        public bool CheckSessionSuggestions(int sessionId, int? level)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            level = level ?? 1;

            var usersWithSuggestions = session.NodesSuggestions.Where(m => m.Level == level).Select(m => m.SuggestedBy).Distinct();
            var sessionUsers = session.Users.Select(m => m.Id).Distinct();

            return usersWithSuggestions.Count() == sessionUsers.Count();
        }

        public bool CheckUserSuggestion(int sessionId, string userid, int? level)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            level = level ?? 1;

            foreach (var nodeSuggestion in session.NodesSuggestions)
            {
                if (nodeSuggestion.SuggestedBy == userid && nodeSuggestion.Level == level)
                {
                    return true;
                }
            }

            return false;
            //return  session.NodesSuggestions.Any(m => m.SuggestedBy.Id == userid);
        }

   


        public bool AddUserToSession(ApplicationIdentity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db.Dispose();
        }


    }
}
