using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public SessionUserViewModel SuggestedBy { get; set; }
        public int? ParentId { get; set; }
        public NodeType Type { get; set; }
        public NodeStates State { get; set; }


        public List<NodeStructureSuggestionVote> StructureVotes { get; set; }
        public List<NodeModification> NodeModifications { get; set; }
        public List<Comment> Comments { get; set; }
    }



      //[Key]
      //  public int Id { get; set; }
      //  public string Name { get; set; }
      //  public virtual ApplicationUser SuggestedBy { get; set; }
      //  public virtual KnowledgeSession Session { get; set; }
      //  public DateTime Date { get; set; }
      //  public int? ParentId { get; set; }
      //  public NodeType Type { get; set; }
      //  public NodeStates State { get; set; }

      //  public virtual ICollection<NodeStructureSuggestionVote> StructureVotes { get; set; }
      //  public virtual ICollection<NodeModification> NodeModifications { get; set; }
      //  public virtual ICollection<Comment> Comments { get; set; }
}
