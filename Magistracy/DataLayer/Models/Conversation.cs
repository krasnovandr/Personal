using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Conversation
    {
        [Key]
        public string ConversationId { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public string PlaylistId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? LastMessageDate { get; set; }
        public string ConversationAvatarFilePath { get; set; }
        public string SongAtThisMoment { get; set; }
        public bool MusicConversation { get; set; }
        public bool IsDialog { get; set; }
        //public Playlist Playlist { get; set; }
        //public virtual ICollection<Playlist> Playlists { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<NotReadMessage> NotReadMessages { get; set; }
    }
}
