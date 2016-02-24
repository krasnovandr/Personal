using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IWallService
    {
        List<WallItemViewModel> GetWall(string userId);
        WallItemViewModel GetWallItem(string userId, int wallItemId);
        void AddWallItem(WallItemViewModel wallItemView);
        void RemoveWallItem(string userId, int wallItemId);
        string GetWallItemImage(int wallItemId);
        IEnumerable<WallItemViewModel> GetUserNews(string userId);

        IEnumerable<FriendUpdateViewModel> GetFriendUpdates(string userId);
        void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike);
        List<WallItemLikeDislikeViewModel> GetWallItemLikeDislikes(int wallItemId);
    }

}
