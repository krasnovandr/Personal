using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public List<NodeResourceViewModel> Resources { get; set; }

        public List<NodeResourceViewModel> ActiveResources
        {
            get { return Resources.Where(m => m.IsDeleted == false).ToList(); }
        }

        public List<ClusterMergeResultsViewModel> MergeResults { get; set; }
    }
}
