namespace AudioNetwork.Services
{
    public static class FilePathContainer
    {
        public static string WallPicturePhysicalPath
        {
            get
            {
                return "~/Content/WallItemsImages/";
            }
        }

        public static string WallPictureRelative
        {
            get
            {
                return "Content/WallItemsImages/";
            }
        }

        public static string ForImagePhysicalPath
        {
            get
            {
                return "~/Content/Images/";
            }
        }

        public static string ImagePathRelative
        {
            get
            {
                return "Content/Images/";
            }
        }

        public static string ForConversationCoversPhysicalPath
        {
            get
            {
                return "~/Content/ConversationCovers/";
            }
        }
        public static string ConversationCoversPathRelative
        {
            get
            {
                return "Content/ConversationCovers/";
            }
        }


        //public static string SongPathRelative
        //{
        //    get
        //    {
        //        return "~/Content/Songs/";
        //    }
        //}

        public static string SongVirtualPath
        {
            get
            {
                return "/Content/Songs/";
            }
        }

        public static string SongAlbumCoverPathPhysical
        {
            get
            {
                return "~/Content/SongsAlbumCovers/";
            }
        }


        public static string SongAlbumCoverPathRelative
        {
            get
            {
                return "Content/SongsAlbumCovers/";
            }
        }

        public static string SongAlbumCoverFileFormat
        {
            get { return ".jpg"; }
        }

        public static string SongDefaultFormat
        {
            get { return ".mp3"; }
        }
    }
}
