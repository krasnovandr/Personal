using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeHistoryRepository : IRepository<NodeHistory>
    {
        private readonly ApplicationDbContext _db;

        public NodeHistoryRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<NodeHistory> GetAll()
        {
            return _db.NodeHistory;
        }

        public NodeHistory Get(int id)
        {
            return _db.NodeHistory.Find(id);
        }

        public void Create(NodeHistory item)
        {
            _db.NodeHistory.Add(item);
        }

        public void Update(NodeHistory item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            NodeHistory nodeHistory = _db.NodeHistory.Find(id);
            if (nodeHistory != null)
                _db.NodeHistory.Remove(nodeHistory);
        }
    }
}
