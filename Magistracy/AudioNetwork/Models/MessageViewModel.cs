using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioNetwork.Models
{
    public class MessageViewModel
    {
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string FromName { get; set; }
        public string FromId { get; set; }
        public string FromAvatarPath { get; set; }

        public DateTime AddDate{ get; set; }

        public string MessageDate
        {
            get
            {
                return this.AddDate.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

        public string ConversationId { get; set; }
        public List<SongViewModel> MessageSongs { get; set; }

    }
}