using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Helpers;
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
                SuggestedBy = _db.Users.Get(suggestionAddViewModel.SuggestedBy),
                Status = TextSuggestionStatus.New
            });

            _db.Save();
        }

        public void EditSuggestion(TextMergeSuggestionEditViewModel suggestionEditViewModel)
        {
            var suggestion = _db.TextMergeSuggestions.Get(suggestionEditViewModel.SuggestionId);
            suggestion.Value = suggestionEditViewModel.Value;

            _db.Save();
        }

        public List<TextMergeSuggestionViewModel> GetSuggestions(int nodeId, int clusterId, string userId)
        {
            var node = _db.Nodes.Get(nodeId);

            var cluster = node.Clusters.FirstOrDefault(m => m.Id == clusterId);


            var result = new List<TextMergeSuggestionViewModel>();


            if (cluster == null) return result;

            foreach (var suggestion in cluster.Suggestions.Where(m => m.Status == TextSuggestionStatus.New))
            {
                var firstResource = _db.NodeResources.Get(suggestion.FirstResourceId);
                var secondResource = _db.NodeResources.Get(suggestion.SecondResourceId);

                var userVote = suggestion.Votes.FirstOrDefault(m => m.VoteBy.Id == userId);
                result.Add(new TextMergeSuggestionViewModel
                {
                    Date = suggestion.Date,
                    SuggestedBy = Mapper.Map<ApplicationUser, SessionUserCompactModel>(suggestion.SuggestedBy),
                    FirstResource = Mapper.Map<NodeResource, NodeResourceViewModel>(firstResource),
                    SecondResource = Mapper.Map<NodeResource, NodeResourceViewModel>(secondResource),
                    Value = suggestion.Value,
                    Id = suggestion.Id,
                    Votes = Mapper.Map<ICollection<TextMergeSuggestionVote>, List<TextMergeSuggestionVoteViewModel>>(suggestion.Votes),
                    UserVote = userVote == null ? default(int?) : userVote.TextMergeSuggestion.Id,
                    Status = suggestion.Status
                });
            }

            return result;
        }

        public int? CheckUserSuggestion(int clusterId, string userId, int firstResource, int secondResource)
        {
            var cluster = _db.ResourceClusters.Get(clusterId);

            var suggestion = cluster.Suggestions.FirstOrDefault(m =>
                m.SuggestedBy.Id == userId
                && m.FirstResourceId == firstResource
                && m.SecondResourceId == secondResource);
            return suggestion == null ? (int?)null : suggestion.Id;
        }

        public bool VoteSuggestion(TextMergeSuggestionVoteViewModel voteViewModel)
        {
            var cluster = _db.ResourceClusters.Get(voteViewModel.ClusterId);
            var alreadySuggested =
                cluster.Suggestions.FirstOrDefault(m => m.Votes.Any(v => v.VoteBy.Id == voteViewModel.VoteBy));
            if (alreadySuggested != null)
            {
                var vote = alreadySuggested.Votes.First(v => v.VoteBy.Id == voteViewModel.VoteBy);
                alreadySuggested.Votes.Remove(vote);
            }

            var suggestion = _db.TextMergeSuggestions.Get(voteViewModel.SuggestionId);


            suggestion.Votes.Add(new TextMergeSuggestionVote
            {
                Date = DateTime.Now,
                VoteBy = _db.Users.Get(voteViewModel.VoteBy)
            });

            _db.Save();
            var voteIsDone = CheckVoteIsDone(voteViewModel.NodeId, voteViewModel.ClusterId);

            if (voteIsDone)
            {
                TextMergeSuggestion winner = cluster.Suggestions.First();//cluster.Suggestions.FirstOrDefault(sugestion => sugestion.Votes.Count);
                var maxVotes = 0;
                foreach (var suggestions in cluster.Suggestions)
                {
                    if (suggestions.Votes.Count > maxVotes)
                    {
                        winner = suggestions;
                        maxVotes = suggestions.Votes.Count;
                    }
                }

                winner.Status = TextSuggestionStatus.Approved;

                var combined = _db.NodeResources.GetAll().Where(m => m.Id == winner.FirstResourceId || m.Id == winner.SecondResourceId);

                foreach (var resource in combined)
                {
                    resource.IsDeleted = true;
                }
                var winnerResource = new NodeResource
                {
                    AddBy = winner.SuggestedBy,
                    Date = DateTime.Now,
                    Cluster = cluster,
                    ResourceRaw = winner.Value,
                    Resource = winner.Value.StripHtml(),
                    Node = _db.Nodes.Get(voteViewModel.NodeId),
                    TextName = string.Join("-", combined.Select(m => m.TextName))
                };

                var merged = cluster.MergeResults.FirstOrDefault(
                    m => m.FirstResourceId == winner.FirstResourceId && m.SecondResourceId == winner.SecondResourceId);
                cluster.MergeResults.Remove(merged);

                var next = cluster.MergeResults.FirstOrDefault();

                _db.NodeResources.Create(winnerResource);
                _db.Save();
                if (next != null)
                {
                    if (next.FirstResourceId.HasValue == false)
                    {
                        next.FirstResourceId = winnerResource.Id;
                    }

                    if (next.SecondResourceId.HasValue == false)
                    {
                        next.SecondResourceId = winnerResource.Id;
                    }
                }
                _db.Save();
            }

            return voteIsDone;
        }


        public bool CheckVoteIsDone(int nodeId, int clusterId)
        {
            var node = _db.Nodes.Get(nodeId);
            var usersCount = _db.KnowledgeSessions.Get(node.Session.Id).Users.Count;

            var cluster = node.Clusters.FirstOrDefault(m => m.Id == clusterId);

            if (cluster == null) return false;

            var maximumVotes = cluster.Suggestions.Select(sugestion => sugestion.Votes.Count).Max();

            return _voteFinishHelper.CheckTextSuggestionVoteComplete(usersCount, maximumVotes);
        }
    }
}
