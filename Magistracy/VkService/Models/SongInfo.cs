using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkService.Models
{
    public class SongInfo
    {
        public string SongPath { get; set; }
        public string SongId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string Lyrics { get; set; }
        public TimeSpan Duration { get; set; }

        //public int Duration { get; set; }
    }
}
