using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MusicRecognition.Interfaces;
using MusicRecognition.Models;
using MusicRecognition.Models.ResultModels;
using Newtonsoft.Json;

namespace MusicRecognition.Services
{
    public class RecognitionService : IRecognitionService
    {
        public RecognizeResult Recognise(string filePath, int startSecond, int audioLengthSeconds)
        {
            var configuration = Configurate();
            var reognizer = new ACRCloudRecognizer(configuration);

            string result = reognizer.RecognizeByFile(filePath, startSecond);

            //var resultStringFormat = Recognize(filePath, 0, audioLengthSeconds);

            var resultJson = JsonConvert.DeserializeObject<RecognizeResult>(result);
            if (resultJson.status.msg == "Success")
            {
                return resultJson;
                //var songData = resultJson.metadata.music.FirstOrDefault();
                //if (songData != null)
                //{
                //    var artist = songData.artists.FirstOrDefault();
                //    string artistName = null;
                //    if (artist != null)
                //    {
                //        artistName = artist.Name;
                //    }
                //    var pictureInfo = SongPictureGetter.CheckContent(artistName, songData.album.Name, songData.title);
                //    model.Artist = artistName;
                //    model.Album = songData.album.Name;
                //    if (pictureInfo != null)
                //    {
                //        model.AlbumCoverPath = pictureInfo.PicturePath;
                //    }
                //    model.Title = songData.title;
            }
            return null;
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
