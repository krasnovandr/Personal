using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Suggestion
    {
        public Suggestion()
        {
            Nodes =  new HashSet<SessionNodeSuggestions>();
            Comments =  new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }
        public int Id { get; set; }
        public string SuggestedBy { get; set; }
        public DateTime SuggestionDate { get; set; }
        public int Type { get; set; }

        public virtual ICollection<SessionNodeSuggestions> Nodes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
