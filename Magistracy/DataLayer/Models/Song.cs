using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Song
    {
        public string SongId { get; set; }
        public string Tag { get; set; }
        public string Year { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string AlbumAndTrackInfo { get; set; }

        public DateTime? AddDate { get; set; }
        public string AddBy { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }

        public int BitRate { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Title { get; set; }
        public string SongPath { get; set; }
        public string SongAlbumCoverPath { get; set; }
        public int ListenCount { get; set; }
        public string Copyright { get; set; }
        public int DiscCount { get; set; }
        public string Composers { get; set; }
        public string Lyrics { get; set; }
        public int Disc { get; set; }
        public string Performers { get; set; }
        public string FileName { get; set; }
        //public virtual ICollection<Playlist> Playlist { get; set; }
        // public virtual ICollection<Message> Messages { get; set; }
    }
}
