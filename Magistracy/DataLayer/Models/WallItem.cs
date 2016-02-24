using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class WallItem
    {
        public int WallItemId { get; set; }
        public string AddByUserId { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public string Header { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
