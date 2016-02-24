using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class NotReadMessage
    {
        [Key]
        public string RecordId { get; set; }
        public string IdUser { get; set; }
        public string MessageId { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
