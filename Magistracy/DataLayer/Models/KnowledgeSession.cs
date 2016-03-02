using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class KnowledgeSession
    {
        public KnowledgeSession()
        {
            Users = new HashSet<ApplicationUser>();
            Nodes = new HashSet<Node>();
            NodesSuggestions= new HashSet<SessionNodeSuggestions>();
        }

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Theme { get; set; }
        public string CreatorId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Node> Nodes { get; set; }
        public virtual ICollection<SessionNodeSuggestions> NodesSuggestions { get; set; }
    }
}
