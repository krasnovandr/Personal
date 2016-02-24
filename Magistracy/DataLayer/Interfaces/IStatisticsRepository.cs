using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IStatisticsRepository
    {
        IEnumerable<ListenedSong> GetLastListenedSongs(string userId, int count = 250);
        IEnumerable<Song> GetFavoriteSongs(int count = 250);
        IEnumerable<Song> GetFavoriteSongs(string userId, int count = 250);
        IEnumerable<Song> GetLastAdded(int count = 250);
        IEnumerable<ListenedSong> GetSongListeneInfo(string songId, int count = 250);
    }
}
