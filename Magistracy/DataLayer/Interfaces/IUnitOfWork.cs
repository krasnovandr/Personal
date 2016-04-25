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
        IRepository<NodeHistory> NodesHistory { get; }
        IRepository<LevelVote> LevelVotes { get; }
        ExtendedRepository<ApplicationUser> Users { get; }
        bool Save();
    }
}
