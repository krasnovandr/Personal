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
        KnowledgeSessionViewModel GetSession(int sessionId);
        NodeViewModel GetSessionRoot(int sessionId);
        void Dispose();

    }
}
