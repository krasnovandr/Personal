using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class WallItemLikeDislike
    {
        [Key]
        public int Id { get; set; }
        public WallItem WallItem { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
        public bool Like { get; set; }
        public bool DisLike { get; set; }
        public DateTime? Date { get; set; }
    }

}
