using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public UserViewModel VoteByUser { get; set; }
        public string VoteBy { get; set; }
        public DateTime VoteDate { get; set; }
        public int NodeId { get; set; }

        //public NodeViewModel Node { get; set; }
    }
}
