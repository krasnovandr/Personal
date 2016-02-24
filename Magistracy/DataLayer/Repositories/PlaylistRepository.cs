using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IPlaylistRepository
    {
        IEnumerable<Song> GetSongs(string userId, string playListid);
        IEnumerable<Song> GetSongs(string playListid);
        void RemovePlaylist(string userId, string playListid);
        IEnumerable<Playlist> GetPlayLists(string userId);
        IEnumerable<Playlist> GetPlayLists();
        void AddPlayList(string userId, Playlist playListModel);
        void SaveCurrentPlaylist(string userId, string playlistId, List<Song> playlistSongs);
        void AddToVkPlayList(string userid, Song songModel);
        bool RemoveSong(string songId, string playlistId);
    }
 
    public class PlaylistRepository : IPlaylistRepository
    {
        public IEnumerable<Song> GetSongs(string userId, string playListid)
        {
            var songs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user == null)
                {
                    return null;
                }
                var playList = user.Playlist.FirstOrDefault(m => m.PlaylistId == playListid);

                if (playList == null)
                {
                    return null;
                }


                var songsId = db.PlaylistItem.Where(m => m.PlaylistId == playListid).OrderBy(m => m.TrackPos).Select(m => m.SongId).ToList();

                foreach (var songId in songsId)
                {
                    var song = db.Songs.FirstOrDefault(m => m.SongId == songId);
                    if (song != null)
                    {
                        songs.Add(song);
                    }
                }
            }
            return songs;
        }

        public IEnumerable<Song> GetSongs(string playListid)
        {
            var songs = new List<Song>();
            using (var db = new ApplicationDbContext())
            {
                var playList = db.Playlist.FirstOrDefault(m => m.PlaylistId == playListid);

                if (playList == null)
                {
                    return null;
                }

                var songsId = db.PlaylistItem.Where(m => m.PlaylistId == playListid).OrderBy(m => m.TrackPos).Select(m => m.SongId).ToList();

                foreach (var songId in songsId)
                {
                    var song = db.Songs.FirstOrDefault(m => m.SongId == songId);
                    songs.Add(song);
               } 
            }
            return songs;
        }

        public void RemovePlaylist(string userId, string playListid)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null)
                {
                    var playListUser = user.Playlist.FirstOrDefault(m => m.PlaylistId == playListid);
                    var playList = db.Playlist.FirstOrDefault(m => m.PlaylistId == playListid);

                    if (playListUser != null)
                    {
                        user.Playlist.Remove(playListUser);
                        var items = db.PlaylistItem.Where(m => m.PlaylistId == playListUser.PlaylistId);

                        if (items.Any())
                        {
                            db.PlaylistItem.RemoveRange(items);
                        }
                    }

                    if (playList != null)
                    {
                        db.Playlist.Remove(playList);
                    }
                }

                db.SaveChanges();
            }
        }

        public IEnumerable<Playlist> GetPlayLists(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user != null && user.Playlist != null)
                {
                    return user.Playlist.OrderBy(m => m.AddDate); //Where(m=>m.PlaylistId != userId);
                }

                return null;
            }
        }

        public IEnumerable<Playlist> GetPlayLists()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Playlist.ToList();
            }
        }

        public void AddPlayList(string userId, Playlist playListModel)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                playListModel.AddDate = DateTime.Now;
                playListModel.PlaylistId = Guid.NewGuid().ToString();
                db.Playlist.Add(playListModel);
                if (user != null)
                {
                    user.Playlist.Add(playListModel);
                }

                db.SaveChanges();
            }
        }

        public void AddToVkPlayList(string userId, Song song)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                var myPlaylist = db.Playlist.FirstOrDefault(m => m.PlaylistId == userId + "VK");
                song.AddDate = DateTime.Now;
                // song.SongId = Guid.NewGuid().ToString();

                var songExists = db.Songs.FirstOrDefault(m => m.SongId == song.SongId);

                if (songExists == null)
                {
                    db.Songs.Add(song);
                }
                if (myPlaylist == null)
                {
                    if (user != null)
                    {
                        var playlist = new Playlist
                        {
                            AddDate = DateTime.Now,
                            Note = "Vk плэйлист",
                            PlayListName = "Добавлено из вк",
                            PlaylistId = userId + "VK",

                        };


                        db.Playlist.Add(playlist);
                        user.Playlist.Add(playlist);

                        AddSongItemToPlayList(song, playlist, db);
                    }

                }
                else
                {
                    AddSongItemToPlayList(song, myPlaylist, db);
                }

                db.SaveChanges();
            }
        }

        public bool RemoveSong(string songId, string playlistId)
        {
            using (var db = new ApplicationDbContext())
            {
                var playlistItem = db.PlaylistItem.FirstOrDefault(m => m.PlaylistId == playlistId && m.SongId == songId);
                if (playlistItem != null)
                {
                    db.PlaylistItem.Remove(playlistItem);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }

        }

        private static void AddSongItemToPlayList(Song song, Playlist playlist, ApplicationDbContext db)
        {
            var newRecord = new PlaylistItem
            {
                SongId = song.SongId,
                AddDate = DateTime.Now,
                PlaylistItemId = Guid.NewGuid().ToString(),
                PlaylistId = playlist.PlaylistId,
            };
            db.PlaylistItem.Add(newRecord);
        }

        public void SaveCurrentPlaylist(string userId, string playlistId, List<Song> playlistSongs)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);

                if (user == null)
                {
                    return;
                }

                //if (string.IsNullOrEmpty(playlistParams.PlaylistId))
                //{
                //    FillPlayListSongs(playlistParams, playlistSongs, db);

                //    playlistParams.PlaylistId = Guid.NewGuid().ToString();
                //    playlistParams.AddDate = DateTime.Now;
                //    db.Playlist.Add(playlistParams);
                //    user.Playlist.Add(playlistParams);
                //}
                var items = db.PlaylistItem.Where(m => m.PlaylistId == playlistId);

                if (items.Any())
                {
                    db.PlaylistItem.RemoveRange(items);
                }

                FillPlayListSongs(playlistId, playlistSongs, db);


                //var playlist = user.Playlist.FirstOrDefault(m => m.PlaylistId == playlistId);
                db.SaveChanges();

                //if (playlist == null)
                //{

                //}
            }


        }

        private static void FillPlayListSongs(string playlistId, List<Song> playlistSongs, ApplicationDbContext db)
        {
            if (playlistSongs == null)
            {
                return;
            }

            for (int i = 0; i < playlistSongs.Count; i++)
            {
                var newRecord = new PlaylistItem
                {
                    SongId = playlistSongs[i].SongId,
                    AddDate = DateTime.Now,
                    PlaylistItemId = Guid.NewGuid().ToString(),
                    PlaylistId = playlistId,
                    TrackPos = i + 1
                };
                db.PlaylistItem.Add(newRecord);
            }
            //foreach (var song in playlistSongs)
            //{




            //}
        }
    }
}
