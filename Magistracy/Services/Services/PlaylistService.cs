using System.Collections.Generic;
using System.Linq;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Models;
using DataLayer.Repositories;

namespace Services.Services
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

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public List<PlaylistViewModel> GetMyPlaylists(string userId)
        {
            var playlists = new List<PlaylistViewModel>();

            var playListsDb = _playlistRepository.GetPlayLists(userId);
            if (playListsDb != null)
            {
                playlists.AddRange(playListsDb.Select(ModelConverters.ToPlaylistViewModel));
            }

            return playlists;
        }

        public List<PlaylistViewModel> GetPlaylists()
        {
            var playlists = new List<PlaylistViewModel>();
            var playListsDb = _playlistRepository.GetPlayLists();
            playlists.AddRange(playListsDb.Select(ModelConverters.ToPlaylistViewModel));

            return playlists;
        }

        public List<SongViewModel> GetSongs(string userId, string playlistId)
        {
            var songs = new List<SongViewModel>();
            var songsDb = _playlistRepository.GetSongs(userId, playlistId).ToList();
            songs.AddRange(songsDb.Select(ModelConverters.ToSongViewModel));

            return songs;
        }

        public void AddPlayList(string userId, PlaylistViewModel playListModel)
        {
            _playlistRepository.AddPlayList(userId, ModelConverters.ToPlaylistModel(playListModel));
        }

        public void RemovePlaylist(string userId, string playlistId)
        {
            _playlistRepository.RemovePlaylist(userId, playlistId);
        }

        public void SaveCurrentPlaylist(string userId, string currentPlayList, List<Song> playlistSongs)
        {
            _playlistRepository.SaveCurrentPlaylist(userId, currentPlayList, playlistSongs);
        }

        public bool RemoveSongFromPlaylist(string songId, string playlistId)
        {
          return  _playlistRepository.RemoveSong(songId, playlistId);
        }
    }
}