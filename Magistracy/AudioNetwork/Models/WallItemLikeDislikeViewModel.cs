using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioNetwork.Models
{
    public class WallItemLikeDislikeViewModel
    {
        public UserViewModel User { get; set; }
        public bool Like { get; set; }
        public bool DisLike { get; set; }
        public DateTime? Date { get; set; }
    }
}