using System;
using System.Collections.Generic;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeStructureSuggestionViewModel
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SuggestionNodeViewModel> Nodes { get; set; }
        //public List<NodeStructureSuggestionVoteViewModel> Votes { get; set; }
        //public SessionUserViewModel SuggestedBy { get; set; }
    }
}