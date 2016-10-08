using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public enum NodeType
    {
        Suggested,
        Configurator
    }

    public enum NodeStructureVoteTypes
    {
        Initialize,
        DoneLeaf,
        DoneContinue
    }

    public enum VoteTypes
    {
        Approve,
        Reject
    }

    public enum NodeStructureVoteType
    {
        Initialize,
        DoneLeaf,
        DoneContinue
    }

    public enum NodeStates
    {
        StructureSuggestion,
        StructureSuggestionWait,
        StructureSuggestionVote,
        StructureSuggestionWinner,
        WinAndNotLeaf,
        Leaf
    }

    public enum ModificationType
    {
        Add,
        Edit,
        Remove
    }

    public enum VoteResultTypes
    {
        Up,
        Down,
        NotFinished
    }

    public enum ModificationStatus
    {
        Open,
        Accepted,
        Declined
    }

    public class KnowledgeSession
    {
        public KnowledgeSession()
        {
            Users = new HashSet<ApplicationUser>();
            SessionNodes = new List<SessionNode>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string Theme { get; set; }
        public string CreatorId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<SessionNode> SessionNodes { get; set; }
    }

    public class SessionNode
    {
        public SessionNode()
        {
            StructureVotes = new List<NodeStructureSuggestionVote>();
            NodeModifications = new List<NodeModification>();
            Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ApplicationUser SuggestedBy { get; set; }
        public virtual KnowledgeSession Session { get; set; }
        public virtual NodeStructureSuggestion NodeStructureSuggestion { get; set; }
        public DateTime Date { get; set; }
        public int? ParentId { get; set; }
        public NodeType Type { get; set; }
        public NodeStates State { get; set; }

        public virtual ICollection<NodeStructureSuggestionVote> StructureVotes { get; set; }
        public virtual ICollection<NodeModification> NodeModifications { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<NodeResource> NodeResources { get; set; }
    }

    public class NodeStructureSuggestionVote
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser VoteBy { get; set; }
        public virtual NodeStructureSuggestion Suggestion { get; set; }
        public virtual SessionNode SessionNode { get; set; }
        public NodeStructureVoteTypes VoteType { get; set; }
        public DateTime Date { get; set; }
    }

    public class NodeModification
    {
        public NodeModification()
        {
            //Nodes = new HashSet<SessionNode>();
            //Comments = new HashSet<Comment>();
            Votes = new List<NodeModificationVote>();
        }

        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser SuggestedBy { get; set; }
        public virtual SessionNode Node { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }

        public ModificationType Type { get; set; }
        public ModificationStatus Status { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<NodeModificationVote> Votes { get; set; }
    }

    public class NodeModificationVote
    {
        [Key]
        public int Id { get; set; }

        public VoteTypes Type { get; set; }
        public virtual ApplicationUser VoteBy { get; set; }
        public virtual NodeModification NodeModification { get; set; }
        public DateTime Date { get; set; }
    }

    public class Comment
    {

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string Value { get; set; }
        public virtual ApplicationUser CommentBy { get; set; }
        public virtual SessionNode CommentTo { get; set; }

        //public virtual ICollection<NodeModification> Suggestions { get; set; }
    }

    public class NodeHistory
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual SessionNode Node { get; set; }
        //public int SessionNodeSuggestionsId { get; set; }
        public string Value { get; set; }
        public virtual ApplicationUser ByUser { get; set; }
        public int? SuggestionId { get; set; }
    }

    public class NodeStructureSuggestion
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public virtual ICollection<SessionNode> Nodes { get; set; }
        public virtual ICollection<NodeStructureSuggestionVote> Votes { get; set; }
        public virtual ApplicationUser SuggestedBy { get; set; }
        public int? ParentId { get; set; }
    }

    public enum ContentType
    {
        Text,
        Image,
        Audio
    }

    public class NodeResource
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual SessionNode Node { get; set; }
        public virtual ApplicationUser AddBy { get; set; }
        //public ContentType Type { get; set; }
        public string ResourceRaw { get; set; }
        public string Resource { get; set; }
    }
}
