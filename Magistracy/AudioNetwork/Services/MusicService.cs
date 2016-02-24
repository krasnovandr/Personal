using System.Collections.Generic;
using System.Linq;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Repositories;

namespace AudioNetwork.Services
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

    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;

        public MusicService(
            IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        public List<SongViewModel> GetSongs()
        {
            var songs = new List<SongViewModel>();
            var songsDb = _musicRepository.GetSongs();
            songs.AddRange(songsDb.Select(ModelConverters.ToSongViewModel));
            return songs;
        }

        public List<SongViewModel> GetUserSongs(string userId)
        {
            var songs = new List<SongViewModel>();
            var songsDb = _musicRepository.GetSongs(userId);
            songs.AddRange(songsDb.Select(ModelConverters.ToSongViewModel));

            return songs;
        }

        public SongViewModel GetSong(string songId)
        {
            var song = _musicRepository.GetSong(songId);
            return ModelConverters.ToSongViewModel(song);
        }

        public void RemoveSong(string songId, string userId)
        {
            _musicRepository.RemoveSong(songId, userId);
        }

        public void AddSongToUser(string songId, string userId)
        {
            _musicRepository.AddSongToUser(songId, userId);
        }

        public void ListenedSong(string songId, string userId)
        {
            _musicRepository.ListenedSong(songId, userId);
        }

        public IEnumerable<SongViewModel> GetSongsUploadBy(string userId)
        {
            var result = _musicRepository.GetSongsUploadBy(userId);

            return result.Select(ModelConverters.ToSongViewModel);
        }
    }
}