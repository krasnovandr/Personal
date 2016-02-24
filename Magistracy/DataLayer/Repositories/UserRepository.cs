using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Repositories
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

    public class UserRepository : IUserRepository
    {
        public IEnumerable<ApplicationUser> GetUsers(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var friendsId = db.Friends.Where(m => m.UserId == userId).Select(m => m.FriendId).ToList();
                friendsId.Add(userId);
                var users = db.Users.Where(m => friendsId.Contains(m.Id) == false).ToList();

                return users;
            }
        }

        public ApplicationUser GetUser(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = (from entity in db.Users
                            where entity.Id == id
                            select entity).FirstOrDefault();

                return user;
            }
        }

        public void AddImage(string filepath, string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = (from entity in db.Users
                            where entity.Id == id
                            select entity).FirstOrDefault();
                if (user != null)
                {
                    user.AvatarFilePath = filepath;

                }
                db.SaveChanges();
            }
        }

        public void UpdateUser(ApplicationUser userInfo)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = (from entity in db.Users
                            where entity.Id == userInfo.Id
                            select entity).FirstOrDefault();
                if (user != null)
                {
                    user.Email = userInfo.Email;
                    user.BestAlarmClock = userInfo.BestAlarmClock;
                    user.BestCinemaSoundtrack = userInfo.BestCinemaSoundtrack;
                    user.BestForeignArtist = userInfo.BestForeignArtist;
                    user.BestGameSoundtrack = userInfo.BestGameSoundtrack;
                    user.BestGenre = userInfo.BestGenre;
                    user.BestNativeArtist = userInfo.BestNativeArtist;
                    user.BestRelaxSong = userInfo.BestRelaxSong;
                    user.BestSleeping = userInfo.BestSleeping;
                    user.BestVocalist = userInfo.BestVocalist;
                    user.City = userInfo.City;
                    user.Country = userInfo.Country;
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.BirthDate = userInfo.BirthDate?? DateTime.Now;
                    user.LastEntrenchedSong = userInfo.LastEntrenchedSong;
                    user.WorstGenre = userInfo.WorstGenre;
                    user.LastActivity = DateTime.Now;
                    //user.BestAtrists = userInfo.BestAtrists;
                    //user.BestGenres = userInfo.BestGenres;
                }
                db.SaveChanges();
            }
        }

        public void AddFriend(string userId, string friendId)
        {
            using (var db = new ApplicationDbContext())
            {
                var friend = new Friend
                {
                    UserId = userId,
                    AddDate = DateTime.Now,
                    FriendId = friendId,
                    RecordId = Guid.NewGuid().ToString()
                };
                db.Friends.Add(friend);
                db.SaveChanges();
            }


        }

        public void ConfirmFriend(string userId, string friendId)
        {
            using (var db = new ApplicationDbContext())
            {

                var record = db.Friends.FirstOrDefault(m => m.UserId == friendId && m.FriendId == userId);
                if (record != null)
                {
                    record.Confirmed = true;
                    db.SaveChanges();

                    var friend = new Friend
                    {
                        UserId = userId,
                        AddDate = DateTime.Now,
                        FriendId = friendId,
                        RecordId = Guid.NewGuid().ToString(),
                        Confirmed = true
                    };
                    db.Friends.Add(friend);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveFriend(string userId, string friendId)
        {
            using (var db = new ApplicationDbContext())
            {

                var userFriend = db.Friends.FirstOrDefault(m => m.UserId == userId && m.FriendId == friendId);

                if (userFriend != null)
                {
                    db.Friends.Remove(userFriend);
                }

                var friendUser = db.Friends.FirstOrDefault(m => m.UserId == friendId && m.FriendId == userId);

                if (friendUser != null)
                {
                    db.Friends.Remove(friendUser);
                }

                db.SaveChanges();
            }
        }

        public IEnumerable<ApplicationUser> GetOutgoingRequests(string userId)
        {
            var friends = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                var friendsId = db.Friends.Where(m => m.UserId == userId && m.Confirmed== false).Select(m => m.FriendId);

                foreach (var friendId in friendsId)
                {
                    var friend = db.Users.FirstOrDefault(m => m.Id == friendId);
                    friends.Add(friend);
                }
            }
            return friends;
        }

        public IEnumerable<ApplicationUser> GetIncomingRequests(string userId)
        {
            var friends = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                var friendsId = db.Friends.Where(m => m.FriendId == userId && m.Confirmed == false).Select(m => m.UserId);

                foreach (var friendId in friendsId)
                {
                    var friend = db.Users.FirstOrDefault(m => m.Id == friendId);
                    friends.Add(friend);
                }
            }
            return friends;
        }


        public IEnumerable<ApplicationUser> GetFriends(string userId)
        {
            var friends = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                var friendsId = db.Friends.Where(m => m.UserId == userId && m.Confirmed).Select(m => m.FriendId);

                foreach (var friendId in friendsId)
                {
                    var friend = db.Users.FirstOrDefault(m => m.Id == friendId);
                    friends.Add(friend);
                }
            }
            return friends;
        }

        public void UpdateUserCurrentSong(string userId, string songId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                // var song = db.Songs.FirstOrDefault(m => m.SongId == songId);
                if (user != null)
                {
                    user.SongAtThisMoment = songId;
                }

                db.SaveChanges();
            }
        }

        public void UpdateUserVkInfo(string userId, string login, string password)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                // var song = db.Songs.FirstOrDefault(m => m.SongId == songId);
                if (user != null)
                {
                    user.VkLogin = login;
                    user.VkPassword = password;
                }

                db.SaveChanges();
            }
        }
    }
}
