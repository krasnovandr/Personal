using System;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private KnowledgeSessionRepository _knowledgeSessionRepository;
        private NodesRepository _nodesRepository;
        private UserRepository _userRepository;
        private NodeStructureVotesRepository _nodeStructureVotesRepository;
        private NodeHistoryRepository _nodeHistoryRepository;
        private bool _disposed = false;

        public IRepository<SessionNode> Nodes
        {
            get
            {
                return _nodesRepository ??
                    (_nodesRepository = new NodesRepository(_db));
            }
        }

        public IRepository<NodeHistory> NodesHistory
        {
            get
            {
                return _nodeHistoryRepository ??
                    (_nodeHistoryRepository = new NodeHistoryRepository(_db));
            }
        }

        public IRepository<KnowledgeSession> KnowledgeSessions
        {
            get
            {
                return _knowledgeSessionRepository ??
                    (_knowledgeSessionRepository = new KnowledgeSessionRepository(_db));
            }
        }

        public IRepository<NodeStructureSuggestionVote> LevelVotes
        {
            get
            {
                return _nodeStructureVotesRepository ??
                    (_nodeStructureVotesRepository = new NodeStructureVotesRepository(_db));
            }
        }


        public ExtendedRepository<ApplicationUser> Users
        {
            get
            {
                return _userRepository ??
                       (_userRepository = new UserRepository(_db));
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
