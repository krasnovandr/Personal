using System.Collections.Generic;
using Shared;

namespace TextMining
{
    public interface ITextMiningApi
    {
        ClusterAnalysModel DoClustering(List<NodeResourceViewModel> nodeResources);
    }
}
