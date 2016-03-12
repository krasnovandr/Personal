using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ISessionVoteService:IDisposable
    {
        int AddLevelVote(LevelVoteViewModel levelVote);
        bool CheckLevelVoteFinished(int sessionId, int level);
        bool CheckUserForLevelVote(int session, int level, string id);
        List<LevelVoteViewModel> GetVoteResults(int sessionId, int value, string id);
    }
}
