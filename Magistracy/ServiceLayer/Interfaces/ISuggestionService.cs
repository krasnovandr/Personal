using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ISuggestionService
    {
        SuggestionSessionUserViewModel GetNodeStructureSuggestionWinner(int nodeId);
        bool CheckStructureSuggestionVoteDone(int sessionId, int nodeId, NodeStructureVoteTypes voteType);
        int? CheckUserStructureSuggestionVote(string userId, int nodeId);
        List<SuggestionSessionUserViewModel> GetSuggestions(int sessionId, int nodeId);
        int CreateSuggestion(string userId, int parentId);
        void VoteNodeStructureSuggestion(NodeStructureSuggestionVoteViewModel suggestion);
    }
}