using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IWallRepository
    {
        List<WallItem> GetWall(string userId);
        WallItem GetWallIem(string userId, int wallItemId);
        void AddWallItem(string userId, WallItem wallItem, IEnumerable<string> songsId, string pictureId);
        void RemoveWallItem(string userId, int wallItemId);
        List<Song> GetWallItemSongs(string userId, int wallItemId);
        void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike);
        List<WallItemLikeDislike> GetWallItemLikeDislikes(int wallItemId);
        string GetWallItemImage(int wallItemId);
    }
}
