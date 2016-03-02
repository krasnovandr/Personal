using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using JetBrains.Annotations;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IKnowledgeSessionService
    {
        int CreateSession(KnowledgeSessionViewModel knowledgeSession, string userId);
        bool AddUserToSession(ApplicationIdentity user);
        void Dispose();
        void AddmembersToSession(List<ApplicationUser> members, int sessionId);
        int AddNodeToSession(NodeViewModel node, int sessionId, string userId);
        KnowledgeSessionViewModel GetSession(int sessionId);
        List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level);
        List<KnowledgeSessionViewModel> GetUserSessions(string userId);
    }
}
