using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public NodeModificationService(IUnitOfWork db)
        {
            _db = db;
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

        public void VoteNodeModificationSuggestion(NodeModificationVoteViewModel nodeModificationVoteViewModel)
        {
            nodeModificationVoteViewModel.Date = DateTime.Now;
            var nodeModification = Mapper.Map<NodeModificationVoteViewModel, NodeModificationVote>(nodeModificationVoteViewModel);
            nodeModification.VoteBy = _db.Users.Get(nodeModificationVoteViewModel.VoteBy);
            nodeModification.NodeModification =
                _db.NodeModifications.Get(nodeModificationVoteViewModel.NodeModificationId);
            _db.NodeModificationVotes.Create(nodeModification);

            _db.Save();
        }

        public void GetNodeModificationSuggestions(int nodeId)
        {

        }
    }
}
