using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RDotNet;

namespace Test_RNet
{
    public class MyClass
    {
        public string TextName { get; set; }
        public int ClusterNumber { get; set; }
    }

    public class TextMiningApi
    {
        private const string R_Scripts = @"..\..\R_Scripts\";
        public static void DoClustering(List<string> texts)
        {
            var directory = PrepareDirectory().ToString();
            var scriptDirectory = GetScriptPath("Starter.R");

            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();

            engine.Evaluate("mining <- dget(" + MakeParam(scriptDirectory.AbsolutePath) + ")");

            var testResult = engine.Evaluate("mining(" +
                MakeParam(directory) + ',' +
                MakeParam(GetScriptPath(null).AbsolutePath) + ")").AsDataFrame();

            var result = MapPlaneClusteringResult(testResult);

            var groupped = result.GroupBy(m => m.ClusterNumber);
            var items = new List<string>();
            foreach (var group in groupped)
            {
                foreach (var item in group)
                {
                    items.Add(item.TextName);

                }

                var group1 = engine.CreateCharacterVector(items);
            }

           
        }

        private static List<MyClass> MapPlaneClusteringResult(DataFrame testResult)
        {
            var result = new List<MyClass>();

            for (int i = 0; i < testResult.RowCount; ++i)
            {
                result.Add(new MyClass()
                {
                    TextName = testResult.RowNames[i],
                    ClusterNumber = (int)testResult[i, 0]
                });
            }

            return result;
        }


        private static Uri GetScriptPath(string scriptName)
        {
            var scriptFolderPath = Path.GetFullPath(R_Scripts);

            if (string.IsNullOrEmpty(scriptName) == false)
            {
                scriptFolderPath += scriptName;
            }

            var unixDirectoryUri = new Uri(scriptFolderPath);
            return unixDirectoryUri;
        }

        private static string MakeParam(string param)
        {
            return @"" + "'" + param + "'";
        }

        private static Guid PrepareDirectory()
        {
            List<string> texts;
            texts = Texts.ToList();
            var guid = Guid.NewGuid();

            Directory.CreateDirectory(guid.ToString());

            for (int i = 0; i < texts.Count; i++)
            {
                var fileNumber = i + 1;
                var path = guid.ToString() + "/" + fileNumber + ".txt";
                File.WriteAllText(path, texts[i]);
            }

            return guid;
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
