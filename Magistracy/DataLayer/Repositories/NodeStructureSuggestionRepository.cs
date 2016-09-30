using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeStructureSuggestionRepository : IRepository<NodeStructureSuggestion>
    {
        private ApplicationDbContext db;

        public NodeStructureSuggestionRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<NodeStructureSuggestion> GetAll()
        {
            return db.NodeStructureSuggestion;
        }

        public NodeStructureSuggestion Get(int id)
        {
            return db.NodeStructureSuggestion.Find(id);
        }

        public void Create(NodeStructureSuggestion item)
        {
            db.NodeStructureSuggestion.Add(item);
        }

        public void Update(NodeStructureSuggestion item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var nodeStructureSuggestion = db.NodeStructureSuggestion.Find(id);
            if (nodeStructureSuggestion != null)
                db.NodeStructureSuggestion.Remove(nodeStructureSuggestion);
        }
    }
}
