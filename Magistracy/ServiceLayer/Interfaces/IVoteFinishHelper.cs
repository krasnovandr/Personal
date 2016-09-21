using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using JetBrains.Annotations;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Interfaces
{
    public interface IVoteFinishHelper
    {
        bool CheckLevelVoteFinished(int maximumVotes, int totalUsers, DateTime voteStartDate);
        //VoteResultTypes CheckSuggestionVoteFinished(int votesUpCount, int votesDownCount, int sessionUsers);
    }
}
