using System;
using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface IKnowledgeSessionMemberService
    {
        void AddmembersToSession(List<ApplicationUser> members, int sessionId);
        List<KnowledgeSessionViewModel> GetUserSessions(string userId);
        //List<UserViewModel>  GetMembers(NodeIdentifyModel nodeIdentifyModel);
        //List<UserViewModel> GetOrderedMembers(NodeIdentifyModel nodeIdentifyModel);
        //bool CheckUserSuggestion(NodeIdentifyModel nodeIdentifyModel, string userid);
        //UserViewModel GetWinner(NodeIdentifyModel nodeIdentifyModel);
        void Dispose();
    }
}
