using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class WallItemsSongs
    {
        [Key]
        public int Id { get; set; }
        public string SongId { get; set; }
        public int WallItemId { get; set; }
    }
}
