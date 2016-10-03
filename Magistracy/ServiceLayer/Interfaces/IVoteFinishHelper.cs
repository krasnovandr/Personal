using System;
using DataLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IVoteFinishHelper
    {
        bool CheckLevelVoteFinished(int maximumVotes, int totalUsers, DateTime voteStartDate);
        //VoteResultTypes CheckSuggestionVoteFinished(int votesUpCount, int votesDownCount, int sessionUsers);
        VoteResultTypes CheckModificationVoteFinished(int votesUp, int votesDown, int usersCount);
    }
}
