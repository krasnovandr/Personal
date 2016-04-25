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
    public class KnowledgeSessionMemberService : IKnowledgeSessionMemberService
    {
        private readonly IUnitOfWork _db;
        private readonly ILevelVoteService _levelVoteService;
        private readonly ISuggestionVoteService _suggestionVoteService;
        private readonly ISessionSuggestionService _sessionSuggestionService;

        public KnowledgeSessionMemberService(
            IUnitOfWork db,
            ILevelVoteService sessionVoteService,
            ISessionSuggestionService sessionSuggestionService,
            ILevelVoteService levelVoteService,
            ISuggestionVoteService suggestionVoteService)
        {
            this._db = db;
            _sessionSuggestionService = sessionSuggestionService;
            _levelVoteService = levelVoteService;
            _suggestionVoteService = suggestionVoteService;
        }



        public void AddmembersToSession(List<ApplicationUser> members, int sessionId)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);

            if (session != null)
            {
                foreach (var member in members)
                {
                    var user = _db.Users.Get(member.Id);
                    session.Users.Add(user);
                }
            }

            _db.KnowledgeSessions.Update(session);
            _db.Save();
        }

        public List<KnowledgeSessionViewModel> GetUserSessions(string userId)
        {
            var sessions = _db.KnowledgeSessions.GetAll().ToList();
            var userSessions = sessions.Where(session => session.Users.Any(m => m.Id == userId)).ToList();

            var userSessionsViewModel =
                Mapper.Map<List<KnowledgeSession>, List<KnowledgeSessionViewModel>>(userSessions);

            return userSessionsViewModel;
        }
        //void AddmembersToSession(List<ApplicationUser> members, int sessionId);
        //List<KnowledgeSessionViewModel> GetUserSessions(string userId);
        //List<UserViewModel> GetMembers(NodeIdentifyModel nodeIdentifyModel);
        //List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel);
        //bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid);
        //UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel);

        public List<UserViewModel> GetMembers(NodeIdentifyModel nodeIdentifyModel)
        {
            var session = _db.KnowledgeSessions.Get(nodeIdentifyModel.SessionId);
            var members = Mapper.Map<ICollection<ApplicationUser>, List<UserViewModel>>(session.Users);

            foreach (var member in members)
            {
                FillMemberViewModel(nodeIdentifyModel, member);
                member.LevelSuggestion = _levelVoteService.CheckUserForLevelVote(nodeIdentifyModel, member.Id, 0);
            }

            return members;
        }
        //REFACT
        private void FillMemberViewModel(NodeIdentifyModel nodeIdentifyModel, UserViewModel member)
        {
            var session = _db.KnowledgeSessions.Get(nodeIdentifyModel.SessionId);
            var suggestions = Mapper.Map<ICollection<SessionNodeSuggestions>, List<NodeViewModel>>(session.NodesSuggestions);
            member.SessionSuggestion = CheckUserSuggestion(nodeIdentifyModel, member.Id);
            member.SuggestedNodes = suggestions.Where(m => m.CreatedBy == member.Id && m.ParentId == nodeIdentifyModel.ParentId).ToList(); ;
            member.VotesResults = _levelVoteService.GetVoteResults(nodeIdentifyModel, member.Id);
        }

        public List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel)
        {
            var members = this.GetMembers(nodeIdentifyModel);

            return members.OrderByDescending(m => m.VotesResults.Count()).ToList();
        }


        public UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel)
        {
            var winner = this.GetOrderedMembers(nodeIdentifyModel).FirstOrDefault();

            if (winner == null) throw new Exception("No members");

            foreach (var winnerNode in winner.SuggestedNodes)
            {
                _sessionSuggestionService.UpdateNodeWithSuggestions(nodeIdentifyModel.SessionId, nodeIdentifyModel.ParentId, winnerNode);
                _suggestionVoteService.UpdateSuggestionsWithVotes(nodeIdentifyModel.SessionId, nodeIdentifyModel.ParentId, winnerNode);
            }

            return winner;
        }

        public bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid)
        {
            var session = _db.KnowledgeSessions.Get(nodeIdentifyModel.SessionId);

            foreach (var nodeSuggestion in session.NodesSuggestions)
            {
                if (nodeSuggestion.SuggestedBy == userid
                    && nodeSuggestion.ParentId == nodeIdentifyModel.ParentId)
                {
                    return true;
                }
            }

            return false;
        }




        public void Dispose()
        {
            _db.Dispose();
        }


    }
}
