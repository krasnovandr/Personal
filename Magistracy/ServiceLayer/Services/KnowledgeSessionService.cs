using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class KnowledgeSessionService : IKnowledgeSessionService
    {
        private readonly IUnitOfWork db;

        public KnowledgeSessionService(IUnitOfWork db)
        {
            this.db = db;
        }

        public int CreateSession(KnowledgeSessionViewModel knowledgeSession, string userId)
        {
            var session = new KnowledgeSession
            {
                Theme = knowledgeSession.Theme,
                CreationDate = DateTime.Now,
                CreatorId = userId
            };
            var user = db.Users.Get(userId);

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
            db.KnowledgeSessions.Create(session);
            db.Save();

            return session.Id;
        }

        public void AddmembersToSession(List<ApplicationUser> members, int sessionId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);

            if (session != null)
            {
                foreach (var member in members)
                {
                    var user = db.Users.Get(member.Id);
                    session.Users.Add(user);
                }
            }

            db.KnowledgeSessions.Update(session);
            db.Save();
        }

        public int AddNodeToSession(NodeViewModel node, int sessionId, string userId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);
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
                db.KnowledgeSessions.Update(session);
                db.Save();
            }

            return newNode.Id;
        }

        public KnowledgeSessionViewModel GetSession(int sessionId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);

            var sessionViewModel = Mapper.Map<KnowledgeSession, KnowledgeSessionViewModel>(session);
            sessionViewModel.Nodes = Mapper.Map<ICollection<Node>, List<NodeViewModel>>(session.Nodes);
            sessionViewModel.Users = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);

            return sessionViewModel;
        }

        public NodeViewModel GetSessionRoot(int sessionId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);
            if (session == null) return null;

            var root = session.Nodes.FirstOrDefault(m => m.ParentId == null);
            var rootViewModel = Mapper.Map<Node, NodeViewModel>(root);

            return rootViewModel;
        }

        public List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level)
        {
            var session = db.KnowledgeSessions.Get(sessionId);
            if (session == null) return null;

            var levelNodes = session.Nodes.Where(m => m.Level == level);
            var levelNodesViewModel = Mapper.Map<IEnumerable<Node>, List<NodeViewModel>>(levelNodes);

            return levelNodesViewModel;
        }

        public List<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var sessions = db.KnowledgeSessions.GetAll().ToList();
            var userSessions = sessions.Where(session => session.Users.Any(m => m.Id == userId)).ToList();

            var userSessionsViewModel =
                Mapper.Map<List<KnowledgeSession>, List<KnowledgeSessionViewModel>>(userSessions);

            return userSessionsViewModel;
        }


        public bool AddUserToSession(ApplicationIdentity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}
