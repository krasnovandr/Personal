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
    public class SuggestionService : ISuggestionService
    {
        private readonly IUnitOfWork _db;
        private readonly IVoteFinishHelper _voteFinishHelper;

        public SuggestionService(
            IUnitOfWork db,
            IVoteFinishHelper voteFinishHelper)
        {
            _db = db;
            _voteFinishHelper = voteFinishHelper;
        }


        public int CreateSuggestion(string userId, int parentId)
        {
            var nodeStructureSuggestion = new NodeStructureSuggestion
            {
                Date = DateTime.Now,
                SuggestedBy = _db.Users.Get(userId),
                ParentId = parentId
            };
            _db.NodeStructureSuggestions.Create(nodeStructureSuggestion);

            _db.Save();

            return nodeStructureSuggestion.Id;
        }

        public int? CheckUserStructureSuggestionVote(string userId, int nodeId)
        {
            var suggestionVotes = _db.NodeStructureSuggestionsVotes.GetAll();

            var vote = suggestionVotes.FirstOrDefault(m => m.VoteBy.Id == userId && m.SessionNode.Id == nodeId);

            return vote != null ? vote.Suggestion.Id : (int?)null;
        }

        public bool CheckStructureSuggestionVoteDone(int sessionId, int nodeId, NodeStructureVoteTypes voteType)
        {
            var suggestion = _db.NodeStructureSuggestionsVotes.GetAll().Where(m => m.SessionNode.Id == nodeId && m.VoteType == voteType);
            var session = _db.KnowledgeSessions.Get(sessionId);

            return suggestion.Count() == session.Users.Count;
        }

        public void VoteNodeStructureSuggestion(NodeStructureSuggestionVoteViewModel suggestionViewModel)
        {
            var suggestion = _db.NodeStructureSuggestions.Get(suggestionViewModel.SuggestionId);
            var suggestionVote = new NodeStructureSuggestionVote
            {
                Date = DateTime.Now,
                Suggestion = suggestion,
                VoteBy = _db.Users.Get(suggestionViewModel.VoteBy),
                VoteType = suggestionViewModel.VoteType,
                SessionNode = _db.Nodes.Get(suggestion.ParentId ?? 0)
            };


            _db.NodeStructureSuggestionsVotes.Create(suggestionVote);

            var isDone = CheckStructureSuggestionVoteDone(suggestionViewModel.SessionId, suggestionViewModel.NodeId, suggestionViewModel.VoteType);

            if (isDone)
            {
                var parentNode = _db.Nodes.Get(suggestionViewModel.NodeId);
                switch (suggestionViewModel.VoteType)
                {
                    case NodeStructureVoteTypes.Initialize:
                        parentNode.State = NodeStates.StructureSuggestionWinner;
                        break;
                    case NodeStructureVoteTypes.DoneLeaf:
                        parentNode.State = NodeStates.WinAndNotLeaf;

                        foreach (var nodeInSuggestion in suggestion.Nodes)
                        {
                            nodeInSuggestion.State = NodeStates.Leaf;
                            nodeInSuggestion.Type = NodeType.Configurator;
                        }

                        break;

                    case NodeStructureVoteTypes.DoneContinue:
                        parentNode.State = NodeStates.WinAndNotLeaf;

                        foreach (var nodeInSuggestion in suggestion.Nodes)
                        {
                            nodeInSuggestion.State = NodeStates.StructureSuggestion;
                            nodeInSuggestion.Type = NodeType.Configurator;
                        }

                        break;
                }
            }

            _db.Save();
        }

        //user-> suggestion-> nodes


        public List<SuggestionSessionUserViewModel> GetSuggestions(int sessionId, int nodeId)
        {
            var users = _db.KnowledgeSessions.Get(sessionId).Users;
            var members = Mapper.Map<ICollection<ApplicationUser>, List<SessionUserViewModel>>(users);
            var suggestions = _db.NodeStructureSuggestions.GetAll().ToList();
            var result = new List<SuggestionSessionUserViewModel>();

            foreach (var member in members)
            {
                var suggestion = suggestions.FirstOrDefault(m => m.ParentId == nodeId && m.SuggestedBy.Id == member.Id);

                if (suggestion != null)
                {
                    member.NodeStructureSuggestion =
                        Mapper.Map<NodeStructureSuggestion, NodeStructureSuggestionViewModel>(suggestion);

                }

                result.Add(new SuggestionSessionUserViewModel()
                {
                    Id = member.Id,
                    AvatarFilePath = member.AvatarFilePath,
                    FirstName = member.FirstName,
                    LastActivity = member.LastActivity,
                    LastName = member.LastName,
                    NodeStructureSuggestion = suggestion != null ?
                    Mapper.Map<NodeStructureSuggestion, NodeStructureSuggestionViewModel>(suggestion) : null,
                    UserName = member.UserName,
                });

            }

            return result;
        }

        public SuggestionSessionUserViewModel GetNodeStructureSuggestionWinner(int nodeId)
        {
            var suggestions = _db.NodeStructureSuggestions.GetAll();

            var winneredSuggestion = suggestions.OrderByDescending(m => m.Votes.Count).FirstOrDefault();

            if (winneredSuggestion == null)
            {
                return null;
            }
            var winner = Mapper.Map<ApplicationUser, SuggestionSessionUserViewModel>(winneredSuggestion.SuggestedBy);
            winner.NodeStructureSuggestion = Mapper.Map<NodeStructureSuggestion, NodeStructureSuggestionViewModel>(winneredSuggestion);
            return winner;
        }


        //public bool MakeNodeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel)
        //{
        //    var session = _db.KnowledgeSessions.Get(nodeSuggestionViewModel.SessionId);
        //    SessionNodes node;

        //    if (nodeSuggestionViewModel.NodeId != null)
        //    {
        //        node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeSuggestionViewModel.NodeId);
        //    }
        //    else
        //    {
        //        node = new SessionNodes
        //        {
        //            Level = nodeSuggestionViewModel.Level,
        //            Name = nodeSuggestionViewModel.Suggestion,
        //            DateCreation = DateTime.Now,
        //            SuggestedBy = nodeSuggestionViewModel.WinnerId,
        //            ParentId = nodeSuggestionViewModel.ParentId
        //        };
        //        session.NodesSuggestions.Add(node);
        //    }

        //    if (node != null)
        //    {
        //        var suggestion = new Suggestion
        //        {
        //            Type = nodeSuggestionViewModel.Type,
        //            SuggestedBy = _db.Users.Get(nodeSuggestionViewModel.SuggestedBy),
        //            SuggestionDate = DateTime.Now,
        //            Status = (int)SuggestionStatus.Open,
        //            Value = nodeSuggestionViewModel.Suggestion
        //        };

        //        var comment = new Comment
        //        {
        //            CommentBy = _db.Users.Get(nodeSuggestionViewModel.SuggestedBy),
        //            Date = DateTime.Now,
        //            Value = nodeSuggestionViewModel.Comment
        //        };
        //        suggestion.Comments.Add(comment);
        //        node.Suggestions.Add(suggestion);
        //    }

        //    try
        //    {
        //        _db.Save();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public void UpdateNodeWithSuggestions(int sessionId, int? level, NodeViewModel winnerNode)
        //{
        //    var session = _db.KnowledgeSessions.Get(sessionId);
        //    var suggestedNode = session.NodesSuggestions.FirstOrDefault(m => m.Id == winnerNode.Id);
        //    if (suggestedNode == null)
        //    {
        //        return;
        //    }
        //    var currentSuggestion =
        //        suggestedNode.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

        //    if (currentSuggestion != null)
        //    {
        //        winnerNode.CurrentSuggestion = Mapper.Map<Suggestion, SuggestionViewModel>(currentSuggestion);
        //    }

        //    winnerNode.Suggestions =
        //        Mapper.Map<ICollection<Suggestion>, List<SuggestionViewModel>>(suggestedNode.Suggestions);

        //}

        //public bool AddComment(int sessionId, string comment, int nodeId, string userId)
        //{
        //    var session = _db.KnowledgeSessions.Get(sessionId);
        //    var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
        //    if (node == null) return false;

        //    var suggestion = node.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

        //    if (suggestion == null) return false;

        //    suggestion.Comments.Add(new Comment
        //    {
        //        Date = DateTime.Now,
        //        CommentBy = _db.Users.Get(userId),
        //        Value = comment,
        //    });

        //    try
        //    {
        //        _db.Save();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public List<CommentViewModel> GetComments(int sessionId, int nodeId)
        //{
        //    var session = _db.KnowledgeSessions.Get(sessionId);
        //    var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
        //    if (node == null) return null;

        //    var suggestion = node.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

        //    if (suggestion == null) return null;

        //    return Mapper.Map<ICollection<Comment>, List<CommentViewModel>>(suggestion.Comments);
        //}


    }
}
