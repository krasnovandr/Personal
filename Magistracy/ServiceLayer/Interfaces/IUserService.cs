using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> SearchUsers(UserSearchModel searchModel, string userId);
        List<UserViewModel> GetUsers(string userId);
        UserViewModel GetUser(string id);
        void UpdateUser(UserViewModel userInfo);
        void UpdateUserVkInfo(string userId, string login, string password);
        void UpdateUserCurrentSong(string userId, string songId);
        void AddFriend(string userId, string id);
        List<UserViewModel> GetFriends(string userId);
        void RemoveFriend(string userId, string id);

        //    List<UserViewModel> GetNotConfirmedFriends(string userId);
        List<UserViewModel> GetIncomingRequests(string userId);
        List<UserViewModel> GetOutgoingRequests(string userId);
        void ConfirmFriend(string userId, string id);
    }
}
