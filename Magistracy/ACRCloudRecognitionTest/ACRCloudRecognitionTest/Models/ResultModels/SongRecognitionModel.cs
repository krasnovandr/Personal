using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicRecognition.Models.ResultModels
{
    public class SongRecognitionModel
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string AlbumCoverPath { get; set; }
    }
}
