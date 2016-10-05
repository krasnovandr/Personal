using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _db;

        public CommentsRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments;
        }

        public Comment Get(int id)
        {
            return _db.Comments.Find(id);
        }

        public void Create(Comment item)
        {
            _db.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var comment = _db.Comments.Find(id);
            if (comment != null)
                _db.Comments.Remove(comment);
        }
    }
}
