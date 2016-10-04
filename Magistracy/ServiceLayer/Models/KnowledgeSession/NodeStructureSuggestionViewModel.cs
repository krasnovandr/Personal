using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeStructureSuggestionViewModel
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SuggestionNodeViewModel> Nodes { get; set; }
        [JsonIgnore]
        public List<NodeStructureSuggestionVoteViewModel> Votes { get; set; }
        //public SessionUserViewModel SuggestedBy { get; set; }
    }
}