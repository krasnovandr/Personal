using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using Newtonsoft.Json;

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

        //public List<NodeStructureSuggestionVote> StructureVotes { get; set; }
        //public List<NodeModification> NodeModifications { get; set; }
        //public List<Comment> Comments { get; set; }
    }

    public class SuggestionNodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string SuggestedBy { get; set; }
        public int? ParentId { get; set; }
        public NodeType Type { get; set; }
        public NodeStates State { get; set; }

        [JsonIgnore]
        public List<NodeModificationViewModel> NodeModifications { get; set; }

        public NodeModificationViewModel CurrentSuggestion
        {
            get
            {
                return NodeModifications.FirstOrDefault(m => m.Status == ModificationStatus.Open);
            }
        }

        public List<CommentViewModel> Comments { get; set; }
    }

    //public class NodeStructureSuggestionViewModel
    //{
    //    public int Id { get; set; }
    //    public DateTime Date { get; set; }
    //    public List<SuggestionNodeViewModel> Nodes { get; set; }
    //    public List<NodeStructureSuggestionVoteViewModel> Votes { get; set; }
    //    //public SessionUserViewModel SuggestedBy { get; set; }
    //}

    //public class NodeStructureSuggestionVoteViewModel
    //{
    //    public int Id { get; set; }
    //    public SuggestionNodeViewModel VoteBy { get; set; }
    //    public NodeStructureVoteTypes VoteType { get; set; }
    //    public DateTime Date { get; set; }
    //}
}
