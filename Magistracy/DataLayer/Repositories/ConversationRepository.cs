using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IConversationRepository
    {
        IEnumerable<Conversation> GetMyConversations(string userId, int type);
        IEnumerable<Conversation> GetAllMusicConversations();
        void AddConversation(string userId, Conversation conversation);
        Conversation AddOrGetDialog(string userId, string myId);
        void RemoveConversation(string userId, Conversation song);
        void AddUserToConversation(string userId, string conversationId);
        void RemoveUserFromConversation(string userId, string conversationId);
        IEnumerable<Message> GetConversationMessages(string userId, Conversation toConversationModel);
        void AddMessageToConversation(string myId, string message, string conversationId, List<string> songIds);
        int GetMyNotReadMessagesCount(string myId);
        int GetConversationNotReadMessagesCount(string myId, string conversationId);
        int ReadConversationMessages(string myId, string conversationId);
        void UpdateConversationCurrentSong(string conversationd, string songId);
        Conversation GetConversation(string conversationId);
        IEnumerable<ApplicationUser> GetConversationUsers(string conversationId);
        void RemoveMessageFromConversation(string myId, string messageId, string conversationId);
        List<Song> GetMessageSongs(string messageId);
        void AddImage(string imagePath, string conversationId);
        void UpdateConversationName(string conversationId, string conversationName);
    }

    public class ConversationRepository : IConversationRepository
    {
        public IEnumerable<Conversation> GetMyConversations(string userId, int type)
        {
            var conversations = new List<Conversation>();

            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user == null)
                {
                    return conversations;
                }
                var userConversations = GetUserConversations(db, userId);
                switch (type)
                {
                    case 1:
                        conversations.AddRange(userConversations);
                        break;
                    case 2: conversations.AddRange(userConversations.Where(m => m.IsDialog == false && m.MusicConversation == false));
                        break;
                    case 3: conversations.AddRange(userConversations.Where(m => m.IsDialog));
                        break;
                    case 4: conversations.AddRange(userConversations.Where(m => m.MusicConversation));
                        break;
                }
                return conversations.OrderByDescending(m => m.LastMessageDate);
            }

        }

        public IEnumerable<Conversation> GetAllMusicConversations()
        {
            var musicConversations = new List<Conversation>();

            using (var db = new ApplicationDbContext())
            {
                musicConversations.AddRange(db.Conversations.Where(m => m.MusicConversation));

                return musicConversations;
            }

        }

        public void AddConversation(string userId, Conversation conversation)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                conversation.CreatorId = userId;
                conversation.ConversationId = Guid.NewGuid().ToString();
                conversation.AddDate = DateTime.Now;
                AddPlaylistToConversation(db, conversation);

                if (user != null)
                {
                    db.Conversations.Add(conversation);
                    var userConversation = new UserConversations
                    {
                        ConversationId = conversation.ConversationId,
                        UserId = userId,
                        AddDate = DateTime.Now
                    };

                    db.UserConversations.Add(userConversation);

                }

                db.SaveChanges();
            }
        }

        private void AddPlaylistToConversation(ApplicationDbContext db, Conversation conversation)
        {
            var playlist = new Playlist
            {
                AddDate = DateTime.Now,
                PlayListName = "Плэйлист " + conversation.Name,
                PlaylistId = Guid.NewGuid().ToString(),
            };

            db.Playlist.Add(playlist);
            db.SaveChanges();
            conversation.PlaylistId = playlist.PlaylistId;
        }

        public Conversation AddOrGetDialog(string userId, string myId)
        {
            var conversation = new Conversation();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                var me = db.Users.FirstOrDefault(m => m.Id == myId);
                if (me != null)
                {
                    var conversationExists = GetUserConversation(db, userId, myId + userId, myId);

                    if (conversationExists != null)
                    {
                        return conversationExists;
                    }
                }

                if (user != null)
                {
                    conversation.CreatorId = myId;
                    conversation.ConversationId = myId + userId;
                    conversation.AddDate = DateTime.Now;
                    conversation.Name = user.UserName;
                    conversation.IsDialog = true;
                    conversation.ConversationAvatarFilePath = user.AvatarFilePath;

                    if (me != null)
                    {
                        db.Conversations.Add(conversation);
                        AddPlaylistToConversation(db, conversation);
                        db.SaveChanges();
                        AddUserToConversation(userId, conversation.ConversationId);
                        AddUserToConversation(myId, conversation.ConversationId);
                    }
                }

            }
            return conversation;
        }

        public void RemoveConversation(string userId, Conversation conversation)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                var conversationToRemove = db.Conversations.FirstOrDefault(m => m.ConversationId == conversation.ConversationId);
                var conversationUsers = GetConversationUsers(db, conversation.ConversationId).ToList();
                if (user != null && conversationToRemove != null)
                {
                    if (conversationUsers.Count > 1)
                    {
                        RemoveUserFromConversation(userId, conversation.ConversationId);
                    }
                    else
                    {
                        RemoveUserFromConversation(userId, conversation.ConversationId);
                        db.Conversations.Remove(conversationToRemove);
                    }

                }

                db.SaveChanges();
            }
        }

        public void AddUserToConversation(string userId, string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);
                    if (conversation != null)
                    {
                        db.UserConversations.Add(
                            new UserConversations
                            {
                                ConversationId = conversationId,
                                AddDate = DateTime.Now,
                                UserId = userId
                            });
                    }

                }

                db.SaveChanges();
            }
        }

        public void RemoveUserFromConversation(string userId, string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);
                    if (conversation != null)
                    {
                        var userConv = db.UserConversations.Where(m => m.ConversationId == conversationId);
                        var removeRecord = userConv.FirstOrDefault(m => m.UserId == userId);
                        if (removeRecord != null)
                        {
                            db.UserConversations.Remove(removeRecord);
                        }
                    }

                }

                db.SaveChanges();
            }
        }

        public IEnumerable<Message> GetConversationMessages(string userId, Conversation conversationModel)
        {
            var conversationMessages = new List<Message>();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var myConversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationModel.ConversationId);

                    if (myConversation != null)
                    {
                        conversationMessages.AddRange(myConversation.Messages);
                    }
                }

                return conversationMessages.OrderBy(m => m.AddDate);
            }
        }

        public int GetMyNotReadMessagesCount(string myId)
        {
            int count = 0;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == myId);

                if (user != null)
                {
                    var conversationsId = db.UserConversations.Where(m => m.UserId == user.Id).Select(m => m.ConversationId);

                    var myConversations = new List<Conversation>();
                    foreach (var id in conversationsId)
                    {
                        var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == id);
                        myConversations.Add(conversation);
                    }

                    foreach (var conversation in myConversations)
                    {
                        count += conversation.NotReadMessages.Count(m => m.IdUser == myId);
                    }
                }

                return count;
            }
        }

        public int GetConversationNotReadMessagesCount(string myId, string conversationId)
        {
            int count = 0;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == myId);

                if (user != null)
                {

                    var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                    if (conversation != null)
                    {
                        count += conversation.NotReadMessages.Count(m => m.IdUser == myId);
                    }
                }

                return count;
            }
        }

        public int ReadConversationMessages(string myId, string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == myId);
                int count = 0;
                if (user != null)
                {
                    var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                    if (conversation != null)
                    {
                        var notReadMessages = conversation.NotReadMessages.Where(m => m.IdUser == myId).ToList();
                        count = notReadMessages.Count();
                        foreach (var message in notReadMessages)
                        {
                            conversation.NotReadMessages.Remove(message);
                        }

                    }
                }
                db.SaveChanges();
                return count;
            }
        }

        public void RemoveMessageFromConversation(string myId, string messageId, string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == myId);

                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                if (conversation != null && user != null)
                {
                    var message = conversation.Messages.FirstOrDefault(m => m.MessageId == messageId);

                    if (message != null)
                    {
                        conversation.Messages.Remove(message);
                    }


                }

                db.SaveChanges();
            }
        }

        public void AddMessageToConversation(string myId, string text, string conversationId, List<string> songIds)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == myId);

                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                if (conversation != null && user != null)
                {
                    var message = new Message
                    {
                        MessageId = Guid.NewGuid().ToString(),
                        FromId = myId,
                        FromName = user.FirstName,
                        AddDate = DateTime.Now,
                        FromAvatarPath = user.AvatarFilePath,
                        Text = text
                    };
                    conversation.Messages.Add(message);
                    conversation.LastMessageDate = DateTime.Now;
                    var convUsers = GetConversationUsers(conversation.ConversationId);

                    if (songIds != null)
                    {
                        foreach (var song in songIds)
                        {
                            db.MessageSongs.Add(new MessageSongs
                            {
                                SongId = song,
                                MessageId = message.MessageId
                            });
                        }
                    }
                    foreach (var conversationUser in convUsers)
                    {
                        if (conversationUser.Id != myId)
                        {
                            var newMessage = new NotReadMessage
                            {
                                IdUser = conversationUser.Id,
                                RecordId = Guid.NewGuid().ToString(),
                                MessageId = message.MessageId,
                            };
                            conversation.NotReadMessages.Add(newMessage);
                        }
                    }
                }

                db.SaveChanges();
            }


        }

        public List<Song> GetMessageSongs(string messageId)
        {
            var resultSongs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var songs = db.MessageSongs.Where(m => m.MessageId == messageId);
                foreach (var song in songs)
                {
                    var findedSong = db.Songs.FirstOrDefault(m => m.SongId == song.SongId);
                    if (findedSong != null)
                    {
                        resultSongs.Add(findedSong);

                    }


                }
                return resultSongs;
            }
        }

        public void AddImage(string imagePath, string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                if (conversation != null)
                {
                    conversation.ConversationAvatarFilePath = imagePath;
                }

                db.SaveChanges();
            }
        }

        public void UpdateConversationCurrentSong(string conversationId, string songId)
        {
            using (var db = new ApplicationDbContext())
            {
                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                if (conversation != null)
                {
                    conversation.SongAtThisMoment = songId;
                }

                db.SaveChanges();
            }
        }

        public void UpdateConversationName(string conversationId, string conversationName)
        {
            using (var db = new ApplicationDbContext())
            {
                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);

                if (conversation != null)
                {
                    conversation.Name = conversationName;
                }

                db.SaveChanges();
            }
        }

        public Conversation GetConversation(string conversationId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);
            }
        }

        public IEnumerable<Conversation> GetUserConversations(ApplicationDbContext db, string userId)
        {
            var conversations = new List<Conversation>();
            var conversationsId = db.UserConversations.Where(m => m.UserId == userId).Select(m => m.ConversationId);


            foreach (var id in conversationsId)
            {
                var conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == id);
                conversations.Add(conversation);
            }

            return conversations;
        }

        public IEnumerable<ApplicationUser> GetConversationUsers(ApplicationDbContext db, string conversationId)
        {
            var users = new List<ApplicationUser>();
            var usersId = db.UserConversations.Where(m => m.ConversationId == conversationId).Select(m => m.UserId);


            foreach (var id in usersId)
            {
                var user = db.Users.FirstOrDefault(m => m.Id == id);
                users.Add(user);
            }

            return users;
        }

        public IEnumerable<ApplicationUser> GetConversationUsers(string conversationId)
        {
            var users = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                var usersId = db.UserConversations.Where(m => m.ConversationId == conversationId).Select(m => m.UserId);

                foreach (var id in usersId)
                {
                    var user = db.Users.FirstOrDefault(m => m.Id == id);
                    users.Add(user);
                }
            }

            return users;
        }

        public Conversation GetUserConversation(ApplicationDbContext db, string userId, string conversationId, string myId)
        {
            Conversation conversation;
            var conversationsId = db.UserConversations.Where(m => m.UserId == myId).Select(m => m.ConversationId);
            var exist = conversationsId.Contains(conversationId);

            if (exist)
            {
                conversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);
            }
            else
            {
                var findConversation = db.Conversations.FirstOrDefault(m => m.ConversationId == conversationId);
                if (findConversation != null)
                {
                    AddUserToConversation(myId, findConversation.ConversationId);
                    return findConversation;
                }
                return null;
            }
            return conversation;
        }

    }
}