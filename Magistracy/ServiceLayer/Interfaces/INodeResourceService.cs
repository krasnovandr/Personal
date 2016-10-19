using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models.KnowledgeSession;
using Shared;

namespace ServiceLayer.Interfaces
{
    public interface INodeResourceService
    {
        void AddResourceToNode(NodeResourceViewModel resourceViewModel);
        List<NodeResourceViewModel> GetNodeResources(int nodeId);
    }
}
