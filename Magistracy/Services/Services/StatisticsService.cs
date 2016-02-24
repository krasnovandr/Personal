using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Repositories;

namespace AudioNetwork.Services
{
    public interface IStatisticsService
    {
        IEnumerable<SongViewModel> GetFavouriteSongs(string userId);
        IEnumerable<SongViewModel> GetFavouriteSongs();
        IEnumerable<SongViewModel> GetLastListenedSongs(string userId);
        IEnumerable<SongViewModel> GetLastAdded();
        IEnumerable<SongChartModel> GetChartData(string songId);
    }

    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMusicRepository _musicRepository;

        public StatisticsService(
            IStatisticsRepository statisticsRepository,
            IMusicRepository musicRepository)
        {
            _statisticsRepository = statisticsRepository;
            _musicRepository = musicRepository;
        }

        public IEnumerable<SongViewModel> GetFavouriteSongs(string userId)
        {
            var songs = _statisticsRepository.GetFavoriteSongs(userId).ToList();
            var resultGroups = ModelConverters.ToSongViewModelList(songs).GroupBy(m => m.SongId);
            var result = new List<SongViewModel>();
            foreach (var group in resultGroups)
            {
                var song = group.FirstOrDefault();
                if (song != null)
                {
                    song.ListenCount = group.Count();
                    result.Add(song);
                }
            }
            return result;
        }

        public IEnumerable<SongViewModel> GetFavouriteSongs()
        {
            var songs = _statisticsRepository.GetFavoriteSongs().ToList();
            var resultGroups = ModelConverters.ToSongViewModelList(songs).GroupBy(m => m.SongId);
            var result = new List<SongViewModel>();
            foreach (var group in resultGroups)
            {
                var song = group.FirstOrDefault();
                if (song != null)
                {
                    song.ListenCount = group.Count();
                    result.Add(song);
                }
            }
            return result;
        }

        public IEnumerable<SongViewModel> GetLastListenedSongs(string userId)
        {
            var songs = _statisticsRepository.GetLastListenedSongs(userId).ToList();
            // var resultGroups = ModelConverters.ToSongViewModelList(songs).GroupBy(m => m.SongId);
            var result = new List<SongViewModel>();
            foreach (var song in songs)
            {
                var songDb = _musicRepository.GetSong(song.SongId);
                if (songDb == null)
                {
                    return result;
                }
                var songView = ModelConverters.ToSongViewModel(songDb);
                songView.LastListenDate = song.ListenDate;
                result.Add(songView);
            }

            return result.OrderByDescending(m => m.LastListenDate);
        }

        public IEnumerable<SongViewModel> GetLastAdded()
        {
            var songs = _statisticsRepository.GetLastAdded().ToList();
            return ModelConverters.ToSongViewModelList(songs).ToList().OrderByDescending(m => m.AddDate);
        }

        public IEnumerable<SongChartModel> GetChartData(string songId)
        {
            var chartData = new List<SongChartModel>();

            var songListen = _statisticsRepository.GetSongListeneInfo(songId).ToList().GroupBy(m => m.ListenDate.Day);

            foreach (var group in songListen)
            {
                var song = group.FirstOrDefault();
                if (song != null)
                {
                    var temp = new SongChartModel
                    {
                        x = song.ListenDate.ToJavaScriptMilliseconds(),
                        ListenCount = group.Count()
                    };
                    chartData.Add(temp);
                }
            }
            return chartData;
        }
    }
}