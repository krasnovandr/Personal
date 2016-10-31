using System.Data.Entity;
using DataLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.EF
{
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



        public DbSet<KnowledgeSession> KnowledgeSessions { get; set; }
        public DbSet<SessionNode> Nodes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NodeModification> NodeModifications { get; set; }
        public DbSet<NodeModificationVote> NodeModificationVotes { get; set; }
        public DbSet<NodeStructureSuggestionVote> NodeStructureVotes { get; set; }
        public DbSet<NodeStructureSuggestion> NodeStructureSuggestion { get; set; }
        public DbSet<NodeHistory> NodeHistory { get; set; }
        public DbSet<NodeResource> NodeResources { get; set; }
        public DbSet<ResourceCluster> ResourceClusters { get; set; }
        public DbSet<TextMergeSuggestion> TextMergeSuggestions { get; set; }
        public DbSet<TextMergeSuggestionVote> TextMergeSuggestionVotes { get; set; }
        

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
