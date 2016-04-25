using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Interfaces
{
    public interface ISuggestionVoteService : IDisposable
    {
        //int AddLevelVote(LevelVoteViewModel levelVote);
        //string CheckLevelVoteFinished(int sessionId, int level, LevelVoteType levelVoteType);
        //LevelVoteViewModel CheckUserForLevelVote(int session, int level, string id);
        //List<LevelVoteViewModel> GetVoteResults(int sessionId, int value, string id);
        bool AddSuggestionVote(VoteViewModel voteViewModel, int sessionId);
        bool UpdateSuggestionsWithVotes(int sessionId, int? parentId, NodeViewModel winnerNode);
    }
}
