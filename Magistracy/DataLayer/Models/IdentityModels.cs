using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            KnowledgeSessions = new HashSet<KnowledgeSession>();
            SessionNodes = new List<SessionNode>();
            Comments = new List<Comment>();
            NodeModifications = new List<NodeModification>();
            NodeModificationVotes = new List<NodeModificationVote>();
            NodeStructureVotes = new List<NodeStructureVote>();

        }
        public DateTime LastActivity { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AvatarFilePath { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string BestGenre { get; set; }
        public string WorstGenre { get; set; }
        public string BestForeignArtist { get; set; }
        public string BestNativeArtist { get; set; }
        public string LastEntrenchedSong { get; set; }
        public string BestVocalist { get; set; }
        public string BestCinemaSoundtrack { get; set; }
        public string BestGameSoundtrack { get; set; }
        public string BestAlarmClock { get; set; }
        public string BestSleeping { get; set; }
        public string BestRelaxSong { get; set; }
        public string SongAtThisMoment { get; set; }



        //public virtual ICollection<Song> Songs { get; set; }
        //public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Playlist> Playlist { get; set; }
        //public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<WallItem> WallItems { get; set; }
        public string VkLogin { get; set; }
        public string VkPassword { get; set; }

        public virtual ICollection<KnowledgeSession> KnowledgeSessions { get; set; }
        public virtual ICollection<SessionNode> SessionNodes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<NodeModification> NodeModifications { get; set; }
        public virtual ICollection<NodeModificationVote> NodeModificationVotes { get; set; }
        public virtual ICollection<NodeStructureVote> NodeStructureVotes { get; set; }
        //public virtual ICollection<KnowledgeSessionRole> KnowledgeSessionRoles { get; set; }
        //public virtual ICollection<NodeStructureVote> LevelVotes { get; set; }
    }
}