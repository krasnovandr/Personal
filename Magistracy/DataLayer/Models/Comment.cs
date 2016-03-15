
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Comment
    {
        public Comment()
        {
            Suggestions =  new HashSet<Suggestion>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public virtual ApplicationUser CommentBy { get; set; }

        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
