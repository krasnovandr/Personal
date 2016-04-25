using System;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeSuggestionViewModel
    {
        public string SuggestedBy { get; set; }
        public string WinnerId { get; set; }
        public int Type { get; set; }
        public string Suggestion { get; set; }
        public string Comment { get; set; }
        public int SessionId { get; set; }
        public int Level { get; set; }
        public int? NodeId { get; set; }
        public int ParentId { get; set; }
        //public virtual ICollection<SessionNodeSuggestions> Nodes { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ICollection<Vote> Votes { get; set; }
    }
}