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
    public interface ILevelVoteService:IDisposable
    {
        int AddLevelVote(LevelVoteViewModel levelVote);
        string CheckLevelVoteFinished(NodeIdentifyModel nodeIdentifyModeltify, LevelVoteType levelVoteType);
        //LevelVoteViewModel CheckUserForLevelVote(int session, int level, string id, LevelVoteType levelVoteType);
        List<LevelVoteViewModel> GetVoteResults(NodeIdentifyModel nodeIdentifyModeltify, string userId);
        LevelVoteViewModel CheckUserForLevelVote(NodeIdentifyModel nodeIdentifyModeltify, string userId, LevelVoteType levelVoteType);
    }
}
