using System;
using System.Collections.Generic;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class KnowledgeSessionViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Theme { get; set; }
        public string CreatorId { get; set; }
        public int SessionState { get; set; }

        public virtual List<UserViewModel> Users { get; set; }
        public virtual List<NodeViewModel> NodesSuggestions { get; set; }
        public virtual List<NodeViewModel> Nodes { get; set; }
        public NodeViewModel Root { get; set; }
    }
}
