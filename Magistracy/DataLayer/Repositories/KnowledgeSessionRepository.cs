using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class KnowledgeSessionRepository : IRepository<KnowledgeSession>
    {
        private ApplicationDbContext db;

        public KnowledgeSessionRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<KnowledgeSession> GetAll()
        {
            return db.KnowledgeSessions;
        }

        public KnowledgeSession Get(int id)
        {
            return db.KnowledgeSessions.Find(id);
        }

        public void Create(KnowledgeSession item)
        {
            db.KnowledgeSessions.Add(item);
        }

        public void Update(KnowledgeSession item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            KnowledgeSession knowledgeSession = db.KnowledgeSessions.Find(id);
            if (knowledgeSession != null)
                db.KnowledgeSessions.Remove(knowledgeSession);
        }
    }
}
