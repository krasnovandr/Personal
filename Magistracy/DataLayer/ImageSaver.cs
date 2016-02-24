using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class ImageSaver
    {
        public static string ImagePathRelative
        {
            get
            {
                return "~/Content/Images/";
            }
        }

        public static string ImagePath
        {
            get
            {
                return "Content/Images/";
            }
        }

        public static string SongPath
        {
            get
            {
                return "Content/Songs/";
            }
        }

        public static string SongAlbumCoverPath
        {
            get
            {
                return "Content/SongsAlbumCovers/";
            }
        }
    }
}
