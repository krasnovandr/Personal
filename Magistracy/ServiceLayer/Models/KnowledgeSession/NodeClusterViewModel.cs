using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeClusterViewModel
    {
        public string ClusterImagePath { get; set; }
        public string WordCloudImagePath { get; set; }
        public List<ResourceClusterViewModel> Clusters { get; set; }
    }
}
