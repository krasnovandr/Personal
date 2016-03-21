using System.Collections.Generic;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface IHistoryService
    {
        List<NodeHistoryViewModel> Get(int sessionId, int nodeId);
        bool AddRecord(int sesionId, int nodeId, string name, string userId, int? suggestionId);
        void UpdateHistoryWithWinner(int sessionId, int level, string winnerId);
    }
}