using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class SessionNodeSuggestions
    {
        public SessionNodeSuggestions()
        {
            KnowledgeSessions = new HashSet<KnowledgeSession>();
            Suggestions = new HashSet<Suggestion>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SuggestedBy { get; set; }
        public DateTime DateCreation { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }

        public virtual ICollection<KnowledgeSession> KnowledgeSessions { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }




}
