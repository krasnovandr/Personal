using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IMusicRepository
    {
        IEnumerable<Song> GetSongs(string userId);
        //IEnumerable<Song> GetSongs(string userId, string playListid);
        IEnumerable<Song> GetSongs();
        IEnumerable<Song> GetSongsUploadBy(string userId);
        // void RemovePlaylist(string userId, string playListid);
        // void RemoveSong(string userId, string playListId, string songId);

        void AddSong(Song song, string userId);
        void AddSongToUser(string songId, string userId);
        void RemoveSong(string songId, string userId);
        Song GetSong(string songId);

        void ListenedSong(string songId, string userId);
    }

    public class MusicRepository : IMusicRepository
    {
        public IEnumerable<Song> GetSongs(string userId)
        {
            var songs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var playList = db.Playlist.FirstOrDefault(m => m.PlaylistId == userId);

                if (playList == null)
                {
                    return songs;
                }

                var songsId = db.PlaylistItem.Where(m => m.PlaylistId == playList.PlaylistId).OrderByDescending(m => m.AddDate).Select(m => m.SongId).ToList();

                foreach (var songId in songsId)
                {
                    var song = db.Songs.FirstOrDefault(m => m.SongId == songId);
                    songs.Add(song);
                }
            }
            return songs;
        }

        public IEnumerable<Song> GetSongs()
        {
            var songs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var songsDb = db.Songs.ToList();
                songs.AddRange(songsDb);
            }
            return songs.OrderByDescending(m => m.AddDate);
        }

        public IEnumerable<Song> GetSongsUploadBy(string userId)
        {
            var songs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var songsDb = db.Songs.Where(m => m.AddBy == userId);
                songs.AddRange(songsDb);
            }
            return songs.OrderByDescending(m => m.AddDate);
        }

        public Song GetSong(string songId)
        {

            using (var db = new ApplicationDbContext())
            {
                return db.Songs.FirstOrDefault(m => m.SongId == songId);
            }

        }

        public void ListenedSong(string songId, string userId)
        {
            using (var db = new ApplicationDbContext())
            {

                //var currentItem = db.ListenedSong.FirstOrDefault(m => m.SongId == songId && m.UserId == userId);
                //if (currentItem != null)
                //{
                //    currentItem.ListenCount++;
                //}
                //else
                //{
                var listenedSong = new ListenedSong
                {
                    UserId = userId,
                    ListenDate = DateTime.Now,
                    SongId = songId
                };
                db.ListenedSong.Add(listenedSong);

                //                }


                db.SaveChanges();
            }
        }

        public void AddSong(Song song, string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                song.AddBy = userId;
                song.AddDate = DateTime.Now;
                db.Songs.Add(song);

                CreateUpdateDefaultPlaylist(song.SongId, userId, db);

                db.SaveChanges();
            }
        }

        private static void CreateUpdateDefaultPlaylist(string songId, string userId, ApplicationDbContext db)
        {
            var user = (from entity in db.Users
                        where entity.Id == userId
                        select entity).FirstOrDefault();

            if (user != null && user.Playlist.Any(m => m.PlaylistId == userId) == false)
            {
                var defaultPlaylist = new Playlist
                {
                    AddDate = DateTime.Now,
                    PlaylistId = userId,
                    PlayListName = "Мои аудиозаписи",
                    DefaultPlaylist = true
                };


                db.Playlist.Add(defaultPlaylist);
                user.Playlist.Add(defaultPlaylist);

                var newRecord = new PlaylistItem
                {
                    SongId = songId,
                    AddDate = DateTime.Now,
                    PlaylistItemId = Guid.NewGuid().ToString(),
                    PlaylistId = defaultPlaylist.PlaylistId,
                    TrackPos = 1
                };

                db.PlaylistItem.Add(newRecord);
            }
            else
            {
                var defaultPlaylist = db.Playlist.FirstOrDefault(m => m.PlaylistId == userId);

                if (defaultPlaylist != null)
                {
                    var trackPos = db.PlaylistItem.Where(m => m.PlaylistId == defaultPlaylist.PlaylistId).Max(m => m.TrackPos);
                    var newRecord = new PlaylistItem
                    {
                        SongId = songId,
                        AddDate = DateTime.Now,
                        PlaylistItemId = Guid.NewGuid().ToString(),
                        PlaylistId = defaultPlaylist.PlaylistId,
                        TrackPos = trackPos + 1
                    };

                    db.PlaylistItem.Add(newRecord);
                }
            }
            db.SaveChanges();
        }

        public void AddSongToUser(string songId, string userId)
        {
            using (var db = new ApplicationDbContext())
            {

                CreateUpdateDefaultPlaylist(songId, userId, db);

                db.SaveChanges();
            }
        }

        public void RemoveSong(string songId, string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var defaultPlaylist = db.Playlist.FirstOrDefault(m => m.PlaylistId == userId);

                if (defaultPlaylist != null)
                {

                    var songToremove = db.PlaylistItem.FirstOrDefault(m => m.PlaylistId == defaultPlaylist.PlaylistId
                                                                           && m.SongId == songId);
                    if (songToremove != null)
                    {
                        db.PlaylistItem.Remove(songToremove);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
