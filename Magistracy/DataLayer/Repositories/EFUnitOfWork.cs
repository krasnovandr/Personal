using System;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private KnowledgeSessionRepository _knowledgeSessionRepository;
        private UserRepository _userRepository;
        private LevelVoteRepository _levelVoteRepository;

        public IRepository<KnowledgeSession> KnowledgeSessions
        {
            get
            {
                return _knowledgeSessionRepository ??
                    (_knowledgeSessionRepository = new KnowledgeSessionRepository(db));
            }
        }

        public IRepository<LevelVote> LevelVotes
        {
            get
            {
                return _levelVoteRepository ??
                    (_levelVoteRepository = new LevelVoteRepository(db));
            }
        }


        public ExtendedRepository<ApplicationUser> Users
        {
            get
            {
                return _userRepository ??
                       (_userRepository = new UserRepository(db));
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
