using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using RDotNet;
using Shared;
using TextMining.Models;

namespace TextMining
{
    public class ClusterModel
    {
        //public string Resource { get; set; }
        public string ByUser { get; set; }
        public string HierarchicalClusteringPath { get; set; }
        public string TextName { get; set; }
        public int ResourceId { get; set; }
    }

    public class ClusterItem
    {
        public List<ClusterModel> Clusters { get; set; }
        public List<MergeModel> MergeResults { get; set; }
    }

    public class ClusterAnalysModel
    {
        public int NodeId { get; set; }
        public string WordCloudRelativePath { get; set; }
        public string PlaneClusteringRelativePath { get; set; }
        public List<ClusterItem> ClusterItems { get; set; }
    }

    public class TextMiningApi : ITextMiningApi
    {
        private readonly REngine _engine;
        private const string R_Scripts = @"TextMining\R_Scripts\";
        private const string ResultDirectory = @"AudioNetwork\Content\TextMining";
        private const string ContentDirectory = @"Content\TextMining";
        private const string HierarchicalClusteringImageName = "HierarchicalClustering.png";
        private const string PlaneClusteringImageName = "PlaneClustering.png";
        private const string WordCloudImageName = "wordcloud.png";
        private readonly Dictionary<string, NodeResourceViewModel> _userTextMapping = new Dictionary<string, NodeResourceViewModel>();
        public TextMiningApi()
        {
            REngine.SetEnvironmentVariables();
            _engine = REngine.GetInstance();
            _engine.Initialize();
        }

        private string RootDirectory
        {
            get { return Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName; }
        }

        private string ResultPath
        {
            get { return Path.Combine(RootDirectory, ResultDirectory); }
        }

        public ClusterAnalysModel DoClustering(List<NodeResourceViewModel> nodeResources)
        {
            try
            {
                var resutDirectory = PrepareDirectory(nodeResources);
                var nodeId = nodeResources.First().NodeId;
               var nodeContentDirectory = Path.Combine(ContentDirectory, nodeId.ToString());
                var clusterAnalys = new ClusterAnalysModel
                {
                    NodeId = nodeId,
                    PlaneClusteringRelativePath = Path.Combine(nodeContentDirectory, PlaneClusteringImageName),
                    WordCloudRelativePath = Path.Combine(nodeContentDirectory, WordCloudImageName),
                    ClusterItems = new List<ClusterItem>()
                };

                var starterScript = GetScriptPath("Starter.R");
                var resultDirectoryUnixPath = new Uri(resutDirectory);

                _engine.Evaluate("mining <- dget(" + MakeParam(starterScript.AbsolutePath) + ")");
                var scriptsDirectory = MakeParam(GetScriptPath(null).AbsolutePath);


                var planeClusteringResult = _engine.Evaluate("mining(" +
                                                             MakeParam(resultDirectoryUnixPath.AbsolutePath) + ',' +
                                                             scriptsDirectory + ")").AsDataFrame();

                var result = MapPlaneClusteringResult(planeClusteringResult);

                var groupped = result.GroupBy(m => m.ClusterNumber);

                DoHierarchicalClustering(groupped, resutDirectory, resultDirectoryUnixPath, clusterAnalys, nodeContentDirectory);
                return clusterAnalys;
            }
            catch (Exception exception)
            {
                throw new Exception("Text Mining Exception", exception);
            }
        }

        private void DoHierarchicalClustering(IEnumerable<IGrouping<int, TextClusterModel>> groupped, string resutDirectory, Uri resultDirectoryUnixPath, ClusterAnalysModel clusterAnalys, string nodeContentDirectory)
        {
            var hierarchicalScript = GetScriptPath("HierarchicalClustering.R");
            var items = new List<string>();
            int number = 0;
            foreach (var group in groupped)
            {
                var clusterItem = new ClusterItem
                {
                    Clusters = new List<ClusterModel>()
                };

                foreach (var item in @group)
                {
                    number = item.ClusterNumber;
                    var cluetrDirectory = Path.Combine(resutDirectory, item.ClusterNumber.ToString());
                    if (!Directory.Exists(cluetrDirectory))
                    {
                        Directory.CreateDirectory(cluetrDirectory);
                    }
                    File.Move(Path.Combine(resutDirectory, item.TextName), Path.Combine(cluetrDirectory, item.TextName));

                    items.Add(item.TextName);



                    clusterItem.Clusters.Add(new ClusterModel
                    {
                        TextName = item.TextName,
                        ByUser = _userTextMapping[item.TextName].AddBy,
                        ResourceId = _userTextMapping[item.TextName].Id,
                        //Resource = File.ReadAllText(Path.Combine(cluetrDirectory, item.TextName)),
                        HierarchicalClusteringPath = Path.Combine(Path.Combine(nodeContentDirectory, item.ClusterNumber.ToString()), HierarchicalClusteringImageName)
                    });
                }
               
                //var group1 = _engine.CreateCharacterVector(items);

                if (items.Count() > 2)
                {
                    _engine.Evaluate("hierarchicalMining <- dget(" + MakeParam(hierarchicalScript.AbsolutePath) + ")");
                    var path = resultDirectoryUnixPath.AbsolutePath + Path.AltDirectorySeparatorChar + number;
                    var resultMerge = _engine.Evaluate("hierarchicalMining(" +
                                                      MakeParam(path) + ")").AsDataFrame();

                    clusterItem.MergeResults = MapMergeResult(resultMerge, items);
                }

                clusterAnalys.ClusterItems.Add(clusterItem);

                //Thread.Sleep(5000);
                items.Clear();
            }
        }

