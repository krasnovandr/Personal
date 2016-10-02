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
    public class NodeModificationsRepository : IRepository<NodeModification>
    {
        private readonly ApplicationDbContext _db;

        public NodeModificationsRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<NodeModification> GetAll()
        {
            return _db.NodeModifications;
        }

        public NodeModification Get(int id)
        {
            return _db.NodeModifications.Find(id);
        }

        public void Create(NodeModification item)
        {
            _db.NodeModifications.Add(item);
        }

        public void Update(NodeModification item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var nodeModification = _db.NodeModifications.Find(id);
            if (nodeModification != null)
                _db.NodeModifications.Remove(nodeModification);
        }
    }
}
