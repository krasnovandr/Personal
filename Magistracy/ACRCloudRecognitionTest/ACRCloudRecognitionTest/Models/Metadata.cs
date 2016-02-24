using System.Collections.Generic;

namespace MusicRecognition.Models
{
    public class Metadata
    {
        public List<Music> music { get; set; }
        public string timestamp_utc { get; set; }
    }
}
