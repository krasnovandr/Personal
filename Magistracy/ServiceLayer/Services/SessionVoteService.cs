using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

namespace ServiceLayer.Services
{
    public class SessionVoteService : ISessionVoteService
    {
        private readonly IUnitOfWork db;
        private readonly ISessionSuggestionService _sessionSuggestionService;

        public SessionVoteService(
            IUnitOfWork db,
            ISessionSuggestionService sessionSuggestionService)
        {
            this.db = db;
            _sessionSuggestionService = sessionSuggestionService;
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

            var suggestion = GetCurrentSuggestion(voteViewModel.Node.Id, sessionId);
            if (suggestion == null) return false;

            var voteBefore =
                suggestion.Votes.FirstOrDefault(m => m.VoteBy.Id == voteViewModel.VoteBy);

            if (voteBefore != null)
            {
                if (voteBefore.Type == voteViewModel.Type)
                {
                    return false;
                }

                voteBefore.Type = voteViewModel.Type;
            }
            else
            {
                suggestion.Votes.Add(vote);

            }

            var voteFinished = CheckVoteFinished(suggestion.Votes, sessionId);

            if (voteFinished != VoteResultTypes.NotFinished)
                suggestion.Status = (int)SuggestionStatus.Closed;

            if (voteFinished == VoteResultTypes.Up)
            {
                switch (suggestion.Type)
                {
                    case (int)SuggestionTypes.Add: break;

                    case (int)SuggestionTypes.Edit:
                        var node = suggestion.Nodes.FirstOrDefault(m => m.Id == voteViewModel.Node.Id);
                        if (node != null) node.Name = suggestion.Value;
                        break;
                    case (int)SuggestionTypes.Remove:
                        RemoveNodeFromSuggestion(voteViewModel.Node.Id, sessionId);
                        break;
                }
            }

            if (voteFinished == VoteResultTypes.Down && suggestion.Type == (int)SuggestionTypes.Add)
            {
                RemoveNodeFromSuggestion(voteViewModel.Node.Id, sessionId);
            }

            return SaveToDb();
        }

        private void RemoveNodeFromSuggestion(int nodeId, int sessionId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);
            var nodeToRemove =
                session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
            if (nodeToRemove != null)
                session.NodesSuggestions.Remove(nodeToRemove);
        }

        private VoteResultTypes CheckVoteFinished(ICollection<Vote> votes, int sessionId)
        {
            var sessionUsers = db.KnowledgeSessions.Get(sessionId).Users.Count;

            var votesUp = votes.Where(m => m.Type == (int)VoteTypes.Up);
            var votesDown = votes.Where(m => m.Type == (int)VoteTypes.Down);

            double coefficientUp = (double)votesUp.Count() / sessionUsers;
            if (coefficientUp * 100 >= levelVoteFinishedValue)
            {
                return VoteResultTypes.Up;
            }

            double coefficientDown = (double)votesDown.Count() / sessionUsers;
            if (coefficientDown * 100 >= (100 - levelVoteFinishedValue))
            {
                return VoteResultTypes.Down;
            }

            return VoteResultTypes.NotFinished;
        }

        private bool SaveToDb()
        {
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

        private Suggestion GetCurrentSuggestion(int nodeId, int sessionId)
        {
            var session = db.KnowledgeSessions.Get(sessionId);

            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);

            if (node == null) return null;

            var suggestion = node.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

            if (suggestion == null) return null;

            return suggestion;
        }

        public bool UpdateSuggestionsWithVotes(int sessionId, int? level, NodeViewModel winnerNode)
        {
            var suggestion = GetCurrentSuggestion(winnerNode.Id, sessionId);
            if (suggestion == null) return false;


            var upVotes = suggestion.Votes.Where(m => m.Type == (int)VoteTypes.Up);
            var downVotes = suggestion.Votes.Where(m => m.Type == (int)VoteTypes.Down);
            winnerNode.CurrentSuggestion.VotesDown = Mapper.Map<IEnumerable<Vote>, List<VoteViewModel>>(downVotes);
            winnerNode.CurrentSuggestion.VotesUp = Mapper.Map<IEnumerable<Vote>, List<VoteViewModel>>(upVotes);

            return true;
        }
    }
}
