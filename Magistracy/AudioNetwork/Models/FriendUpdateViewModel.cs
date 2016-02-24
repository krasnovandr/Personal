using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioNetwork.Models
{
    public class FriendUpdateViewModel
    {
        public UserViewModel Friend { get; set; }
        public List<SongViewModel> Songs { get; set; }
        public DateTime AddDate { get; set; }

        public string DateFormatted
        {
            get { return AddDate.ToString("g"); }
        }
    }
}