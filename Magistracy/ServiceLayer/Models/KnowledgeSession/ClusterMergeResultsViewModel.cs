using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class ClusterMergeResultsViewModel
    {
        public int Id { get; set; }
        //public ResourceClusterViewModel Cluster { get; set; }
        public int? FirstResourceId { get; set; }
        public int? SecondResourceId { get; set; }
        public NodeResourceViewModel FirstResource { get; set; }
        public NodeResourceViewModel SecondResource { get; set; }
    }
}
