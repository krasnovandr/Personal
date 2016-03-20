using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ServiceLayer.Models.KnowledgeSession
{
   public class SuggestionViewModel
    {
        public int Id { get; set; }
        public UserViewModel SuggestedBy { get; set; }
        public DateTime SuggestionDate { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Value { get; set; }

        public List<VoteViewModel> VotesUp { get; set; }
        public List<VoteViewModel> VotesDown { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
