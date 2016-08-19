using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;

namespace Test_RNet
{
    class Program
    {
        static void Main(string[] args)
        {
            TextMiningApi.DoClustering(null);

            ////var engine = RDotNet.REngine.GetInstanceFromID(Guid.NewGuid().ToString());
            //REngine.SetEnvironmentVariables();
            //REngine engine = REngine.GetInstance();

            //engine.Initialize();
            //engine.Evaluate("times <- dget('E:/Users/Andrei/Desktop/fun.R')");
            //engine.Evaluate("times('E:/Users/Andrei/Desktop/R_Test')");
        }
    }
}
