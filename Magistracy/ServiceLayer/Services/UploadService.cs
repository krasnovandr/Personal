using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using DataLayer.Models;
using LastFmServices;
using MusicRecognition.Interfaces;
using MusicRecognition.Models.ResultModels;
using ServiceLayer.Helpers;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using TagLib;
using VkService;
using VkService.Models;
using File = TagLib.File;

namespace ServiceLayer.Services
{
    public class UploadService : IUploadService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IRecognitionService _recognitionService;

        public UploadService(
            IMusicRepository musicRepository,
            IPlaylistRepository playlistRepository,
            IUserRepository userRepository,
            IConversationRepository conversationRepository,
            IRecognitionService recognitionService)
        {
            _musicRepository = musicRepository;
            _playlistRepository = playlistRepository;
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
            _recognitionService = recognitionService;
        }

        public void UploadSong(string fileExtension, string fileName, string pathSong, string songId, string absoluteSongCoverPath, string userId)
        {
            if (string.IsNullOrEmpty(fileName) == false)
            {
                fileName = fileName.Replace("\\p{Cntrl}", "");
            }

            var songPathToDb = FilePathContainer.SongVirtualPath + songId + fileExtension;
            var songAlbumPicturePathToDb = FilePathContainer.SongAlbumCoverPathRelative + songId + FilePathContainer.SongAlbumCoverFileFormat;
            var song = new Song
            {
                AddDate = DateTime.Now,
                SongId = songId,
                SongPath = songPathToDb,
                FileName = fileName,
            };
            var audioFile = TagLib.File.Create(pathSong);

            song.Year = audioFile.Tag.Year.ToString();
            song.BitRate = audioFile.Properties.AudioBitrate;
            song.Duration = audioFile.Properties.Duration;
            song.DiscCount = (int)audioFile.Tag.DiscCount;
            song.Composers = audioFile.Tag.Composers.ConvertStringArrayToString().ToUtf8();
            song.Disc = (int)audioFile.Tag.Disc;
            song.Performers = audioFile.Tag.Performers.ConvertStringArrayToString().ToUtf8();

            song = Recognise(pathSong, song);
            bool recogniseFailed = CheckRecogniseFailed(song);
            if (recogniseFailed)
            {
                song.Artist = string.Join(",", audioFile.Tag.AlbumArtists);
                song.Genre = string.Join(",", audioFile.Tag.Genres);
                song.Album = audioFile.Tag.Album.ToUtf8();
                song.Title = audioFile.Tag.Title.ToUtf8();
            }

            if (audioFile.Tag.Pictures.Any())
            {
                song.SongAlbumCoverPath = songAlbumPicturePathToDb;
                SongPictureGetter.GetAndSavePictureByTag(audioFile.Tag, absoluteSongCoverPath, songId);
            }
            else
            {
                var pictureInfo = SongPictureGetter.CheckContent(song.Artist, song.Album, song.Title);
                if (pictureInfo != null)
                {
                    song.SongAlbumCoverPath = pictureInfo.PicturePath;
                }
            }


            var vkaudioService = new VkAudioService();
            song.Lyrics = vkaudioService.GetSongLyrics(song.Title, song.Artist);



            //var saveSongCoverPath = absoluteSongCoverPath;


            //var title = audioFile.Tag.Title.ToUtf8();
            //var artist = audioFile.Tag.AlbumArtists.ConvertStringArrayToString().ToUtf8();

            //if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(artist))
            //{

            //}


            //string lyrics = string.Empty;
            //string titleFromVk = string.Empty;
            //string artistVk = string.Empty;

            //if (songInfoFromVk != null)
            //{
            //    lyrics = songInfoFromVk.Lyrics.ToUtf8();
            //    titleFromVk = songInfoFromVk.Title.ToUtf8();
            //    artistVk = songInfoFromVk.Title.ToUtf8();
            //}


            //songAlbumPicturePathToDb = SongAlbumPicturePathToDb(audioFile, saveSongCoverPath, songId, songAlbumPicturePathToDb, titleFromVk, artistVk, fileName, ref content);
            //GetSongTags(audioFile.Tag, titleFromVk, artistVk);






            //    ModelConverters.ToSongFromTagModel(audioFile, songId, songPathToDb, songAlbumPicturePathToDb);

            _musicRepository.AddSong(song, userId);
        }

        private bool CheckRecogniseFailed(Song song)
        {
            bool recogniseNeeded =
                string.IsNullOrEmpty(song.Artist)
                || string.IsNullOrEmpty(song.Genre)
                || string.IsNullOrEmpty(song.Title)
                || string.IsNullOrEmpty(song.Album);


            return recogniseNeeded;
        }


