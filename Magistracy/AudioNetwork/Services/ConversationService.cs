using System.Collections.Generic;
using System.Linq;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Models;
using DataLayer.Repositories;

namespace AudioNetwork.Services
{
    public interface IConversationService
    {
        List<ConversationViewModel> GetConversations(int type, string userId);
        ConversationViewModel GetConversation(string userId, string conversationId);
        List<UserViewModel> GetConversationPeople(string conversationId);
        SongViewModel GetConversationCurrentSong(Conversation conversation);
        List<SongViewModel> GetConversationSongs(ConversationViewModel conversation);
        void AddConversation(string userId, ConversationViewModel conversationModel);
        ConversationViewModel AddOrGetDialog(string userId, string myId);
        void RemoveConversation(string userId, ConversationViewModel conversationViewModel);
        void AddMessageToConversation(string myId, string text, string conversationId, List<Song> songs);
        void RemoveMessageFromConversation(string myId, string messageId, string conversationId);
        void AddUserToConversation(string userId, string conversationId);
        void RemoveUserFromConversation(string id, string conversationId);
        int GetMyNotReadMessagesCount(string myId);
        int ReadConversationMessages(string myId, string conversationId);
        void UpdateConversationCurrentSong(string conversationId, string songId);
        List<ConversationViewModel> GetMusicConversations(string userId);
        List<MessageViewModel> GetConversationMessages(ConversationViewModel conversationViewModel, string userId);
    }
    public class ConversationService : IConversationService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IConversationRepository _conversationRepository;

        public ConversationService(
            IConversationRepository conversationRepository,
            IMusicRepository musicRepository,
            IPlaylistRepository playlistRepository)
        {
            _conversationRepository = conversationRepository;
            _musicRepository = musicRepository;
            _playlistRepository = playlistRepository;
        }

        public List<ConversationViewModel> GetConversations(int type, string userId)
        {
            var myConversationsdb = _conversationRepository.GetMyConversations(userId, type).ToList();
            var result = new List<ConversationViewModel>();

            foreach (var conversation in myConversationsdb)
            {
                var converView = GetConversation(userId, conversation.ConversationId);
                result.Add(converView);
            }

            return result;
        }

        public ConversationViewModel GetConversation(string userId, string conversationId)
        {
            var conversation = _conversationRepository.GetConversation(conversationId);
            var converView = ModelConverters.ToConversationViewModel(conversation);

            converView.ConversationUsers = GetConversationPeople(conversation.ConversationId);
            converView.NotReadCount = _conversationRepository.GetConversationNotReadMessagesCount(userId,
                conversation.ConversationId);

            converView.CurrentSong = GetConversationCurrentSong(conversation);
            converView.ConversationSongs = GetConversationSongs(converView);
            return converView;
        }

        public List<UserViewModel> GetConversationPeople(string conversationId)
        {
            var conversationPeople = _conversationRepository.GetConversationUsers(conversationId);
            var conversationUsers = new List<UserViewModel>();
            conversationUsers.AddRange(conversationPeople.Select(ModelConverters.ToUserViewModel));
            return conversationUsers;
        }

        public SongViewModel GetConversationCurrentSong(Conversation conversation)
        {
            var song = _musicRepository.GetSong(conversation.SongAtThisMoment);
            if (song != null)
            {
                return ModelConverters.ToSongViewModel(song);
            }

            return null;
        }

        public List<SongViewModel> GetConversationSongs(ConversationViewModel conversation)
        {
            var result = new List<SongViewModel>();
            if (string.IsNullOrEmpty(conversation.PlaylistId) == false)
            {
                var songs = _playlistRepository.GetSongs(conversation.PlaylistId);
                if (songs != null)
                {
                    result.AddRange(songs.Select(ModelConverters.ToSongViewModel));
                }
            }

            return result;
        }

        public void AddConversation(string userId, ConversationViewModel conversationModel)
        {
            var model = ModelConverters.ToConversationModel(conversationModel);
            model.ConversationAvatarFilePath = FilePathContainer.ConversationCoversPathRelative +
                                                               "/DefaultConversation.jpg";
            _conversationRepository.AddConversation(userId, model);
        }

        public List<MessageViewModel> GetConversationMessages(ConversationViewModel conversationViewModel, string userId)
        {
            var messages = _conversationRepository.GetConversationMessages(userId,
                ModelConverters.ToConversationModel(conversationViewModel));
            var resultList = new List<MessageViewModel>();
            resultList.AddRange(messages.Select(ModelConverters.ToMessageViewModel));
            foreach (var message in resultList)
            {
                message.MessageSongs = new List<SongViewModel>();
                var songs = _conversationRepository.GetMessageSongs(message.MessageId);
                message.MessageSongs.AddRange(songs.Select(ModelConverters.ToSongViewModel));
            }
            return resultList;
        }

        public ConversationViewModel AddOrGetDialog(string userId, string myId)
        {
            return ModelConverters.ToConversationViewModel(_conversationRepository.AddOrGetDialog(userId, myId));
        }

        public void RemoveConversation(string userId, ConversationViewModel conversationViewModel)
        {
            _conversationRepository.RemoveConversation(userId, ModelConverters.ToConversationModel(conversationViewModel));
        }

        public void AddMessageToConversation(string myId, string text, string conversationId, List<Song> songs)
        {
            var songIds = new List<string>();
            if (songs != null)
            {
                songIds = songs.Select(m => m.SongId).ToList();
            }
            _conversationRepository.AddMessageToConversation(myId, text, conversationId, songIds);
        }

        public void RemoveMessageFromConversation(string myId, string messageId, string conversationId)
        {
            _conversationRepository.RemoveMessageFromConversation(myId, messageId, conversationId);
        }

        public void AddUserToConversation(string userId, string conversationId)
        {
            _conversationRepository.AddUserToConversation(userId, conversationId);
        }

        public void RemoveUserFromConversation(string id, string conversationId)
        {
            _conversationRepository.RemoveUserFromConversation(id, conversationId);
        }

        public int GetMyNotReadMessagesCount(string myId)
        {
            return _conversationRepository.GetMyNotReadMessagesCount(myId);
        }

        public int ReadConversationMessages(string myId, string conversationId)
        {
            return _conversationRepository.ReadConversationMessages(myId, conversationId);
        }

        public void UpdateConversationCurrentSong(string conversationId, string songId)
        {
            _conversationRepository.UpdateConversationCurrentSong(conversationId, songId);
        }

        public List<ConversationViewModel> GetMusicConversations(string userId)
        {
            var musicConversations = _conversationRepository.GetAllMusicConversations();
            var ids = _conversationRepository.GetMyConversations(userId, 4).Select(m => m.ConversationId).ToList();
            var result = new List<ConversationViewModel>();

            foreach (var conversation in musicConversations)
            {
                var converView = GetConversation(userId, conversation.ConversationId);
                if (ids.Contains(converView.ConversationId))
                {
                    converView.MyConversation = true;
                }

                result.Add(converView);
            }

            return result;

        }
    }
}