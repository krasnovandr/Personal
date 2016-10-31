using System;
using System.Collections.Generic;
using DataLayer.Models;
using Shared;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class TextMergeSuggestionAddViewModel
    {
        public string Value { get; set; }
        public string SuggestedBy { get; set; }
        public int FirstResourceId { get; set; }
        public int SecondResourceId { get; set; }
        public int ClusterId { get; set; }
        public int NodeId { get; set; }
    }

    public class TextMergeSuggestionEditViewModel
    {
        public int SuggestionId { get; set; }
        public string Value { get; set; }
    }


    public class TextMergeSuggestionViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
        public SessionUserCompactModel SuggestedBy { get; set; }
        public NodeResourceViewModel FirstResource { get; set; }
        public NodeResourceViewModel SecondResource { get; set; }
        //public virtual ResourceCluster Cluster { get; set; }
        public List<TextMergeSuggestionVoteViewModel> Votes { get; set; }
        public int? UserVote { get; set; }
        public TextSuggestionStatus Status { get; set; }

    }

    public class TextMergeSuggestionVoteViewModel
    {
        public int SessionId { get; set; }
        public int SuggestionId { get; set; }
        public int ClusterId { get; set; }
        public int NodeId { get; set; }
        public string VoteBy{ get; set; }
        public DateTime Date { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string AvatarFilePath { get; set; }
    }


    //public class TextMergeSuggestionVote
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public VoteTypes Type { get; set; }
    //    public virtual ApplicationUser VoteBy { get; set; }
    //    public virtual TextMergeSuggestion TextMergeSuggestion { get; set; }
    //    public DateTime Date { get; set; }
    //}

}
