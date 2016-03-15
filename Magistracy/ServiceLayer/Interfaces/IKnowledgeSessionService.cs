using System;
using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

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
        bool SaveSuggestedNodes(List<NodeViewModel> nodes, string userId, int sessionId);
        List<UserViewModel> GetMembers(int sessionId, int? level);
        List<UserViewModel> GetOrderedMembers(int sessionId, int? level);
        bool CheckUserSuggestion(int sessionId, string userid, int? level);
        UserViewModel GetWinner(int sessionId, int? level);
    }
}
