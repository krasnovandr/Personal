using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMining;

namespace ServiceLayer.Interfaces
{
   public interface ITextMiningService
    {
        ClusterAnalysModel DoClustering(int nodeId);
    }
}
