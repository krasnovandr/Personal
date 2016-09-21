//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using DataLayer.Interfaces;
//using DataLayer.Models;
//using ServiceLayer.Interfaces;
//using ServiceLayer.Models.KnowledgeSession;
//using ServiceLayer.Models.KnowledgeSession.Enums;

//namespace ServiceLayer.Services
//{
//    public class SuggestionVoteService : ISuggestionVoteService
//    {
//        private readonly IUnitOfWork _db;
//        private readonly IHistoryService _historyService;
//        private readonly IVoteFinishHelper _voteFinishHelper;

//        public SuggestionVoteService(
//            IUnitOfWork db,
//            IHistoryService historyService,
//            IVoteFinishHelper voteFinishHelper)
//        {
//            _db = db;
//            _historyService = historyService;
//            _voteFinishHelper = voteFinishHelper;
//        }

//        public bool AddSuggestionVote(VoteViewModel voteViewModel, int sessionId)
//        {
//            var vote = new Vote
//            {
//                VoteDate = DateTime.Now,
//                Type = voteViewModel.Type,
//                VoteBy = _db.Users.Get(voteViewModel.VoteBy),
//            };

//            var suggestion = GetCurrentSuggestion(voteViewModel.NodeId, sessionId);
//            if (suggestion == null) return false;

//            var voteBefore =
//                suggestion.Votes.FirstOrDefault(m => m.VoteBy.Id == voteViewModel.VoteBy);

//            if (voteBefore != null)
//            {
//                voteBefore.Type = voteViewModel.Type;
//            }
//            else
//            {
//                suggestion.Votes.Add(vote);

//            }

//            var voteFinished = CheckVoteFinished(suggestion.Votes, sessionId);

//            //if (voteFinished != VoteResultTypes.NotFinished)
//            //    suggestion.Status = (int)SuggestionStatus.Closed;

//            if (voteFinished == VoteResultTypes.Up)
//            {
//                suggestion.Status = (int)SuggestionStatus.Accepted;
//                switch (suggestion.Type)
//                {
//                    case (int)SuggestionTypes.Add: break;

//                    case (int)SuggestionTypes.Edit:
//                        var node = suggestion.Nodes.FirstOrDefault(m => m.Id == voteViewModel.NodeId);
//                        if (node != null)
//                        {
//                            node.Name = suggestion.Value;
//                            _historyService.AddRecord(sessionId, node.Id, node.Name, suggestion.SuggestedBy.Id, suggestion.Id);
//                        }

//                        break;
//                    case (int)SuggestionTypes.Remove:
//                        RemoveNodeFromSuggestion(voteViewModel.NodeId, sessionId);
//                        break;
//                }
//            }


//            if (voteFinished == VoteResultTypes.Down)
//            {
//                suggestion.Status = (int)SuggestionStatus.Declined;
//                if (suggestion.Type == (int)SuggestionTypes.Add)
//                {
//                    RemoveNodeFromSuggestion(voteViewModel.NodeId, sessionId);

//                }
//            }

//            return _db.Save();
//        }

//        private void RemoveNodeFromSuggestion(int nodeId, int sessionId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            var nodeToRemove =
//                session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
//            if (nodeToRemove != null)
//                session.NodesSuggestions.Remove(nodeToRemove);
//        }

//        private VoteResultTypes CheckVoteFinished(ICollection<Vote> votes, int sessionId)
//        {
//            var sessionUsers = _db.KnowledgeSessions.Get(sessionId).Users.Count;

//            var votesUp = votes.Where(m => m.Type == (int)VoteTypes.Up);
//            var votesDown = votes.Where(m => m.Type == (int)VoteTypes.Down);

//            var result =_voteFinishHelper.CheckSuggestionVoteFinished(votesUp.Count(), votesDown.Count(), sessionUsers);

//            return result;
//        }

//        private Suggestion GetCurrentSuggestion(int nodeId, int sessionId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);

//            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);

//            if (node == null) return null;

//            var suggestion = node.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

//            if (suggestion == null) return null;

//            return suggestion;
//        }

//        public bool UpdateSuggestionsWithVotes(int sessionId, int? parentId, NodeViewModel winnerNode)
//        {
//            var suggestion = GetCurrentSuggestion(winnerNode.Id, sessionId);
//            if (suggestion == null) return false;


//            var upVotes = suggestion.Votes.Where(m => m.Type == (int)VoteTypes.Up);
//            var downVotes = suggestion.Votes.Where(m => m.Type == (int)VoteTypes.Down);
//            winnerNode.CurrentSuggestion.VotesDown = Mapper.Map<IEnumerable<Vote>, List<VoteViewModel>>(downVotes);
//            winnerNode.CurrentSuggestion.VotesUp = Mapper.Map<IEnumerable<Vote>, List<VoteViewModel>>(upVotes);

//            return true;
//        }

//        public void Dispose()
//        {
//            _db.Dispose();
//        }
//    }
//}
