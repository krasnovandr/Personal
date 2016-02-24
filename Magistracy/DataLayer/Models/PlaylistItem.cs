using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PlaylistItem
    {
        public string PlaylistItemId { get; set; }
        public string SongId { get; set; }
        public DateTime? AddDate { get; set; }
        public string PlaylistId { get; set; }
        public int TrackPos { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
