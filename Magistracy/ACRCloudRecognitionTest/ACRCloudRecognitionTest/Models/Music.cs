using System.Collections.Generic;

namespace MusicRecognition.Models
{
    public class Music
    {
        public ExternalIds external_ids { get; set; }
        public int play_offset_ms { get; set; }
        public ExternalMetadata external_metadata { get; set; }
        public string label { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string duration_ms { get; set; }
        public Album album { get; set; }
        public string acrid { get; set; }
        public List<Genre> genres { get; set; }
        public List<Artist> artists { get; set; }
    }
}
