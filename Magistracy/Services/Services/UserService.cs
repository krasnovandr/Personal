using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Models;
using DataLayer.Repositories;
using TagLib.Riff;

namespace AudioNetwork.Services
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> SearchUsers(UserSearchModel searchModel, string userId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMusicRepository _musicRepository;

        public UserService(
            IUserRepository userRepository, IMusicRepository musicRepository)
        {
            _userRepository = userRepository;
            _musicRepository = musicRepository;
        }

        public IEnumerable<UserViewModel> SearchUsers(UserSearchModel searchModel, string userId)
        {
            var usersDb = _userRepository.GetUsers(userId).ToList();
            var userList = ModelConverters.ToUserViewModelList(usersDb);
            var result = new List<UserViewModel>();

            if (string.IsNullOrEmpty(searchModel.Country) == false)
            {
                result.AddRange(userList.Where(m => m.Country != null && m.Country.ToLower().Contains(searchModel.Country.ToLower())));
            }

            if (string.IsNullOrEmpty(searchModel.City) == false)
            {
                result.AddRange(userList.Where(m => m.City != null && m.City.ToLower().Contains(searchModel.City.ToLower())));
            }

            if (string.IsNullOrEmpty(searchModel.Genres) == false)
            {
                result.AddRange(userList.Where(m => m.BestGenres != null && m.BestGenres.ToLower().Contains(searchModel.Genres.ToLower())));
            }

            if (string.IsNullOrEmpty(searchModel.Atrists) == false)
            {
                result.AddRange(
                    userList.Where(m =>
                        m.BestVocalist != null && m.BestVocalist.ToLower().Contains(searchModel.Atrists.ToLower()) ||
                         m.BestForeignArtist != null && m.BestForeignArtist.ToLower().Contains(searchModel.Atrists.ToLower()) ||
                         m.BestNativeArtist != null && m.BestNativeArtist.ToLower().Contains(searchModel.Atrists.ToLower()))
                         );
            }

            if (string.IsNullOrEmpty(searchModel.FirstName) == false)
            {
                result.AddRange(userList.Where(m => m.FirstName != null && m.FirstName.ToLower().Contains(searchModel.FirstName.ToLower())));
            }

            if (string.IsNullOrEmpty(searchModel.LastName) == false)
            {
                result.AddRange(userList.Where(m => m.LastName != null && m.LastName.ToLower().Contains(searchModel.LastName.ToLower())));
            }

            if (searchModel.BirthDate.HasValue)
            {
                result.AddRange(userList.Where(m => m.BirthDate.Date == searchModel.BirthDate.Value.Date));
            }

            return result;
        }

        public List<UserViewModel> GetUsers(string userId)
        {
            var user = _userRepository.GetUsers(userId);
            var resultList = new List<UserViewModel>();
            resultList.AddRange(user.Select(ModelConverters.ToUserViewModel));

            return resultList;
        }

        public UserViewModel GetUser(string id)
        {
            var user = ModelConverters.ToUserViewModel(_userRepository.GetUser(id));
            if (user != null)
            {

                var song = _musicRepository.GetSong(user.SongAtThisMoment);
                if (song != null)
                {
                    if (user.IsOnline == false)
                    {
                        user.CurrentSong = null;
                    }
                    else
                    {
                        user.CurrentSong = ModelConverters.ToSongViewModel(song);
                    }
                }
            }

            return user;
        }

        public void UpdateUser(UserViewModel userInfo)
        {
            var user = ModelConverters.ToApplicationUser(userInfo);
            _userRepository.UpdateUser(user);
        }

        public void UpdateUserVkInfo(string userId, string login, string password)
        {
            _userRepository.UpdateUserVkInfo(userId, login, password);
        }

        public void UpdateUserCurrentSong(string userId, string songId)
        {
            _userRepository.UpdateUserCurrentSong(userId, songId);
        }

        public void AddFriend(string userId, string id)
        {
            _userRepository.AddFriend(userId, id);
        }

        public List<ApplicationUser> GetFriends(string userId)
        {

            var friends = _userRepository.GetFriends(userId).ToList();
            var resultList = new List<UserViewModel>();
            resultList.AddRange(friends.Select(ModelConverters.ToUserViewModel));

            foreach (var friend in resultList)
            {
                var song = _musicRepository.GetSong(friend.SongAtThisMoment);
                if (song != null)
                {
                    if (friend.LastActivity > DateTime.Now.AddMinutes(3))
                    {
                        friend.CurrentSong = null;
                    }
                    else
                    {
                        friend.CurrentSong = ModelConverters.ToSongViewModel(song);
                    }
                }

            }

            return friends;
        }

        public void RemoveFriend(string userId, string id)
        {
            _userRepository.RemoveFriend(userId, id);
        }
    }
}