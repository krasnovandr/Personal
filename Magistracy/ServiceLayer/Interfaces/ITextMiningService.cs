using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models.KnowledgeSession;
using TextMining;

namespace ServiceLayer.Interfaces
{
    public interface ITextMiningService
    {
        NodeClusterViewModel DoClustering(int nodeId);
        NodeClusterViewModel GetNodeClusters(int nodeId);
        ResourceClusterViewModel GetMergeData(int nodeId, int clusterId);
    }
}
