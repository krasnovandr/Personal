using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeStructureVotesRepository : IRepository<NodeStructureVote>
    {
        private readonly ApplicationDbContext _db;

        public NodeStructureVotesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<NodeStructureVote> GetAll()
        {
            return _db.NodeStructureVotes;
        }

        public NodeStructureVote Get(int id)
        {
            return _db.NodeStructureVotes.Find(id);
        }

        public void Create(NodeStructureVote item)
        {
            _db.NodeStructureVotes.Add(item);
        }

        public void Update(NodeStructureVote item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            NodeStructureVote nodeStructureVote = _db.NodeStructureVotes.Find(id);
            if (nodeStructureVote != null)
                _db.NodeStructureVotes.Remove(nodeStructureVote);
        }
    }
}
