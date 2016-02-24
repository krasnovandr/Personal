using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserConversations
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public DateTime AddDate { get; set; }
    }

}
