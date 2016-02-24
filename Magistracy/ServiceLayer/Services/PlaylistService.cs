using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Helpers;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{


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