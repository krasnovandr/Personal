using System.Collections.Generic;
using System.Configuration;

namespace MusicRecognition.Services
{
    public class RecognitionService
    {

        public string Recognize(string filePath, int startSecond, int audioLengthSeconds)
        {
            var configuration = Configurate();
            var reognizer = new ACRCloudRecognizer(configuration);

            string result = reognizer.RecognizeByFile(filePath, startSecond);

            return result;
        }

        private static Dictionary<string, object> Configurate()
        {
            var accesKey = ConfigurationManager.AppSettings["access_key"];
            var accessSecret = ConfigurationManager.AppSettings["access_secret"];

            var config = new Dictionary<string, object>
            {
                {"host", "ap-southeast-1.api.acrcloud.com"},
                {"access_key", "f958570586f34fb73c685ce1cbfaa805"},
                {"access_secret", "4kMjaDTGYZo5gBW47zqansDKgNptLzHiwpIu76Ry"},
                {"timeout", 10}
            };

            return config;
        }
    }
}
