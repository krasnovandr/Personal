using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Services
{
    public class LevelVoteService : ILevelVoteService
    {
        private readonly IUnitOfWork db;
        private readonly IVoteFinishHelper _voteFinishHelper;

        public LevelVoteService(
            IUnitOfWork db,
            IVoteFinishHelper voteFinishHelper)
        {
            this.db = db;
            _voteFinishHelper = voteFinishHelper;
        }


        public void Dispose()
        {
            db.Dispose();
        }

        public int AddLevelVote(LevelVoteViewModel levelVoteViewModel)
        {
            var levelVote = new LevelVote
            {
                Level = levelVoteViewModel.Level,
                SessionId = levelVoteViewModel.SessionId,
                SuggetedBy = db.Users.Get(levelVoteViewModel.SuggetedBy),
                VoteBy = db.Users.Get(levelVoteViewModel.VoteBy),
                Type = (int)levelVoteViewModel.Type,
                Date = DateTime.Now,
                ParentId = levelVoteViewModel.ParentId
            };

            db.LevelVotes.Create(levelVote);
            db.Save();

            return levelVote.Id;
        }

        public string CheckLevelVoteFinished(NodeIdentifyModel nodeIdentifyModeltify, LevelVoteType levelVoteType)
        {
            bool voteFinished = false;
            var levelVotes = db.LevelVotes.GetAll()
                .Where(
                m => 
                     m.SessionId == nodeIdentifyModeltify.SessionId
                    && m.Type == (int)levelVoteType
                    && m.ParentId == nodeIdentifyModeltify.ParentId).ToList();

            var usersGroup = levelVotes.GroupBy(m => m.SuggetedBy).OrderBy(m => m.Count());
            var firstVote = levelVotes.OrderBy(m => m.Date).FirstOrDefault();

            var firstGroup = usersGroup.FirstOrDefault();
            var sessionUsers = db.KnowledgeSessions.Get(nodeIdentifyModeltify.SessionId).Users;
            if (firstGroup == null) return null;
            if (firstVote == null) return null;

            voteFinished = _voteFinishHelper.CheckLevelVoteFinished(firstGroup.Count(), sessionUsers.Count, firstVote.Date);

            if (voteFinished)
            {
                var firstOrDefault = firstGroup.FirstOrDefault();
                if (firstOrDefault != null) return firstOrDefault.SuggetedBy.Id;
                return null;
            }

            return null;
        }

        public LevelVoteViewModel CheckUserForLevelVote(NodeIdentifyModel nodeIdentifyModeltify, string userId, LevelVoteType levelVoteType)
        {
            var levelVotes = db.LevelVotes.GetAll();

            var result = levelVotes.FirstOrDefault(
                m => m.SessionId == nodeIdentifyModeltify.SessionId
                    && m.ParentId == nodeIdentifyModeltify.ParentId
                    && m.VoteBy.Id == userId
                    && m.Type == (int)levelVoteType);
            return result == null ? null : Mapper.Map<LevelVote, LevelVoteViewModel>(result);
        }

        public List<LevelVoteViewModel> GetVoteResults(NodeIdentifyModel nodeIdentifyModeltify, string id)
        {
            var levelVotes = db.LevelVotes.GetAll()
           .Where(m =>m.SessionId == nodeIdentifyModeltify.SessionId
                && m.ParentId == nodeIdentifyModeltify.ParentId);

            var userVotes = levelVotes.Where(m => m.SuggetedBy.Id == id);

            var result = Mapper.Map<IEnumerable<LevelVote>, List<LevelVoteViewModel>>(userVotes);

            return result;
        }
    }
}
