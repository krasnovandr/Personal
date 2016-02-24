using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{

    public class MessageSongs
    {
        [Key]
        public int Id { get; set; }
        public string SongId { get; set; }
        public string MessageId { get; set; }
    }
}
