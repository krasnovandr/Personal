using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
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

}
