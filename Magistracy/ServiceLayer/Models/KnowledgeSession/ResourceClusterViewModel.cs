using System;
using System.Collections.Generic;
using DataLayer.Models;
using Shared;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class ResourceClusterViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public SessionNode Node { get; set; }
        public int ClusterNumber { get; set; }
        public string HierarchicalClusteringPath { get; set; }

        public List<NodeResourceViewModel> Resources { get; set; }
        public List<ClusterMergeResultsViewModel> MergeResults { get; set; }
    }
}
