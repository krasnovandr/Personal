using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Friend
    {
        [Key]
        public string RecordId { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool Confirmed { get; set; }
        public DateTime AddDate { get; set; }
    }
}
