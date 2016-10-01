using System;
using DataLayer.Models;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeStructureSuggestionVoteViewModel
    {
        public int Id { get; set; }
        public string VoteBy { get; set; }
        public DateTime Date { get; set; }
        public int NodeId { get; set; }
        public int SessionId { get; set; }
        public NodeStructureVoteTypes VoteType { get; set; }
        public int SuggestionId { get; set; }
    }
}
