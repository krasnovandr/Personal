using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface INodeModificationService
    {
        void CreateNodeModificationSuggestion(NodeModificationViewModel nodeModificationViewModel);
        void VoteNodeModificationSuggestion(NodeModificationVoteViewModel voteViewModel);
    }
}
