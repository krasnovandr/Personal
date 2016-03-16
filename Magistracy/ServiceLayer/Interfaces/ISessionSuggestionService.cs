using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ISessionSuggestionService
    {
        bool MakeNodeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel);
        void UpdateNodeWithSuggestions(int sessionId, int? level, NodeViewModel winnerNode);
    }
}