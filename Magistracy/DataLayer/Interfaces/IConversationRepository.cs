using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
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
}
