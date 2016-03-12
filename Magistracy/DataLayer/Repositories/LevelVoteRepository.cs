using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class LevelVoteRepository : IRepository<LevelVote>
    {
        private readonly ApplicationDbContext _db;

        public LevelVoteRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<LevelVote> GetAll()
        {
            return _db.LevelVotes;
        }

        public LevelVote Get(int id)
        {
            return _db.LevelVotes.Find(id);
        }

        public void Create(LevelVote item)
        {
            _db.LevelVotes.Add(item);
        }

        public void Update(LevelVote item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            LevelVote levelVote = _db.LevelVotes.Find(id);
            if (levelVote != null)
                _db.LevelVotes.Remove(levelVote);
        }
    }
}