        //public static Song ToSongFromTagModel(File audioFile, string songId, string songPath, string songAlbumCoverPath, string albumInfoContent, string lyrics, string fileName, SongInfo songInfoFromVk)
        //{
        //    return new Song
        //    {
        //        Year = audioFile.Tag.Year.ToString(),
        //        Artist = string.IsNullOrEmpty(ConvertStringArrayToString(audioFile.Tag.Artists).ToUtf8()) ? songInfoFromVk.Artist : ConvertStringArrayToString(audioFile.Tag.Artists).ToUtf8(),
        //        Genre = string.IsNullOrEmpty(ConvertStringArrayToString(audioFile.Tag.Genres).ToUtf8()) ? songInfoFromVk.Genre : ConvertStringArrayToString(audioFile.Tag.Genres).ToUtf8(),
        //        Album = audioFile.Tag.Album.ToUtf8(),
        //        AddDate = DateTime.Now,
        //        SongId = songId,
        //        BitRate = audioFile.Properties.AudioBitrate,
        //        Duration = audioFile.Properties.Duration,
        //        Title = string.IsNullOrEmpty(audioFile.Tag.Title.ToUtf8()) ? songInfoFromVk.Title.ToUtf8() : audioFile.Tag.Title.ToUtf8(),
        //        SongPath = songPath,
        //        SongAlbumCoverPath = songAlbumCoverPath,
        //        AlbumAndTrackInfo = albumInfoContent,
        //        Copyright = audioFile.Tag.Copyright,
        //        DiscCount = (int)audioFile.Tag.DiscCount,
        //        Composers = ConvertStringArrayToString(audioFile.Tag.Composers).ToUtf8(),
        //        Lyrics = lyrics.ToUtf8(),
        //        Disc = (int)audioFile.Tag.Disc,
        //        Performers = ConvertStringArrayToString(audioFile.Tag.Performers).ToUtf8(),
        //        FileName = fileName,

        //    };
        //}

        public Song Recognise(string pathSong, Song song)
        {
            double totalDuration = 0;
            if (song.Duration.HasValue)
            {
                totalDuration = song.Duration.Value.TotalSeconds;
            }
            if (string.IsNullOrEmpty(pathSong) == false)
            {
                var resultJson = _recognitionService.Recognise(pathSong, (int)totalDuration / 2, 20);
                if (resultJson != null)
                {
                    var songData = resultJson.metadata.music.FirstOrDefault();
                    if (songData != null)
                    {
                        if (songData.artists != null)
                        {
                            song.Artist = string.Join(",", songData.artists.Select(m => m.Name));
                        }

                        if (songData.album != null)
                        {
                            song.Album = songData.album.Name;
                        }

                        if (songData.genres != null)
                        {
                            song.Genre = string.Join(",", songData.genres.Select(m => m.Name));
                        }

                        song.Title = songData.title;
                        song.Copyright = songData.label;
                        song.Duration = TimeSpan.FromMilliseconds(Convert.ToDouble(songData.duration_ms));
                    }
                }
            }


            return song;
        }

        public void SaveSongFromVkUpdatePicture(SongViewModel song, string userid)
        {
            var songInfo = SongPictureGetter.GetPictureByWebService(null, song.Title, song.Artist, "");
            if (songInfo != null)
            {
                song.SongAlbumCoverPath = songInfo.PicturePath;
            }
            TimeSpan temp;
            TimeSpan.TryParse(song.DurationFormatted, out temp);
            song.Duration = temp;
            _playlistRepository.AddToVkPlayList(userid, ModelConverters.ToSongModel(song));
        }

        public void UploadUserImage(string imagePath, string userId)
        {
            _userRepository.AddImage(imagePath, userId);
        }

        public void UploadConversationImage(string imagePath, string conversationId)
        {
            _conversationRepository.AddImage(imagePath, conversationId);
        }

        public List<SongViewModel> GetSongVk(string userId, VkUserModel model)
        {
            var audioService = new VkAudioService(model.Login, model.Password);
            var result = new List<SongViewModel>();
            var songs = audioService.GetSongs(model.FindLyrics).ToList();
            if (songs.Any())
            {
                _userRepository.UpdateUserVkInfo(userId, model.Login, model.Password);
            }
            result.AddRange(songs.Select(ModelConverters.ToSongFromVk));

            return result;
        }

        //private SongInfo GetLyricsAndSongInfoByVK(File audioFile, string fileName)
        //{
        //    var authorize = new VkAuthorization();
        //    var api = authorize.Authorize();
        //    var songLyricsAndInfoGetter = new IVkAudioService(api);
        //    var titleEncoded = audioFile.Tag.Title.ToUtf8();
        //    var artistEncoded = audioFile.Tag.Artists.ConvertStringArrayToString().ToUtf8();

        //    if (string.IsNullOrEmpty(titleEncoded) == false)
        //    {
        //        var info = songLyricsAndInfoGetter.GetSongInfo(artistEncoded + " " + titleEncoded);
        //        if (info != null)
        //        {
        //            return info;
        //        }

        //    }

        //    if (string.IsNullOrEmpty(fileName) == false)
        //    {
        //        var info = songLyricsAndInfoGetter.GetSongInfo(fileName);

        //        if (info != null)
        //        {
        //            return info;
        //        }
        //    }

        //    return null;

        //}


        private string SongAlbumPicturePathToDb(File audioFile, string saveSongCoverPath, string songId,
            string songAlbumPicturePathToDb, string titleVk, string artistVk, string filename, ref string content)
        {
            if (audioFile.Tag.Pictures.Any())
            {
                SongPictureGetter.GetAndSavePictureByTag(audioFile.Tag, saveSongCoverPath, songId);
            }
            else
            {
                var trackInfo = SongPictureGetter.GetPictureByWebService(audioFile.Tag, titleVk, artistVk, filename);

                if (trackInfo != null)
                {
                    songAlbumPicturePathToDb = trackInfo.PicturePath;
                    content = trackInfo.Content;
                }
            }
            return songAlbumPicturePathToDb;
        }



    }
}