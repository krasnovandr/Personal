using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using TextMining;

namespace ServiceLayer.Services
{
    public class TextMiningService : ITextMiningService
    {
        private readonly IUnitOfWork _db;
        private readonly INodeResourceService _nodeResourceService;
        private readonly ITextMiningApi _textMiningApi;

        public TextMiningService(
            IUnitOfWork db,
            INodeResourceService nodeResourceService,
            ITextMiningApi textMiningApi)
        {
            _db = db;
            _nodeResourceService = nodeResourceService;
            _textMiningApi = textMiningApi;
        }

        public ClusterAnalysModel DoClustering(int nodeId)
        {

            var resources = _nodeResourceService.GetNodeResources(nodeId);
            var result = _textMiningApi.DoClustering(resources);

            var node = _db.Nodes.Get(nodeId);
            foreach (var cluster in result.Clusters)
            {
                foreach (var clusterItem in cluster.ClusterItems)
                {
                    var resource = node.NodeResources.FirstOrDefault(m => m.Id == clusterItem.ResourceId);

                    if (resource != null)
                    {
                        //resource.ClusterNumber = clusterItem.ClusterNumber;
                    }
                }
            }

            return result;
        }
    }
}