        private List<MergeModel> MapMergeResult(DataFrame resultMerge, List<string> items)
        {
            var result = new List<MergeModel>();

            for (int i = 0; i < resultMerge.RowCount; ++i)
            {
                var firstIndex = (int)resultMerge[i, 0];
                var mergeResult = new MergeModel();
                if (firstIndex < 0)
                {
                    firstIndex *= -1;
                    mergeResult.FirstTextName = items.ElementAt(firstIndex - 1);
                }
                else
                {
                    mergeResult.PrevoiusFirst = firstIndex;
                }

                var secondIndex = (int)resultMerge[i, 1];
                if (secondIndex < 0)
                {
                    secondIndex *= -1;
                    mergeResult.SecondTextName = items.ElementAt(secondIndex - 1);
                }
                else
                {
                    mergeResult.PrevoiusSecond = secondIndex;
                }

                result.Add(mergeResult);
            }

            return result;
        }

        private List<TextClusterModel> MapPlaneClusteringResult(DataFrame testResult)
        {
            var result = new List<TextClusterModel>();

            for (int i = 0; i < testResult.RowCount; ++i)
            {
                result.Add(new TextClusterModel()
                {
                    TextName = testResult.RowNames[i],
                    ClusterNumber = (int)testResult[i, 0]
                });
            }

            return result;
        }

        private Uri GetScriptPath(string scriptName)
        {
            var scriptsPath = Path.Combine(RootDirectory, R_Scripts);
            var scriptFolderPath = Path.GetFullPath(scriptsPath);

            if (string.IsNullOrEmpty(scriptName) == false)
            {
                scriptFolderPath += scriptName;
            }

            var unixDirectoryUri = new Uri(scriptFolderPath);
            return unixDirectoryUri;
        }

        private string MakeParam(string param)
        {
            return @"" + "'" + param + "'";
        }

        private string PrepareDirectory(List<NodeResourceViewModel> nodeResources)
        {
            var nodeResource = nodeResources.FirstOrDefault();
            var guid = nodeResource != null ? nodeResource.NodeId : default(int);
            var resutDirectory = Path.Combine(ResultPath, guid.ToString());

            if (Directory.Exists(resutDirectory))
            {
                DeleteDirectory(resutDirectory);
            }
            Directory.CreateDirectory(resutDirectory);
            for (int i = 0; i < nodeResources.Count; i++)
            {
                //todo remove on different users
                var fileNumber = i + 1;
                var fileName = fileNumber + ".txt";
                var path = Path.Combine(resutDirectory, fileName);
                _userTextMapping.Add(fileName, nodeResources[i]);
                Thread.Sleep(100);
                File.WriteAllText(path, nodeResources[i].Resource);
            }

            return resutDirectory;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public static string[] Texts
        {
            get
            {
                return new string[]
                {
                    "This calls the function pam or clara to perform a partitioning around medoids clustering with the number of clusters estimated by optimum average silhouette width (see pam.object) or Calinski-Harabasz index (calinhara). The Duda-Hart test (dudahart2) is applied to decide whether there should be more than one cluster (unless 1 is excluded as number of clusters or data are dissimilarities).",
                    "The pvclust( ) function in the pvclust package provides p-values for hierarchical clustering based on multiscale bootstrap resampling. Clusters that are highly supported by the data will have large p values. Interpretation details are provided Suzuki. Be aware that pvclust clusters columns, not rows. Transpose your data before using.",
                    "integer vector. Numbers of clusters which are to be compared by the average silhouette width criterion. Note: average silhouette width and Calinski-Harabasz can't estimate number of clusters nc=1. If 1 is included, a Duda-Hart test is applied and 1 is estimated if this is not significant.",
                    "No sooner had he fallen to the turf, the wrath of public opinion poured forth.Social media was buzzing -- some laughed, some offered comfort ... some just rolled their eyes.Whatever he does, wherever he goes, few footballers split opinion like Cristiano Ronaldo.",
                    "His petulance -- criticizing Iceland for being 'small-minded' after Portugal failed to beat the football minnow at Euro 2016, then days later throwing a reporter's microphone into a lake -- does not help his cause, either.",
                    "Ronaldo's trademark celebration was on show in Paris.Then there are the endless endorsements which have helped bring in $32 million of the $88 million he has taken home over the past 12 months -- the other $56 million coming courtesy of his salary and bonuses at Real Madrid, according to Forbes.Ronaldo's huge social media profile does little to deter his critics -- he has over 200 million followers across Twitter, Facebook and Instagram.",
                    @"British Prime Minister David Cameron is to resign on Wednesday, paving the way for Home Secretary Theresa May to take the reins.
                        May was officially named Conservative party leader and successor to Cameron 'with immediate effect' Monday, said Graham Brady, chair of the 1922 committee, which is a collection of conservative members of parliament that is key to electing the party leader. She will replace Cameron on Wednesday evening.
                        Cameron had already announced in June that he would step down by October, after failing to convince the country to remain in the European Union in a divisive referendum that has sent shockwaves through Britain's political establishment.
                        But on Monday, May's only remaining rival to replace Cameron -- Energy Minister Andrea Leadsom -- pulled out of the race following controversy over comments she made about motherhood and leadership.
                        'Obviously, with these changes, we now don't need to have a prolonged period of transition. And so tomorrow I will chair my last cabinet meeting. On Wednesday I will attend the House of Commons for prime minister's questions. And then after that I expect to go to the palace and offer my resignation. So we will have a new prime minister in that building behind me by Wednesday evening,' Cameron told reporters outside 10 Downing Street on Monday.",

                    "The vote between May and Leadsom was supposed to go to the wider Conservative Party of 150,000 people, but being the sole candidate, May sidestepped the party rule.Cameron welcomed Leadsom's decision to drop out of the race, and said he was confident that May would steer the country in the right direction, calling her strong and competent, and offering her his full support."
                };
            }
        }
    }
}
