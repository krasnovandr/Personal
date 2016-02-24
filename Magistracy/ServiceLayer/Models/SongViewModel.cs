using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AudioNetwork.Helpers;

namespace AudioNetwork.Models
{
    public class SongChartModel
    {
        public long x { get; set; }
        public int ListenCount { get; set; }
    }

    public class SongViewModel
    {
        public string SongId { get; set; }

        public string id
        {
            get
            {
                return SongId;
            }
        }
        public DateTime AddDate { get; set; }
        public int BitRate { get; set; }
        public TimeSpan Duration { get; set; }

        public string AddDateFormatted
        {
            get
            {
                return this.AddDate.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

        public string DurationFormatted { get; set; }
        //{
        //    get
        //    {
        //        return Duration.ToString(@"mm\:ss");
        //    }
        //}

        public string Tag { get; set; }
        public string Year { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }


        public string Title { get; set; }
        public string SongPath { get; set; }
        public string url { get { return SongPath; } }
        public string SongAlbumCoverPath { get; set; }

        public string Copyright { get; set; }
        public int DiscCount { get; set; }
        public string Composers { get; set; }
        public string Lyrics { get; set; }
        public int Disc { get; set; }
        public string Performers { get; set; }
        public string FileName { get; set; }
        public int ListenCount { get; set; }
        public DateTime? LastListenDate { get; set; }

        public string LastListenDateFormatted
        {
            get
            {
                if (LastListenDate.HasValue)
                {
                    return this.LastListenDate.Value.ToString("MM/dd/yyyy HH:mm");
                }

                return string.Empty;
            }
        }


        public long LastListenDateJavascript
        {
            get
            {
                if (LastListenDate.HasValue)
                {
                    return this.LastListenDate.Value.ToJavaScriptMilliseconds();
                }

                return default(long);
            }
        }
    }
}