using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IMusicService
    {
        List<SongViewModel> GetSongs();
        List<SongViewModel> GetUserSongs(string userId);
        SongViewModel GetSong(string songId);
        void RemoveSong(string songId, string userId);
        void AddSongToUser(string songId, string userId);
        void ListenedSong(string songId, string userId);
        IEnumerable<SongViewModel> GetSongsUploadBy(string userId);
    }
}
