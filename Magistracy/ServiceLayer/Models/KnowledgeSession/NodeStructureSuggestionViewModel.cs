using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using Newtonsoft.Json;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeStructureSuggestionViewModel
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SuggestionNodeViewModel> Nodes { get; set; }
    
        public List<NodeStructureSuggestionVoteViewModel> Votes { get; set; }

        public int DoneLeafCount
        {
            get
            {
                return Votes.Count(m => m.VoteType == NodeStructureVoteTypes.DoneLeaf);
            }
        }

        public int DoneContinueCount
        {
            get
            {
                return Votes.Count(m => m.VoteType == NodeStructureVoteTypes.DoneContinue);
            }
        }

        public int TotalInitial
        {
            get
            {
                return Votes.Count(m => m.VoteType == NodeStructureVoteTypes.Initialize);
            }
        }
        //public SessionUserViewModel SuggestedBy { get; set; }
    }
}