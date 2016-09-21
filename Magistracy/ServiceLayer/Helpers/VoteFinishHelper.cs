using System;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Helpers
{
    public class VoteFinishHelper : IVoteFinishHelper
    {
        private const double levelVoteFinishedValue = 60;
        private readonly TimeSpan voteDeadline = new TimeSpan(0, 0, 30, 0);


        public bool CheckLevelVoteFinished(int maximumVotes, int totalUsers, DateTime voteStartDate)
        {
            double coefficient = (double)maximumVotes / totalUsers;
            bool result = coefficient * 100 >= levelVoteFinishedValue;

            if (result)
            {
                return true;
            }

            return voteStartDate.Add(voteDeadline) <= DateTime.Now;

        }

        //public VoteResultTypes CheckSuggestionVoteFinished(int votesUpCount, int votesDownCount, int sessionUsers)
        //{
        //    double coefficientUp = (double)votesUpCount / sessionUsers;
            
        //    if (coefficientUp * 100 >= levelVoteFinishedValue)
        //    {
        //        return VoteResultTypes.Up;
        //    }

        //    double coefficientDown = (double)votesDownCount / sessionUsers;
            
        //    if (coefficientDown * 100 >= (100 - levelVoteFinishedValue))
        //    {
        //        {
        //            return VoteResultTypes.Down;
        //        }
        //    }

        //    return VoteResultTypes.NotFinished;
        //}
    }
}
