using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class ResourceClusterRepository : IRepository<ResourceCluster>
    {
        private readonly ApplicationDbContext _db;

        public ResourceClusterRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<ResourceCluster> GetAll()
        {
            return _db.ResourceClusters;
        }

        public ResourceCluster Get(int id)
        {
            return _db.ResourceClusters.Find(id);
        }

        public void Create(ResourceCluster item)
        {
            _db.ResourceClusters.Add(item);
        }

        public void Update(ResourceCluster item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var resourceCluster = _db.ResourceClusters.Find(id);
            if (resourceCluster != null)
                _db.ResourceClusters.Remove(resourceCluster);
        }
    }
}
