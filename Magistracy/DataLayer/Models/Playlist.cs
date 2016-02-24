using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Playlist
    {

        public Playlist()
        {
            //PlaylistItem = new List<PlaylistItem>();
        }
        public string PlaylistId { get; set; }
        public string PlayListName { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public bool DefaultPlaylist { get; set; }
        //public virtual ICollection<PlaylistItem> PlaylistItem { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        //public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
