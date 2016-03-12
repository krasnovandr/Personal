using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class LevelVote
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public virtual ApplicationUser SuggetedBy { get; set; }
        public int SessionId { get; set; }
        public virtual ApplicationUser VoteBy { get; set; }
    }
}
