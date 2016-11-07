
using DataLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface INodeService
    {
        //int AddNodeToSession(NodeViewModel node, int sessionId, string getUserId);
        void SaveSuggestedNodes(SuggestedNodesViewModel model, string userId);
        //List<NodeViewModel> GetSessionNodeByLevel(int sessionId, int level);
        NodeViewModel GetNode(int nodeId);
        NodeStates GetNodeState(string userId, int nodeId);
        NodeViewModel GetNodeHistory(int nodeId);
    }
}