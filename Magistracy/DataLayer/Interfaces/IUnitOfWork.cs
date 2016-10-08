using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<KnowledgeSession> KnowledgeSessions { get; }
        IRepository<NodeStructureSuggestion> NodeStructureSuggestions { get; }
        IRepository<NodeModification> NodeModifications { get; }
        IRepository<NodeModificationVote> NodeModificationVotes { get; }
        IRepository<Comment> Comments { get; }
        IRepository<NodeHistory> NodesHistory { get; }
        IRepository<SessionNode> Nodes { get; }
        IRepository<NodeStructureSuggestionVote> NodeStructureSuggestionsVotes { get; }
        IRepository<NodeResource> NodeResources { get; }

        ExtendedRepository<ApplicationUser> Users { get; }
        void Save();
    }
}
