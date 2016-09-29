using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeStructureVotesRepository : IRepository<NodeStructureSuggestionVote>
    {
        private readonly ApplicationDbContext _db;

        public NodeStructureVotesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<NodeStructureSuggestionVote> GetAll()
        {
            return _db.NodeStructureVotes;
        }

        public NodeStructureSuggestionVote Get(int id)
        {
            return _db.NodeStructureVotes.Find(id);
        }

        public void Create(NodeStructureSuggestionVote item)
        {
            _db.NodeStructureVotes.Add(item);
        }

        public void Update(NodeStructureSuggestionVote item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            NodeStructureSuggestionVote nodeStructureSuggestionVote = _db.NodeStructureVotes.Find(id);
            if (nodeStructureSuggestionVote != null)
                _db.NodeStructureVotes.Remove(nodeStructureSuggestionVote);
        }
    }
}
