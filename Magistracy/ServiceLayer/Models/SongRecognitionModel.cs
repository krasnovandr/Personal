using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioNetwork.Models
{
    public class SongRecognitionModel
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string AlbumCoverPath { get; set; }
    }
}
