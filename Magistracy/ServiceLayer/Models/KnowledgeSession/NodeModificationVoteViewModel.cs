using DataLayer.Models;
using System;

namespace ServiceLayer.Models.KnowledgeSession
{

    //public class NodeModificationVote
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public VoteTypes Type { get; set; }
    //    public virtual ApplicationUser VoteBy { get; set; }
    //    public virtual NodeModification NodeModification { get; set; }
    //    public DateTime Date { get; set; }
    //}
    public class NodeModificationVoteViewModel
    {
        public int Id { get; set; }
        public VoteTypes Type { get; set; }
        public string VoteBy { get; set; }
        public int NodeModificationId { get; set; }
        public DateTime Date { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string AvatarFilePath { get; set; }
    }
}
