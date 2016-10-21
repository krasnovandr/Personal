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
        private readonly ITextMiningService _textMiningService;
        public TextMiningApiController(
             ITextMiningService textMiningService)
        {
            _textMiningService = textMiningService;
        }

        [HttpGet]
        public ClusterAnalysModel DoClustering(int nodeId)
        {
            return _textMiningService.DoClustering(nodeId);
        }
    }
}
