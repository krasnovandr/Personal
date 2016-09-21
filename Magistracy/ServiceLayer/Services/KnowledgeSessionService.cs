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

        public KnowledgeSessionService(
            IUnitOfWork db)
        {
            this._db = db;
        }

        public int CreateSession(KnowledgeSessionViewModel knowledgeSession, string userId)
        {
            var user = _db.Users.Get(userId);

            if (user == null) throw new Exception("User Not Found");

            var session = new KnowledgeSession
                {
                    Theme = knowledgeSession.Theme,
                    Date = DateTime.Now,
                    CreatorId = user.Id
                };
            session.Users.Add(user);
            session.SessionNodes.Add(new SessionNode
            {
                SuggestedBy = user,
                Date = DateTime.Now,
                Name = knowledgeSession.Theme,
                ParentId = null,
                Type = NodeType.Configurator
            });

            _db.KnowledgeSessions.Create(session);
            _db.Save();

            return session.Id;
        }

        public KnowledgeSessionViewModel GetSession(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            var sessionViewModel = Mapper.Map<KnowledgeSession, KnowledgeSessionViewModel>(session);
            //sessionViewModel.Nodes = Mapper.Map<ICollection<Node>, List<NodeViewModel>>(session.Nodes);
            sessionViewModel.Users = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);
            sessionViewModel.SessionNodes = Mapper.Map<ICollection<SessionNode>, List<NodeViewModel>>(session.SessionNodes);
            //sessionViewModel.Root = GetSessionRoot(sessionId);
            return sessionViewModel;
        }

        public NodeViewModel GetSessionRoot(int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            if (session == null) return null;
            var root = session.SessionNodes.FirstOrDefault(m => m.ParentId.HasValue == false);
            var rootViewModel = Mapper.Map<SessionNode, NodeViewModel>(root);

            return rootViewModel;
        }



        public void Dispose()
        {
            _db.Dispose();
        }


    }
}
