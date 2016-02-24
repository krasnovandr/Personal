using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Message
    {
        [Key]
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string FromName { get; set; }
        public string FromAvatarPath { get; set; }
        public string FromId { get; set; }
        public DateTime AddDate { get; set; }
        public string ConversationId { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        //public virtual ICollection<Song> Songs { get; set; }
    }
}
