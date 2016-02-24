using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPlaylistService
    {
        List<PlaylistViewModel> GetMyPlaylists(string userId);
        List<PlaylistViewModel> GetPlaylists();
        List<SongViewModel> GetSongs(string userId, string playlistId);
        void AddPlayList(string userId, PlaylistViewModel playListModel);
        void RemovePlaylist(string userId, string playlistId);
        void SaveCurrentPlaylist(string userId, string currentPlayList, List<Song> playlistSongs);
        bool RemoveSongFromPlaylist(string songId, string s);
    }
}
