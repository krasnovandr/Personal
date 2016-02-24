using System;

namespace ServiceLayer.Models
{
    public class WallItemLikeDislikeViewModel
    {
        public UserViewModel User { get; set; }
        public bool Like { get; set; }
        public bool DisLike { get; set; }
        public DateTime? Date { get; set; }
    }
}