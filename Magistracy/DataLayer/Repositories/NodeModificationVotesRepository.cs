using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeModificationVotesRepository : IRepository<NodeModificationVote>
    {
             private readonly ApplicationDbContext _db;

             public NodeModificationVotesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

             public IEnumerable<NodeModificationVote> GetAll()
        {
            return _db.NodeModificationVotes;
        }

             public NodeModificationVote Get(int id)
        {
            return _db.NodeModificationVotes.Find(id);
        }

             public void Create(NodeModificationVote item)
        {
            _db.NodeModificationVotes.Add(item);
        }

             public void Update(NodeModificationVote item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var nodeModificationVote = _db.NodeModificationVotes.Find(id);
            if (nodeModificationVote != null)
                _db.NodeModificationVotes.Remove(nodeModificationVote);
        }
    }
}
