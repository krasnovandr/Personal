namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.String(),
                        CommentBy_Id = c.String(maxLength: 128),
                        CommentTo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CommentBy_Id)
                .ForeignKey("dbo.SessionNodes", t => t.CommentTo_Id)
                .Index(t => t.CommentBy_Id)
                .Index(t => t.CommentTo_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastActivity = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(),
                        AvatarFilePath = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        BestGenre = c.String(),
                        WorstGenre = c.String(),
                        BestForeignArtist = c.String(),
                        BestNativeArtist = c.String(),
                        LastEntrenchedSong = c.String(),
                        BestVocalist = c.String(),
                        BestCinemaSoundtrack = c.String(),
                        BestGameSoundtrack = c.String(),
                        BestAlarmClock = c.String(),
                        BestSleeping = c.String(),
                        BestRelaxSong = c.String(),
                        SongAtThisMoment = c.String(),
                        VkLogin = c.String(),
                        VkPassword = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.KnowledgeSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Theme = c.String(),
                        CreatorId = c.String(),
                        SessionState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SessionNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                        Type = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Session_Id = c.Int(),
                        SuggestedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KnowledgeSessions", t => t.Session_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggestedBy_Id)
                .Index(t => t.Session_Id)
                .Index(t => t.SuggestedBy_Id);
            
            CreateTable(
                "dbo.NodeModifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Node_Id = c.Int(),
                        SuggestedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SessionNodes", t => t.Node_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggestedBy_Id)
                .Index(t => t.Node_Id)
                .Index(t => t.SuggestedBy_Id);
            
            CreateTable(
                "dbo.NodeModificationVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NodeModification_Id = c.Int(),
                        VoteBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NodeModifications", t => t.NodeModification_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.VoteBy_Id)
                .Index(t => t.NodeModification_Id)
                .Index(t => t.VoteBy_Id);
            
            CreateTable(
                "dbo.NodeStructureVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Node_Id = c.Int(),
                        VoteBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SessionNodes", t => t.Node_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.VoteBy_Id)
                .Index(t => t.Node_Id)
                .Index(t => t.VoteBy_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                        PlayListName = c.String(),
                        Note = c.String(),
                        AddDate = c.DateTime(),
                        DefaultPlaylist = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlaylistId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.WallItems",
                c => new
                    {
                        WallItemId = c.Int(nullable: false, identity: true),
                        AddByUserId = c.String(),
                        Note = c.String(),
                        AddDate = c.DateTime(),
                        Header = c.String(),
                    })
                .PrimaryKey(t => t.WallItemId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatorId = c.String(),
                        PlaylistId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        LastMessageDate = c.DateTime(),
                        ConversationAvatarFilePath = c.String(),
                        SongAtThisMoment = c.String(),
                        MusicConversation = c.Boolean(nullable: false),
                        IsDialog = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConversationId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        FromName = c.String(),
                        FromAvatarPath = c.String(),
                        FromId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        ConversationId = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "dbo.NotReadMessages",
                c => new
                    {
                        RecordId = c.String(nullable: false, maxLength: 128),
                        IdUser = c.String(),
                        MessageId = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        RecordId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        FriendId = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.ListenedSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        SongId = c.String(),
                        ListenDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.String(),
                        MessageId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NodeHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.String(),
                        SuggestionId = c.Int(),
                        ByUser_Id = c.String(maxLength: 128),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ByUser_Id)
                .ForeignKey("dbo.SessionNodes", t => t.Node_Id)
                .Index(t => t.ByUser_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.PlaylistItems",
                c => new
                    {
                        PlaylistItemId = c.String(nullable: false, maxLength: 128),
                        SongId = c.String(),
                        AddDate = c.DateTime(),
                        PlaylistId = c.String(maxLength: 128),
                        TrackPos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaylistItemId)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId)
                .Index(t => t.PlaylistId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.String(nullable: false, maxLength: 128),
                        Tag = c.String(),
                        Year = c.String(),
                        Album = c.String(),
                        Genre = c.String(),
                        Artist = c.String(),
                        AlbumAndTrackInfo = c.String(),
                        AddDate = c.DateTime(),
                        AddBy = c.String(),
                        BitRate = c.Int(nullable: false),
                        Duration = c.Time(precision: 7),
                        Title = c.String(),
                        SongPath = c.String(),
                        SongAlbumCoverPath = c.String(),
                        ListenCount = c.Int(nullable: false),
                        Copyright = c.String(),
                        DiscCount = c.Int(nullable: false),
                        Composers = c.String(),
                        Lyrics = c.String(),
                        Disc = c.Int(nullable: false),
                        Performers = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.SongId);
            
            CreateTable(
                "dbo.UserConversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConversationId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WallItemImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WallItemLikeDislikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Like = c.Boolean(nullable: false),
                        DisLike = c.Boolean(nullable: false),
                        Date = c.DateTime(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        WallItem_WallItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.WallItems", t => t.WallItem_WallItemId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.WallItem_WallItemId);
            
            CreateTable(
                "dbo.WallItemsSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.String(),
                        WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KnowledgeSessionApplicationUsers",
                c => new
                    {
                        KnowledgeSession_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.KnowledgeSession_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.KnowledgeSessions", t => t.KnowledgeSession_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.KnowledgeSession_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UsersPlaylists",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.PlaylistId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PlaylistId);
            
            CreateTable(
                "dbo.UsersWallItems",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.WallItemId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WallItems", t => t.WallItemId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WallItemId);
            
            CreateTable(
                "dbo.ConversationMessages",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        MessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.MessageId })
                .ForeignKey("dbo.Messages", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.ConversationNotReadMessages",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        NotReadMessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.NotReadMessageId })
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.NotReadMessages", t => t.NotReadMessageId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.NotReadMessageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WallItemLikeDislikes", "WallItem_WallItemId", "dbo.WallItems");
            DropForeignKey("dbo.WallItemLikeDislikes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.NodeHistories", "Node_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.NodeHistories", "ByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConversationNotReadMessages", "NotReadMessageId", "dbo.NotReadMessages");
            DropForeignKey("dbo.ConversationNotReadMessages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.ConversationMessages", "MessageId", "dbo.Conversations");
            DropForeignKey("dbo.ConversationMessages", "ConversationId", "dbo.Messages");
            DropForeignKey("dbo.UsersWallItems", "WallItemId", "dbo.WallItems");
            DropForeignKey("dbo.UsersWallItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.UsersPlaylists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KnowledgeSessionApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KnowledgeSessionApplicationUsers", "KnowledgeSession_Id", "dbo.KnowledgeSessions");
            DropForeignKey("dbo.SessionNodes", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeStructureVotes", "VoteBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeStructureVotes", "Node_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.SessionNodes", "Session_Id", "dbo.KnowledgeSessions");
            DropForeignKey("dbo.NodeModificationVotes", "VoteBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeModificationVotes", "NodeModification_Id", "dbo.NodeModifications");
            DropForeignKey("dbo.NodeModifications", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeModifications", "Node_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.Comments", "CommentTo_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.Comments", "CommentBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ConversationNotReadMessages", new[] { "NotReadMessageId" });
            DropIndex("dbo.ConversationNotReadMessages", new[] { "ConversationId" });
            DropIndex("dbo.ConversationMessages", new[] { "MessageId" });
            DropIndex("dbo.ConversationMessages", new[] { "ConversationId" });
            DropIndex("dbo.UsersWallItems", new[] { "WallItemId" });
            DropIndex("dbo.UsersWallItems", new[] { "UserId" });
            DropIndex("dbo.UsersPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.UsersPlaylists", new[] { "UserId" });
            DropIndex("dbo.KnowledgeSessionApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.KnowledgeSessionApplicationUsers", new[] { "KnowledgeSession_Id" });
            DropIndex("dbo.WallItemLikeDislikes", new[] { "WallItem_WallItemId" });
            DropIndex("dbo.WallItemLikeDislikes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PlaylistItems", new[] { "PlaylistId" });
            DropIndex("dbo.NodeHistories", new[] { "Node_Id" });
            DropIndex("dbo.NodeHistories", new[] { "ByUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.NodeStructureVotes", new[] { "VoteBy_Id" });
            DropIndex("dbo.NodeStructureVotes", new[] { "Node_Id" });
            DropIndex("dbo.NodeModificationVotes", new[] { "VoteBy_Id" });
            DropIndex("dbo.NodeModificationVotes", new[] { "NodeModification_Id" });
            DropIndex("dbo.NodeModifications", new[] { "SuggestedBy_Id" });
            DropIndex("dbo.NodeModifications", new[] { "Node_Id" });
            DropIndex("dbo.SessionNodes", new[] { "SuggestedBy_Id" });
            DropIndex("dbo.SessionNodes", new[] { "Session_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "CommentTo_Id" });
            DropIndex("dbo.Comments", new[] { "CommentBy_Id" });
            DropTable("dbo.ConversationNotReadMessages");
            DropTable("dbo.ConversationMessages");
            DropTable("dbo.UsersWallItems");
            DropTable("dbo.UsersPlaylists");
            DropTable("dbo.KnowledgeSessionApplicationUsers");
            DropTable("dbo.WallItemsSongs");
            DropTable("dbo.WallItemLikeDislikes");
            DropTable("dbo.WallItemImages");
            DropTable("dbo.UserConversations");
            DropTable("dbo.Songs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PlaylistItems");
            DropTable("dbo.NodeHistories");
            DropTable("dbo.MessageSongs");
            DropTable("dbo.ListenedSongs");
            DropTable("dbo.Friends");
            DropTable("dbo.NotReadMessages");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.WallItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Playlists");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.NodeStructureVotes");
            DropTable("dbo.NodeModificationVotes");
            DropTable("dbo.NodeModifications");
            DropTable("dbo.SessionNodes");
            DropTable("dbo.KnowledgeSessions");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
