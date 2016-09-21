using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodesRepository : IRepository<SessionNode>
    {
        private ApplicationDbContext db;

        public NodesRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<SessionNode> GetAll()
        {
            return db.Nodes;
        }

        public SessionNode Get(int id)
        {
            return db.Nodes.Find(id);
        }

        public void Create(SessionNode item)
        {
            db.Nodes.Add(item);
        }

        public void Update(SessionNode item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var node = db.Nodes.Find(id);
            if (node != null)
                db.Nodes.Remove(node);
        }
    }
}
