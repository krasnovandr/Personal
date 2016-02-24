using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioNetwork.Models
{
    public class PlaylistViewModel
    {
        public string PlaylistId { get; set; }
        public string PlayListName { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public bool DefaultPlaylist { get; set; }
    }
}