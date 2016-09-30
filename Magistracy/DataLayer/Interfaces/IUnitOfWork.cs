using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<KnowledgeSession> KnowledgeSessions { get; }
        IRepository<NodeStructureSuggestion> NodeStructureSuggestions { get; }
        IRepository<NodeHistory> NodesHistory { get; }
        IRepository<SessionNode> Nodes { get; }
        IRepository<NodeStructureSuggestionVote> LevelVotes { get; }
        ExtendedRepository<ApplicationUser> Users { get; }
        void Save();
    }
}
