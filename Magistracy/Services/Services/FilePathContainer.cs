namespace Services.Services
{
    public static class FilePathContainer
    {
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


        public static string SongPathRelative
        {
            get
            {
                return "~/Content/Songs/";
            }
        }

        public static string ForSongPhysicalPath
        {
            get
            {
                return "Content/Songs/";
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
    }
}
