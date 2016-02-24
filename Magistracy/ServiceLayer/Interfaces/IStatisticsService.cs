using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IStatisticsService
    {
        IEnumerable<SongViewModel> GetFavouriteSongs(string userId);
        IEnumerable<SongViewModel> GetFavouriteSongs();
        IEnumerable<SongViewModel> GetLastListenedSongs(string userId);
        IEnumerable<SongViewModel> GetLastAdded();
        IEnumerable<SongChartModel> GetChartData(string songId);
    }
}
