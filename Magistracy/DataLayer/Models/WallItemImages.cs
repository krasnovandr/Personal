using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class WallItemImages
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int WallItemId { get; set; }
    }

}
