using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
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

        //    public ClusterAnalysModel GetClusteringResult(int nodeId)
        //    {
        //        var node = _db.Nodes.Get(nodeId);
        //        node.State = NodeStates.LeafClusteringDone;

        //        if (node.State != NodeStates.LeafClusteringDone)
        //        {
        //            return null;
        //        }


        //return _textMiningApi.GetClusteringResult(nodeId);
        //    }


        public NodeClusterViewModel DoClustering(int nodeId)
        {

            var resources = _nodeResourceService.GetNodeResources(nodeId);
            var result = _textMiningApi.DoClustering(resources);

            var node = _db.Nodes.Get(nodeId);

            //node.State = NodeStates.LeafClusteringDone;
            node.ClusterImagePath = result.PlaneClusteringRelativePath;
            node.WordCloudImagePath = result.WordCloudRelativePath;

            foreach (var cluster in result.Clusters)
            {
                var clusterToAdd = new ResourceCluster
                {
                    ClusterNumber = cluster.ClusterNumber,
                    HierarchicalClusteringPath = cluster.HierarchicalClusteringPath,
                    Date = DateTime.Now,

                };
                foreach (var clusterItem in cluster.ClusterItems)
                {
                    var resource = _db.NodeResources.Get(clusterItem.ResourceId);
                    resource.TextName = clusterItem.TextName;
                    clusterToAdd.Resources.Add(resource);
                }

                if (cluster.MergeResults != null)
                {
                    foreach (var mergeResult in cluster.MergeResults)
                    {
                        clusterToAdd.MergeResults.Add(new ClusterMergeResults()
                        {
                            FirstResourceId = mergeResult.FirstResourceId,
                            SecondResourceId = mergeResult.SecondResourceId
                        });
                    }
                }

                node.Clusters.Add(clusterToAdd);
            }


            _db.Save();


            return GetNodeClusters(nodeId);
        }

        public NodeClusterViewModel GetNodeClusters(int nodeId)
        {
            var result = new NodeClusterViewModel();
            var node = _db.Nodes.Get(nodeId);
            result.ClusterImagePath = node.ClusterImagePath;
            result.WordCloudImagePath = node.WordCloudImagePath;
            result.Clusters = new List<ResourceClusterViewModel>();
            foreach (var cluster in node.Clusters)
            {
                result.Clusters.Add(Mapper.Map<ResourceCluster, ResourceClusterViewModel>(cluster));

            }

            return result;
        }


        public ResourceClusterViewModel GetMergeData(int nodeId, int clusterId)
        {
            var node = _db.Nodes.Get(nodeId);

            var cluster = node.Clusters.FirstOrDefault(m => m.ClusterNumber == clusterId);

            var result = Mapper.Map<ResourceCluster, ResourceClusterViewModel>(cluster);

            return result;
        }
    }
}
