using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Services
{
    public class SessionVoteService : ISessionVoteService
    {
        private readonly IUnitOfWork db;

        public SessionVoteService(IUnitOfWork db)
        {
            this.db = db;
        }

        private const double levelVoteFinishedValue = 60;

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
            };

            db.LevelVotes.Create(levelVote);
            db.Save();

            return levelVote.Id;
        }

        public bool CheckLevelVoteFinished(int sessionId, int level)
        {
            var levelVotes = db.LevelVotes.GetAll()
                .Where(m => m.Level == level && m.SessionId == sessionId);

            var usersGroup = levelVotes.GroupBy(m => m.SuggetedBy).OrderBy(m => m.Count());

            var firstGroup = usersGroup.FirstOrDefault();
            var sessionUsers = db.KnowledgeSessions.Get(sessionId).Users;
            if (firstGroup != null)
            {
                double coefficient = (double)firstGroup.Count() / sessionUsers.Count;
                if (coefficient * 100 >= levelVoteFinishedValue)
                {
                    return true;
                }
            }


            return false;
        }

        public LevelVoteViewModel CheckUserForLevelVote(int sessionId, int level, string id)
        {
            var levelVotes = db.LevelVotes.GetAll();

            var result = levelVotes.FirstOrDefault(m => m.SessionId == sessionId && m.Level == level && m.VoteBy.Id == id);
            return result == null ? null : Mapper.Map<LevelVote, LevelVoteViewModel>(result);
        }

        public List<LevelVoteViewModel> GetVoteResults(int sessionId, int level, string id)
        {
            var levelVotes = db.LevelVotes.GetAll()
           .Where(m => m.Level == level && m.SessionId == sessionId);

            var userVotes = levelVotes.Where(m => m.SuggetedBy.Id == id);

            var result = Mapper.Map<IEnumerable<LevelVote>, List<LevelVoteViewModel>>(userVotes);

            return result;
        }

        public bool AddSuggestionVote(VoteViewModel voteViewModel, int sessionId)
        {
            var vote = new Vote
            {
                VoteDate = DateTime.Now,
                Type = voteViewModel.Type,
                VoteBy = db.Users.Get(voteViewModel.VoteBy),
            };

            var session = db.KnowledgeSessions.Get(sessionId);

            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == voteViewModel.Node.Id);

            if (node == null) return false;

            var suggestion = node.Suggestions.FirstOrDefault(m => m.Status == (int) SuggestionStatus.Open);

            if (suggestion == null) return false;

            suggestion.Votes.Add(vote);

            try
            {
                db.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
