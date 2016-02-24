using System.IO;
using System.Linq;
using AudioNetwork.Helpers;
using LastFmServices;
using TagLib;

namespace Services.Services
{
    public static class SongPictureGetter
    {
        public static AlbumTrackInfo GetPictureByWebService(Tag tag, string titleVk, string artistVk, string fileName)
        {
            string artist = string.Empty;
            string album = string.Empty;
            string track = string.Empty;
            var info = new AlbumTrackInfo();

            if (tag != null)
            {
                if (string.IsNullOrEmpty(tag.Album) == false)
                {
                    album = tag.Album.ToUtf8();
                }


                if (string.IsNullOrEmpty(tag.Title) == false)
                {
                    track = tag.Title.ToUtf8();
                }


                if (string.IsNullOrEmpty(tag.Artists.FirstOrDefault()) == false)
                {
                    var artistWithoutEncoding = tag.Artists.FirstOrDefault();
                    if (artistWithoutEncoding != null)
                    {
                        artist = artistWithoutEncoding.ToUtf8();
                    }

                }

                var albumTrackInfoByTag = CheckContent(artist, album, track);
                if (albumTrackInfoByTag != null)
                {
                    return albumTrackInfoByTag;
                }
            }
     


           var  albumTrackInfo = CheckContent(artistVk.ToUtf8(), "", titleVk.ToUtf8());

            if (albumTrackInfo != null)
            {
                return albumTrackInfo;
            }

            albumTrackInfo = CheckContent("", "", fileName);

            if (albumTrackInfo != null)
            {
                return albumTrackInfo;
            }


            info.PicturePath = FilePathContainer.SongAlbumCoverPathPhysical + "default_song_album.jpg";
            return info;
            //songAlbumPicturePath = TagsInformationGetter.SongAlbumPicturePath(audioFile, lastFm, songAlbumPicturePath, ref albumInfoContent);

            //ImageSaver.SongAlbumCoverPath + "default_song_album.jpg";
        }

        public static void GetAndSavePictureByTag(Tag tag, string songCoverPath, string songId)
        {
            var ms = new MemoryStream(tag.Pictures[0].Data.Data);
            var image = Image.FromStream(ms);
            var pathSongAlbumCover = songCoverPath + songId + ".jpg";
            image.Save(pathSongAlbumCover);
        }

        private static AlbumTrackInfo CheckContent(string artist, string album, string track)
        {
            var lastFm = new LastFmService();

            if (string.IsNullOrEmpty(artist) == false && string.IsNullOrEmpty(album) == false)
            {
                AlbumTrackInfo info = lastFm.getAlbumInfo(artist, album);

                if (info != null)
                {
                    if (string.IsNullOrEmpty(info.PicturePath) == false)
                    {
                        {
                            return info;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(artist) == false && string.IsNullOrEmpty(track) == false)
            {
                AlbumTrackInfo info = info = lastFm.getTrackInfo(artist, track);

                if (info != null)
                {
                    if (string.IsNullOrEmpty(info.PicturePath) == false)
                    {
                        {
                            return info;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(track) == false)
            {
                AlbumTrackInfo info = info = lastFm.searchTrackInfo(track);

                if (info != null)
                {
                    if (string.IsNullOrEmpty(info.PicturePath) == false)
                    {
                        {
                            return info;
                        }
                    }
                }
            }

            return null;
        }


    }
}