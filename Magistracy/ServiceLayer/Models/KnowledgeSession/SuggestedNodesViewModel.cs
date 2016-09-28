using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class SuggestedNodesViewModel
    {
        public List<NodeViewModel> Nodes { get; set; }
        public int ParentId { get; set; }
        public int SessionId { get; set; }
    }
}
