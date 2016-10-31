using System.Collections.Generic;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ITextMergeSuggestionService
    {
        void MakeSuggestion(TextMergeSuggestionAddViewModel suggestionAddViewModel);
        void EditSuggestion(TextMergeSuggestionEditViewModel suggestionEditViewModel);
        List<TextMergeSuggestionViewModel> GetSuggestions(int nodeId, int clusterId, string userId);
        int? CheckUserSuggestion(int clusterId, string userId, int firstResource, int secondResource);
        bool VoteSuggestion(TextMergeSuggestionVoteViewModel voteViewModel);
        bool CheckVoteIsDone(int nodeId, int clusterId);
    }
}
