using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
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
}
