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
            //   this.Friends = new List<Friend>();
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
    }


    public class Song
    {
        public string SongId { get; set; }
        public string Tag { get; set; }
        public string Year { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public string AlbumAndTrackInfo { get; set; }

        public DateTime? AddDate { get; set; }
        public string AddBy { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }

        public int BitRate { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Title { get; set; }
        public string SongPath { get; set; }
        public string SongAlbumCoverPath { get; set; }
        public int ListenCount { get; set; }
        public string Copyright { get; set; }
        public int DiscCount { get; set; }
        public string Composers { get; set; }
        public string Lyrics { get; set; }
        public int Disc { get; set; }
        public string Performers { get; set; }
        public string FileName { get; set; }
        //public virtual ICollection<Playlist> Playlist { get; set; }
        // public virtual ICollection<Message> Messages { get; set; }
    }

    public class Playlist
    {

        public Playlist()
        {
            //PlaylistItem = new List<PlaylistItem>();
        }
        public string PlaylistId { get; set; }
        public string PlayListName { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public bool DefaultPlaylist { get; set; }
        //public virtual ICollection<PlaylistItem> PlaylistItem { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        //public virtual ICollection<Conversation> Conversations { get; set; }
    }

    public class WallItem
    {
        public int WallItemId { get; set; }
        public string AddByUserId { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public string Header { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }

    public class WallItemLikeDislike
    {
        [Key]
        public int Id { get; set; }
        public WallItem WallItem { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
        public bool Like { get; set; }
        public bool DisLike { get; set; }
        public DateTime? Date { get; set; }
    }

    public class PlaylistItem
    {
        public string PlaylistItemId { get; set; }
        public string SongId { get; set; }
        public DateTime? AddDate { get; set; }
        public string PlaylistId { get; set; }
        public int TrackPos { get; set; }
        public virtual Playlist Playlist { get; set; }
    }

    public class Friend
    {
        [Key]
        public string RecordId { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool Confirmed { get; set; }
        public DateTime AddDate { get; set; }
    }

    public class NotReadMessage
    {
        [Key]
        public string RecordId { get; set; }
        public string IdUser { get; set; }
        public string MessageId { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
    }

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

    public class UserConversations
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public DateTime AddDate { get; set; }
    }


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

    public class ListenedSong
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SongId { get; set; }
        public DateTime ListenDate { get; set; }
    }

    public class WallItemsSongs
    {
        [Key]
        public int Id { get; set; }
        public string SongId { get; set; }
        public int WallItemId { get; set; }
    }


    public class WallItemImages
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int WallItemId { get; set; }
    }

    public class MessageSongs
    {
        [Key]
        public int Id { get; set; }
        public string SongId { get; set; }
        public string MessageId { get; set; }
    }





    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyNewContext", false)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<PlaylistItem> PlaylistItem { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<NotReadMessage> NotReadMessages { get; set; }
        public DbSet<ListenedSong> ListenedSong { get; set; }
        public DbSet<WallItem> WallItems { get; set; }
        public DbSet<WallItemsSongs> WallItemsSongs { get; set; }
        public DbSet<UserConversations> UserConversations { get; set; }
        public DbSet<MessageSongs> MessageSongs { get; set; }
        public DbSet<WallItemImages> WallItemImages { get; set; }
        public DbSet<WallItemLikeDislike> WallItemLikeDislike { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Songs)
            //    .WithMany(s => s.Users)
            //    .Map(t => t.MapLeftKey("Id")
            //    .MapRightKey("SongId")
            //    .ToTable("UsersSongs"));

            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Conversations)
            //.WithMany(s => s.Users)
            //.Map(t => t.MapLeftKey("UsersId")
            //.MapRightKey("ConversationId")
            //.ToTable("UsersConversation"));

            modelBuilder.Entity<Conversation>().HasMany(c => c.NotReadMessages)
            .WithMany(s => s.Conversations)
            .Map(t => t.MapLeftKey("ConversationId")
            .MapRightKey("NotReadMessageId")
            .ToTable("ConversationNotReadMessages"));

            //modelBuilder.Entity<Conversation>().HasMany(c => c.Playlists)
            //.WithMany(s => s.Conversations)
            //.Map(t => t.MapLeftKey("ConversationId")
            //.MapRightKey("PlaylistId")
            //.ToTable("ConversationPlaylists"));

            modelBuilder.Entity<Message>().HasMany(c => c.Conversations)
            .WithMany(s => s.Messages)
            .Map(t => t.MapLeftKey("ConversationId")
            .MapRightKey("MessageId")
            .ToTable("ConversationMessages"));

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Playlist)
            .WithMany(s => s.Users)
            .Map(t => t.MapLeftKey("UserId")
            .MapRightKey("PlaylistId")
            .ToTable("UsersPlaylists"));

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.WallItems)
            .WithMany(s => s.Users)
            .Map(t => t.MapLeftKey("UserId")
            .MapRightKey("WallItemId")
            .ToTable("UsersWallItems"));

            //modelBuilder.Entity<WallItem>().HasMany(c => c.WallItemSongs)
            //.WithMany(s => s.WallItems)
            //.Map(t => t.MapLeftKey("SongId")
            //.MapRightKey("WallItemId")
            //.ToTable("WallItemsSongs"));

            //modelBuilder.Entity<Message>().HasMany(c => c.Songs)
            //.WithMany(s => s.Messages)
            //.Map(t => t.MapLeftKey("SongId")
            //.MapRightKey("MessageId")
            //.ToTable("MessageSongs"));

            base.OnModelCreating(modelBuilder);
        }
    }
}