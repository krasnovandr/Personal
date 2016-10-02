using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Newtonsoft.Json;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeModificationViewModel
    {
        public int Id { get; set; }
        public string SuggestedBy { get; set; }
        public int? NodeId { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public ModificationType Type { get; set; }
        public ModificationStatus Status { get; set; }
        public int SuggestionId { get; set; }
        //    //public virtual ICollection<SessionNode> Nodes { get; set; }
        //    //public virtual ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        public virtual ICollection<NodeModificationVoteViewModel> Votes { get; set; }


        public IEnumerable<NodeModificationVoteViewModel> VotesUp
        {
            get { return Votes.Where(m => m.Type == VoteTypes.Approve); }
        }

        public IEnumerable<NodeModificationVoteViewModel> VotesDown
        {
            get
            {
                return Votes.Where(m => m.Type == VoteTypes.Reject); 
            }
        }
    }

    //public class NodeModification
    //{
    //    public NodeModification()
    //    {
    //        //Nodes = new HashSet<SessionNode>();
    //        //Comments = new HashSet<Comment>();
    //        Votes = new List<NodeModificationVote>();
    //    }

    //    [Key]
    //    public int Id { get; set; }
    //    public virtual ApplicationUser SuggestedBy { get; set; }
    //    public virtual SessionNode Node { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Value { get; set; }

    //    public ModificationType Type { get; set; }
    //    public ModificationStatus Status { get; set; }

    //    //public virtual ICollection<SessionNode> Nodes { get; set; }
    //    //public virtual ICollection<Comment> Comments { get; set; }
    //    public virtual ICollection<NodeModificationVote> Votes { get; set; }
    //}
}
