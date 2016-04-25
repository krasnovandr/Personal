using System.Collections.Generic;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface INodeService
    {
        int AddNodeToSession(NodeViewModel node, int sessionId, string getUserId);
        bool SaveSuggestedNodes(List<NodeViewModel> nodes, string getUserId, int sessionId);
        List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level);
        NodeViewModel GetNode(int sessionId, int nodeId);
    }
}