using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class NodeResourceRepository : IRepository<NodeResource>
    {
        private readonly ApplicationDbContext _db;

        public NodeResourceRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<NodeResource> GetAll()
        {
            return _db.NodeResources;
        }

        public NodeResource Get(int id)
        {
            return _db.NodeResources.Find(id);
        }

        public void Create(NodeResource item)
        {
            _db.NodeResources.Add(item);
        }

        public void Update(NodeResource item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var nodeResource = _db.NodeResources.Find(id);
            if (nodeResource != null)
                _db.NodeResources.Remove(nodeResource);
        }
    }
}
