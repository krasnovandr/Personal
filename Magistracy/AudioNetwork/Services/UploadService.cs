using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Repositories;
using LastFmServices;
using TagLib;
using VkService;
using VkService.Models;
using File = TagLib.File;

namespace AudioNetwork.Services
{
    public interface IUploadService
    {
        void SaveSongFromVkUpdatePicture(SongViewModel song, string userid);
        void UploadSong(string fileExtension, string fileName, string pathSong, string songId,
            string absoluteSongCoverPath, string userId);

        void UploadConversationImage(string imagePath, string conversationId);
        void UploadUserImage(string imagePath, string userId);
        List<SongViewModel> GetSongVk(string userId, VkUserModel model);
    }
    public class UploadService : IUploadService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConversationRepository _conversationRepository;

        public UploadService(
            IMusicRepository musicRepository,
            IPlaylistRepository playlistRepository,
            IUserRepository userRepository,
            IConversationRepository conversationRepository)
        {
            _musicRepository = musicRepository;
            _playlistRepository = playlistRepository;
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
        }

        public void UploadSong(string fileExtension, string fileName, string pathSong, string songId, string absoluteSongCoverPath, string userId)
        {
            if (string.IsNullOrEmpty(fileName) == false)
            {
                fileName = fileName.Replace("\\p{Cntrl}", "");
            }

            var songPathToDb = FilePathContainer.SongVirtualPath + songId + fileExtension;
            var songAlbumPicturePathToDb = FilePathContainer.SongAlbumCoverPathRelative + songId + FilePathContainer.SongAlbumCoverFileFormat;

            string content = string.Empty;



            var saveSongCoverPath = absoluteSongCoverPath;
            var audioFile = TagLib.File.Create(pathSong);
            var songInfoFromVk = GetLyricsAndSongInfoByVK(audioFile, fileName);

            string lyrics = string.Empty;
            string titleFromVk = string.Empty;
            string artistVk = string.Empty;

            if (songInfoFromVk != null)
            {
                lyrics = songInfoFromVk.Lyrics.ToUtf8();
                titleFromVk = songInfoFromVk.Title.ToUtf8();
                artistVk = songInfoFromVk.Title.ToUtf8();
            }


            songAlbumPicturePathToDb = SongAlbumPicturePathToDb(audioFile, saveSongCoverPath, songId, songAlbumPicturePathToDb, titleFromVk, artistVk, fileName, ref content);
            GetSongTags(audioFile.Tag, titleFromVk, artistVk);
            var song = ModelConverters.ToSongFromTagModel(audioFile, songId, songPathToDb, songAlbumPicturePathToDb, content, lyrics, fileName, songInfoFromVk);

            _musicRepository.AddSong(song, userId);
        }

        private void GetSongTags(Tag tag, string titleFromVk, string artistVk)
        {
            string artist = string.Empty;
            string track = string.Empty;
            var info = new AlbumTrackInfo();
            var service = new LastFmService();
            
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
            VkSongInfoGetter get = new VkSongInfoGetter(model.Login, model.Password);
            var result = new List<SongViewModel>();
            var songs = get.GetSongs(model.FindLyrics).ToList();
            if (songs.Any())
            {
                _userRepository.UpdateUserVkInfo(userId, model.Login, model.Password);
            }
            result.AddRange(songs.Select(ModelConverters.ToSongFromVk));

            return result;
        }

        private SongInfo GetLyricsAndSongInfoByVK(File audioFile, string fileName)
        {
            var authorize = new VkAuthorization();
            var api = authorize.Authorize();
            var songLyricsAndInfoGetter = new VkSongInfoGetter(api);
            var titleEncoded = audioFile.Tag.Title.ToUtf8();
            var artistEncoded = audioFile.Tag.Artists.ConvertStringArrayToString().ToUtf8();

            if (string.IsNullOrEmpty(titleEncoded) == false)
            {
                var info = songLyricsAndInfoGetter.GetSongInfo(artistEncoded + " " + titleEncoded);
                if (info != null)
                {
                    return info;
                }

            }

            if (string.IsNullOrEmpty(fileName) == false)
            {
                var info = songLyricsAndInfoGetter.GetSongInfo(fileName);

                if (info != null)
                {
                    return info;
                }
            }

            return null;

        }


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