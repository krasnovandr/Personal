using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ITextMergeSuggestionService
    {
        void MakeSuggestion(TextMergeSuggestionAddViewModel suggestionAddViewModel);
        void EditSuggestion(TextMergeSuggestionEditViewModel suggestionEditViewModel);
        List<TextMergeSuggestionViewModel> GetSuggestions(int nodeId, int clusterId);
        int? CheckUserSuggestion(int nodeId, int clusterId, string userId);
        bool VoteSuggestion(TextMergeSuggestionVoteViewModel voteViewModel);
    }
}
