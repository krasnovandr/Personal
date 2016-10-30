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
using Shared;

namespace ServiceLayer.Services
{
    public class TextMergeSuggestionService : ITextMergeSuggestionService
    {
        private readonly IUnitOfWork _db;
        private readonly IVoteFinishHelper _voteFinishHelper;

        public TextMergeSuggestionService(
            IUnitOfWork db,
            IVoteFinishHelper voteFinishHelper)
        {
            _db = db;
            _voteFinishHelper = voteFinishHelper;
        }

        public void MakeSuggestion(TextMergeSuggestionAddViewModel suggestionAddViewModel)
        {

            var node = _db.Nodes.Get(suggestionAddViewModel.NodeId);
            _db.TextMergeSuggestions.Create(new TextMergeSuggestion
            {
                Date = DateTime.Now,
                Cluster = node.Clusters.FirstOrDefault(m => m.Id == suggestionAddViewModel.ClusterId),
                Value = suggestionAddViewModel.Value,
                FirstResourceId = suggestionAddViewModel.FirstResourceId,
                SecondResourceId = suggestionAddViewModel.SecondResourceId,
                SuggestedBy = _db.Users.Get(suggestionAddViewModel.SuggestedBy)
            });

            _db.Save();
        }

        public void EditSuggestion(TextMergeSuggestionEditViewModel suggestionEditViewModel)
        {
            var suggestion = _db.TextMergeSuggestions.Get(suggestionEditViewModel.SuggestionId);
            suggestion.Value = suggestionEditViewModel.Value;

            _db.Save();
        }

        public List<TextMergeSuggestionViewModel> GetSuggestions(int nodeId, int clusterId)
        {
            var node = _db.Nodes.Get(nodeId);

            var cluster = node.Clusters.FirstOrDefault(m => m.Id == clusterId);


            var result = new List<TextMergeSuggestionViewModel>();


            if (cluster == null) return result;

            foreach (var suggestion in cluster.Suggestions)
            {
                var firstResource = _db.NodeResources.Get(suggestion.FirstResourceId);
                var secondResource = _db.NodeResources.Get(suggestion.SecondResourceId);
                result.Add(new TextMergeSuggestionViewModel
                {
                    Date = suggestion.Date,
                    SuggestedBy = Mapper.Map<ApplicationUser, SessionUserCompactModel>(suggestion.SuggestedBy),
                    FirstResource = Mapper.Map<NodeResource, NodeResourceViewModel>(firstResource),
                    SecondResource = Mapper.Map<NodeResource, NodeResourceViewModel>(secondResource),
                    Value = suggestion.Value,
                    Id = suggestion.Id
                });
            }

            return result;
        }

        public int? CheckUserSuggestion(int nodeId, int clusterId, string userId)
        {
            var node = _db.Nodes.Get(nodeId);

            var cluster = node.Clusters.FirstOrDefault(m => m.Id == clusterId);

            if (cluster == null) return null;
            
             var suggestion = cluster.Suggestions.FirstOrDefault(m => m.SuggestedBy.Id == userId);
             return suggestion  == null ? (int?) null: suggestion.Id;
        }

        public bool VoteSuggestion(TextMergeSuggestionVoteViewModel voteViewModel)
        {
            var suggestion = _db.TextMergeSuggestions.Get(voteViewModel.SuggestionId);
          
            suggestion.Votes.Add(new TextMergeSuggestionVote
            {
                Date = DateTime.Now,
                VoteBy = _db.Users.Get(voteViewModel.VoteBy)
            });



            _db.Save();

            return CheckVoteIsDone(voteViewModel.NodeId, voteViewModel.ClusterId);
        }


        public bool CheckVoteIsDone(int nodeId, int clusterId)
        {
            var node = _db.Nodes.Get(nodeId);
            var usersCount = _db.KnowledgeSessions.Get(node.Session.Id).Users.Count;

            var cluster = node.Clusters.FirstOrDefault(m => m.Id == clusterId);

            if (cluster == null) return false;

            var maximumVotes = cluster.Suggestions.Select(sugestion => sugestion.Votes.Count).Max();

            return   _voteFinishHelper.CheckTextSuggestionVoteComplete(usersCount,maximumVotes);
        }
    }
}
