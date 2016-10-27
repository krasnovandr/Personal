using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class TextMergeSuggestionVoteRepository : IRepository<TextMergeSuggestionVote>
    {
        private readonly ApplicationDbContext _db;

        public TextMergeSuggestionVoteRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<TextMergeSuggestionVote> GetAll()
        {
            return _db.TextMergeSuggestionVotes;
        }

        public TextMergeSuggestionVote Get(int id)
        {
            return _db.TextMergeSuggestionVotes.Find(id);
        }

        public void Create(TextMergeSuggestionVote item)
        {
            _db.TextMergeSuggestionVotes.Add(item);
        }

        public void Update(TextMergeSuggestionVote item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var textMergeSuggestionVote = _db.TextMergeSuggestionVotes.Find(id);
            if (textMergeSuggestionVote != null)
                _db.TextMergeSuggestionVotes.Remove(textMergeSuggestionVote);
        }
    }
}
