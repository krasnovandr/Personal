using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Vote
    {
        public Vote()
        {
            Suggestions = new HashSet<Suggestion>();
        }
        public int Id { get; set; }
        public int Type { get; set; }
        public string VoteBy { get; set; }
        public DateTime VoteDate { get; set; }

        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
