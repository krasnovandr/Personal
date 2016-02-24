using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IWallRepository
    {
        List<WallItem> GetWall(string userId);
        WallItem GetWallIem(string userId, int wallItemId);
        void AddWallItem(string userId, WallItem wallItem, IEnumerable<string> songsId, string pictureId);
        void RemoveWallItem(string userId, int wallItemId);
        List<Song> GetWallItemSongs(string userId, int wallItemId);
        void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike);
        List<WallItemLikeDislike> GetWallItemLikeDislikes(int wallItemId);
        string GetWallItemImage(int wallItemId);
    }

    public class WallRepository : IWallRepository
    {
        public List<WallItem> GetWall(string userId)
        {
            var wall = new List<WallItem>();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    wall.AddRange(user.WallItems);
                }
                return wall;
            }
        }

        public WallItem GetWallIem(string userId, int wallItemId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    return user.WallItems.FirstOrDefault(m => m.WallItemId == wallItemId);
                }
                return null;
            }
        }

        public List<Song> GetWallItemSongs(string userId, int wallItemId)
        {
            var resultSongs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var wallItem = user.WallItems.FirstOrDefault(m => m.WallItemId == wallItemId);

                    if (wallItem != null)
                    {
                        var songs = db.WallItemsSongs.Where(m => m.WallItemId == wallItemId);

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
                return null;
            }
        }

        public void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike)
        {
            using (var db = new ApplicationDbContext())
            {
                var wallItem = db.WallItems.FirstOrDefault(m => m.WallItemId == wallItemId);
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                var existsLike = db.WallItemLikeDislike
                    .FirstOrDefault(m => m.ApplicationUser.Id == userId && m.WallItem.WallItemId == wallItemId);


                if (existsLike != null && user != null)
                {
                    existsLike.Like = like;
                    existsLike.DisLike = dislike;
                    existsLike.Date = DateTime.Now;
                }
                else
                {
                    if (wallItem != null && user != null)
                    {
                        var record = new WallItemLikeDislike
                        {
                            Date = DateTime.Now,
                            ApplicationUser = user,
                            WallItem = wallItem,
                            Like = like,
                            DisLike = dislike,
                            UserId = user.Id
                        };

                        db.WallItemLikeDislike.Add(record);
                    }
                }


                db.SaveChanges();
            }
        }

        public List<WallItemLikeDislike> GetWallItemLikeDislikes(int wallItemId)
        {
            var result = new List<WallItemLikeDislike>();

            using (var db = new ApplicationDbContext())
            {
                var wallItem = db.WallItems.FirstOrDefault(m => m.WallItemId == wallItemId);

                if (wallItem != null)
                {
                    var wallItemLikes = db.WallItemLikeDislike.Where(m => m.WallItem.WallItemId == wallItemId).ToList();
                    result.AddRange(wallItemLikes);
                }
            }

            return result;
        }

        public string GetWallItemImage(int wallItemId)
        {
            using (var db = new ApplicationDbContext())
            {
                var wallItem = db.WallItemImages.FirstOrDefault(m => m.WallItemId == wallItemId);

                if (wallItem != null)
                {
                    return wallItem.ImagePath;
                }
                return null;
            }
        }

        public void AddWallItem(string userId, WallItem wallItem, IEnumerable<string> songsId, string picturePath)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                wallItem.AddDate = DateTime.Now;

                if (user != null)
                {
                    user.WallItems.Add(wallItem);
                    db.WallItems.Add(wallItem);
                    db.SaveChanges();
                    if (songsId != null)
                    {
                        foreach (var song in songsId)
                        {
                            db.WallItemsSongs.Add(new WallItemsSongs
                            {
                                SongId = song,
                                WallItemId = wallItem.WallItemId
                            });
                        }
                    }

                    if (string.IsNullOrEmpty(picturePath) == false)
                    {
                        db.WallItemImages.Add(new WallItemImages { ImagePath = picturePath, WallItemId = wallItem.WallItemId });
                    }
                    db.SaveChanges();
                }
            }
        }

        public void RemoveWallItem(string userId, int wallItemId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var wallItem = user.WallItems.FirstOrDefault(m => m.WallItemId == wallItemId);

                    if (wallItem != null)
                    {
                        var itemSongs = db.WallItemsSongs.Where(m => m.WallItemId == wallItemId);
                        db.WallItemsSongs.RemoveRange(itemSongs);
                        user.WallItems.Remove(wallItem);
                        db.WallItems.Remove(wallItem);
                        var likes = db.WallItemLikeDislike.Where(m => m.WallItem.WallItemId == wallItem.WallItemId);
                        db.WallItemLikeDislike.RemoveRange(likes);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
