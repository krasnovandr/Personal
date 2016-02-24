using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
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
}
