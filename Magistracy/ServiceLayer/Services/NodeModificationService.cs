using System;
using System.Linq;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Services
{
    public class NodeModificationService : INodeModificationService
    {

        private readonly IUnitOfWork _db;
        private readonly IVoteFinishHelper _voteFinishHelper;

        public NodeModificationService(IUnitOfWork db, IVoteFinishHelper voteFinishHelper)
        {
            _db = db;
            _voteFinishHelper = voteFinishHelper;
        }

        public void CreateNodeModificationSuggestion(NodeModificationViewModel nodeModificationViewModel)
        {
            nodeModificationViewModel.Date = DateTime.Now;
            SessionNode newNode = null;
            if (nodeModificationViewModel.NodeId.HasValue == false)
            {
                var suggestion = _db.NodeStructureSuggestions.Get(nodeModificationViewModel.SuggestionId);
                var parentNode = _db.Nodes.Get(suggestion.ParentId ?? 0);
                newNode = new SessionNode
                {
                    Date = DateTime.Now,
                    Session = parentNode.Session,
                    Name = nodeModificationViewModel.Value,
                    Type = NodeType.Suggested,
                    SuggestedBy = _db.Users.Get(nodeModificationViewModel.SuggestedBy),
                    ParentId = suggestion.ParentId,
                    NodeStructureSuggestion = suggestion,
                };
                _db.Nodes.Create(newNode);
            }

            var nodeModification = Mapper.Map<NodeModificationViewModel, NodeModification>(nodeModificationViewModel);
            nodeModification.Node = _db.Nodes.Get(nodeModificationViewModel.NodeId.HasValue
                ? nodeModificationViewModel.NodeId.Value : newNode.Id);
            nodeModification.SuggestedBy = _db.Users.Get(nodeModificationViewModel.SuggestedBy);
            _db.NodeModifications.Create(nodeModification);


            _db.Save();

        }

        public void VoteNodeModificationSuggestion(NodeModificationVoteViewModel voteViewModel)
        {
            voteViewModel.Date = DateTime.Now;
            var existingNode = _db.NodeModificationVotes.GetAll()
                .FirstOrDefault(m => m.VoteBy.Id == voteViewModel.VoteBy && m.NodeModification.Id == voteViewModel.NodeModificationId);

            if (existingNode != null)
            {
                existingNode.Type = voteViewModel.Type;
            }
            else
            {
                var nodeModification = Mapper.Map<NodeModificationVoteViewModel, NodeModificationVote>(voteViewModel);
                nodeModification.VoteBy = _db.Users.Get(voteViewModel.VoteBy);
                nodeModification.NodeModification =
                    _db.NodeModifications.Get(voteViewModel.NodeModificationId);
                _db.NodeModificationVotes.Create(nodeModification);
            }

            var modifcationSuggestion = _db.NodeModifications.Get(voteViewModel.NodeModificationId);

            var usersCount = _db.Nodes.Get(modifcationSuggestion.Node.Id).Session.Users.Count;
            var votesUp = modifcationSuggestion.Votes.Count(m => m.Type == VoteTypes.Approve);
            var votesDown = modifcationSuggestion.Votes.Count(m => m.Type == VoteTypes.Reject);
            var voteResult = _voteFinishHelper.CheckModificationVoteFinished(votesUp, votesDown, usersCount);

            switch (voteResult)
            {
                case VoteResultTypes.Up:
                    modifcationSuggestion.Status = ModificationStatus.Accepted;


                    if (modifcationSuggestion.Type == ModificationType.Edit)
                    {
                        var nodeToModify = _db.Nodes.Get(modifcationSuggestion.Node.Id);
                        nodeToModify.Name = modifcationSuggestion.Value;
                    }

                    if (modifcationSuggestion.Type == ModificationType.Remove)
                    {
                        _db.Nodes.Delete(modifcationSuggestion.Node.Id);
                    }
                    break;
                case VoteResultTypes.Down:
                    modifcationSuggestion.Status = ModificationStatus.Declined;
                    break;
            }
            _db.Save();
        }
    }
}
