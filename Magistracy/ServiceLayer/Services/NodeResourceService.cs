using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Helpers;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
using Shared;

namespace ServiceLayer.Services
{
    public class NodeResourceService : INodeResourceService
    {
        private readonly IUnitOfWork _db;

        public NodeResourceService(IUnitOfWork db)
        {
            _db = db;
        }


        public void AddResourceToNode(NodeResourceViewModel resourceViewModel)
        {
            resourceViewModel.Date = DateTime.Now;
            var resource = Mapper.Map<NodeResourceViewModel, NodeResource>(resourceViewModel);

            resource.Node = _db.Nodes.Get(resourceViewModel.NodeId);
            resource.AddBy = _db.Users.Get(resourceViewModel.AddBy);
            resource.Resource = resourceViewModel.ResourceRaw.StripHtml();

            _db.NodeResources.Create(resource);
            _db.Save();
        }

        public List<NodeResourceViewModel> GetNodeResources(int nodeId)
        {
            var node = _db.Nodes.Get(nodeId);

            var resources = Mapper.Map<ICollection<NodeResource>, List<NodeResourceViewModel>>(node.NodeResources);

            return resources;
        }


    }
}
