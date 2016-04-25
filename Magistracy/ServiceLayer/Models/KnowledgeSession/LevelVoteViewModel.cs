using System;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class LevelVoteViewModel
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string SuggetedBy { get; set; }
        public UserViewModel SuggetedByUser { get; set; }
        public int SessionId { get; set; }
        public LevelVoteType Type { get; set; }
        public DateTime Date{ get; set; }
        public string VoteBy { get; set; }
        public UserViewModel VoteByUser { get; set; }
        public int? ParentId { get; set; }
    }
}
