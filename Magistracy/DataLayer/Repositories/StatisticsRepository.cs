using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IStatisticsRepository
    {
        IEnumerable<ListenedSong> GetLastListenedSongs(string userId, int count = 250);
        IEnumerable<Song> GetFavoriteSongs(int count = 250);
        IEnumerable<Song> GetFavoriteSongs(string userId, int count = 250);
        IEnumerable<Song> GetLastAdded(int count = 250);
        IEnumerable<ListenedSong> GetSongListeneInfo(string songId, int count = 250);
    }

    public class StatisticsRepository : IStatisticsRepository
    {
        public IEnumerable<ListenedSong> GetLastListenedSongs(string userId, int count = 250)
        {
            List<ListenedSong> myListenSongs;

            using (var db = new ApplicationDbContext())
            {
                myListenSongs = db.ListenedSong.Where(m => m.UserId == userId).OrderByDescending(m => m.ListenDate).ToList();

                //foreach (var song in myListenSongs)
                //{
                //    var songd = new Song();
                //    songd = GetSongById(db, song.SongId);
                //    songd.
                //    result.Add(GetSongById(db, song.SongId));
                //}
            }

            return myListenSongs.Take(count);
        }

        public IEnumerable<ListenedSong> GetSongListeneInfo(string songId, int count = 250)
        {
            List<ListenedSong> myListenSongs;

            using (var db = new ApplicationDbContext())
            {
                myListenSongs = db.ListenedSong.Where(m => m.SongId == songId).OrderByDescending(m => m.ListenDate).ToList();

                //foreach (var song in myListenSongs)
                //{
                //    var songd = new Song();
                //    songd = GetSongById(db, song.SongId);
                //    songd.
                //    result.Add(GetSongById(db, song.SongId));
                //}
            }

            return myListenSongs.Take(count);
        }

        public IEnumerable<Song> GetFavoriteSongs(int count = 250)
        {
            var result = new List<Song>();

            using (var db = new ApplicationDbContext())
            {
                var myListenSongs = db.ListenedSong;

                foreach (var listenSong in myListenSongs)
                {
                    var song = GetSongById(db, listenSong.SongId);
                    result.Add(song);
                }
            }

            return result.Take(count);
        }

        public IEnumerable<Song> GetFavoriteSongs(string userId, int count = 250)
        {
            var result = new List<Song>();

            using (var db = new ApplicationDbContext())
            {
                var myListenSongs = db.ListenedSong.Where(m => m.UserId == userId);

                foreach (var listenSong in myListenSongs)
                {
                    var song = GetSongById(db, listenSong.SongId);
                    result.Add(song);
                }
            }

            return result.Take(count);
        }

        public IEnumerable<Song> GetLastAdded(int count = 250)
        {
            List<Song> result;

            using (var db = new ApplicationDbContext())
            {
                result = db.Songs.OrderByDescending(m => m.AddDate).ToList();

               
                //result.AddRange(myListenSongs.Select(=> GetSongById(db, song.SongId)));
            }

            return result.Take(count);
        }

        private Song GetSongById(ApplicationDbContext db, string songId)
        {
            Song song = db.Songs.FirstOrDefault(m => m.SongId == songId);

            return song;
        }

    }
}
