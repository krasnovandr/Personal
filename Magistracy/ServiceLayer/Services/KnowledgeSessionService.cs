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
    public class KnowledgeSessionService : IKnowledgeSessionService
    {
        private readonly IUnitOfWork _db;
        private readonly ILevelVoteService _sessionVoteService;
        private readonly ISessionSuggestionService _sessionSuggestionService;

        public KnowledgeSessionService(
            IUnitOfWork db,
            ILevelVoteService sessionVoteService,
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
                session.NodesSuggestions.Add(new SessionNodeSuggestions
                {
                    SuggestedBy = userId,
                    DateCreation = DateTime.Now,
                    Level = 0,
                    Name = knowledgeSession.Theme,
                });
            }
            _db.KnowledgeSessions.Create(session);


            var result  =  _db.Save();

            return result ? session.Id : default(int);
        }

        public KnowledgeSessionViewModel GetSession(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            var sessionViewModel = Mapper.Map<KnowledgeSession, KnowledgeSessionViewModel>(session);
            sessionViewModel.Nodes = Mapper.Map<ICollection<Node>, List<NodeViewModel>>(session.Nodes);
            sessionViewModel.Users = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);
            sessionViewModel.NodesSuggestions = Mapper.Map<ICollection<SessionNodeSuggestions>, List<NodeViewModel>>(session.NodesSuggestions);
            sessionViewModel.Root = GetSessionRoot(sessionId);
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

 

        public void Dispose()
        {
            _db.Dispose();
        }


    }
}
