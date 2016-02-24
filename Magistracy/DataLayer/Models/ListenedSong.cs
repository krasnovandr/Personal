using System;

namespace DataLayer.Models
{
    public class ListenedSong
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SongId { get; set; }
        public DateTime ListenDate { get; set; }
    }
}
