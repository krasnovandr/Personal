﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Services
{
    public class SessionSuggestionService : ISessionSuggestionService
    {
        private readonly IUnitOfWork _db;

        public SessionSuggestionService(IUnitOfWork db)
        {
            _db = db;
        }

        public bool MakeNodeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel)
        {
            var session = _db.KnowledgeSessions.Get(nodeSuggestionViewModel.SessionId);
            SessionNodeSuggestions node;

            if (nodeSuggestionViewModel.Node != null)
            {
                node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeSuggestionViewModel.Node.Id);
            }
            else
            {
                node = new SessionNodeSuggestions
                {
                    Level = nodeSuggestionViewModel.Level,
                    Name = nodeSuggestionViewModel.Suggestion,
                    DateCreation = DateTime.Now,
                    SuggestedBy = nodeSuggestionViewModel.WinnerId,

                };
                session.NodesSuggestions.Add(node);
            }

            if (node != null)
            {
                var suggestion = new Suggestion
                {
                    Type = nodeSuggestionViewModel.Type,
                    SuggestedBy = _db.Users.Get(nodeSuggestionViewModel.SuggestedBy),
                    SuggestionDate = DateTime.Now,
                    Status = (int)SuggestionStatus.Open,
                    Value = nodeSuggestionViewModel.Suggestion
                };

                var comment = new Comment
                {
                    CommentBy = _db.Users.Get(nodeSuggestionViewModel.SuggestedBy),
                    Date = DateTime.Now,
                    Value = nodeSuggestionViewModel.Comment
                };
                suggestion.Comments.Add(comment);
                node.Suggestions.Add(suggestion);
            }

            try
            {
                _db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateNodeWithSuggestions(int sessionId, int? level, NodeViewModel winnerNode)
        {
            var session = _db.KnowledgeSessions.Get(sessionId);
            var suggestedNode = session.NodesSuggestions.FirstOrDefault(m => m.Id == winnerNode.Id);
            if (suggestedNode == null)
            {
                return;
            }
            var currentSuggestion =
                suggestedNode.Suggestions.FirstOrDefault(m => m.Status == (int)SuggestionStatus.Open);

            if (currentSuggestion != null)
            {
                winnerNode.CurrentSuggestion = Mapper.Map<Suggestion, SuggestionViewModel>(currentSuggestion);
            }

            winnerNode.Suggestions =
                Mapper.Map<ICollection<Suggestion>, List<SuggestionViewModel>>(suggestedNode.Suggestions);

        }


    }
}