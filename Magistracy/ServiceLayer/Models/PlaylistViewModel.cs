using System;

namespace ServiceLayer.Models
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