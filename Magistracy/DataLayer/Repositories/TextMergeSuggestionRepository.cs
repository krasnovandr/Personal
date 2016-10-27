using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class TextMergeSuggestionRepository : IRepository<TextMergeSuggestion>
    {
        private readonly ApplicationDbContext _db;

        public TextMergeSuggestionRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<TextMergeSuggestion> GetAll()
        {
            return _db.TextMergeSuggestions;
        }

        public TextMergeSuggestion Get(int id)
        {
            return _db.TextMergeSuggestions.Find(id);
        }

        public void Create(TextMergeSuggestion item)
        {
            _db.TextMergeSuggestions.Add(item);
        }

        public void Update(TextMergeSuggestion item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var textMergeSuggestion = _db.TextMergeSuggestions.Find(id);
            if (textMergeSuggestion != null)
                _db.TextMergeSuggestions.Remove(textMergeSuggestion);
        }
    }
}
