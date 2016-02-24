using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Models;

namespace AudioNetwork.Models
{
    public class WallItemViewModel
    {
        public int WallItemId { get; set; }
        public string Note { get; set; }
        public DateTime AddDate { get; set; }
        public string AddByUserId { get; set; }
        public string IdUserWall { get; set; }
        public string ImagePath { get; set; }

        public string AddDateFormatted
        {
            get
            {
                return AddDate.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public List<SongViewModel> ItemSongs { get; set; }
        public UserViewModel AddByUser { get; set; }
        public string Header { get; set; }

        public List<WallItemLikeDislikeViewModel> LikesList { get; set; }

        public int LikesCount
        {
            get
            {
                return LikesList == null ? 0 : LikesList.Count(m => m.Like);
            }
        }

        public int DislikesCount
        {
            get
            {
                return LikesList == null ? 0 : LikesList.Count(m => m.DisLike);
            }
        }

        //public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}