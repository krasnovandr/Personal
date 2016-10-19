using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;
using Shared;
using TextMining;

namespace AudioNetwork.Web.API
{
    public class TextMiningApiController : ApiController
    {
        private readonly ITextMiningApi _textMiningApi;
        private readonly INodeResourceService _nodeResourceService;
        public TextMiningApiController(
            ITextMiningApi textMiningApi,
            INodeResourceService nodeResourceService)
        {
            _textMiningApi = textMiningApi;
            _nodeResourceService = nodeResourceService;
        }

        [HttpGet]
        public ClusterAnalysModel DoClustering(int nodeId)
        {
            var resources = _nodeResourceService.GetNodeResources(nodeId);
           return _textMiningApi.DoClustering(resources);
        }
    }
}
