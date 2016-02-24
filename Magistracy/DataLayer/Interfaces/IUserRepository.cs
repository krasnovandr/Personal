using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers(string userId);
        ApplicationUser GetUser(string id);
        void AddImage(string filepath, string id);
        void UpdateUser(ApplicationUser user);

        void AddFriend(string userId, string friendId);
        void RemoveFriend(string userId, string friendId);
        IEnumerable<ApplicationUser> GetFriends(string userId);
        void UpdateUserCurrentSong(string userId, string songId);
        void UpdateUserVkInfo(string userId, string login, string password);
        IEnumerable<ApplicationUser> GetOutgoingRequests(string userId);
        IEnumerable<ApplicationUser> GetIncomingRequests(string userId);
        void ConfirmFriend(string userId, string friendId);
    }

}
