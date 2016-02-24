using System;
using System.Collections.Generic;
using DataLayer.Models;
using TagLib.Riff;

namespace AudioNetwork.Models
{
    public class ConversationViewModel
    {
        public string ConversationId { get; set; }
        public string Name { get; set; }
        public string FriendId { get; set; }
        public DateTime AddDate { get; set; }
        public int NotReadCount { get; set; }
        public bool MusicConversation { get; set; }
        public SongViewModel CurrentSong { get; set; }
        public string CreatorId { get; set; }
        public bool MyConversation { get; set; }
        public bool IsDialog { get; set; }
        public string ConversationAvatarFilePath { get; set; }  
        public DateTime? LastMessageDate { get; set; }
        public string LastMessageDateWithFormat
        {
            get
            {
                if (LastMessageDate.HasValue)
                {
                    return LastMessageDate.Value.ToString("MM/dd/yyyy HH:mm");
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }
        public List<UserViewModel> ConversationUsers { get; set; }
        public List<SongViewModel> ConversationSongs { get; set; }
        public string PlaylistId { get; set; }
        //public virtual ICollection<Message> Messages { get; set; }
    }


    public enum ConversationType
    {
        All = 1,
        Conversation = 2,
        Dialog = 3,
        MusicConversation = 4,
    }
}