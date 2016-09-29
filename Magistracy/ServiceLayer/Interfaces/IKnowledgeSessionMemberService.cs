using System;
using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Services;

namespace ServiceLayer.Interfaces
{
    public interface IKnowledgeSessionMemberService
    {
        List<KnowledgeSessionViewModel> GetUserSessions(string userId);
        List<SessionUserViewModel> GetMembers(int sessionId);
        //List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel);
        //bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid);
        //UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel);
        void Dispose();
        List<TreeNodeViewModel> GetTree(int sessionId, string userId);
        void AddMembersToSession(AddMembersViewModel addMembersViewModel);
        List<SessionUserViewModel> GetMembersExtended(int sessionId, int nodeId);
    }
}
