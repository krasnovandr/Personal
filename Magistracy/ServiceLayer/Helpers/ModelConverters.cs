using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AudioNetwork.Models;
using AutoMapper;
using DataLayer.Models;
using DataLayer.Repositories;
using TagLib;
using VkService.Models;

namespace AudioNetwork.Helpers
{
    public static class ModelConverters
    {

        public static List<SongViewModel> ToSongViewModelList(List<Song> songsDb)
        {
            var result = new List<SongViewModel>();
            if (songsDb == null)
            {
                return result;
            }

            result.AddRange(songsDb.Select(ModelConverters.ToSongViewModel));

            return result;
        }

        public static List<UserViewModel> ToUserViewModelList(List<ApplicationUser> usersDb)
        {
            var result = new List<UserViewModel>();
            if (usersDb == null)
            {
                return result;
            }

            result.AddRange(usersDb.Select(ModelConverters.ToUserViewModel));

            return result;
        }

        public static UserViewModel ToUserViewModel(ApplicationUser user)
        {
            Mapper.CreateMap<ApplicationUser, UserViewModel>();
            var userView = Mapper.Map<ApplicationUser, UserViewModel>(user);

            return userView;
        }

        public static ApplicationUser ToApplicationUser(UserViewModel user)
        {
            Mapper.CreateMap<UserViewModel, ApplicationUser>();
            return Mapper.Map<UserViewModel, ApplicationUser>(user);
        }

        public static SongViewModel ToSongViewModel(Song song)
        {
            Mapper.CreateMap<Song, SongViewModel>();
            var result = Mapper.Map<Song, SongViewModel>(song);
            if (result != null && result.Duration != default(TimeSpan))
            {
                result.DurationFormatted = result.Duration.ToString(@"mm\:ss");

            }

            return result;
        }

        public static Song ToSongModel(SongViewModel song)
        {
            Mapper.CreateMap<SongViewModel, Song>();
            return Mapper.Map<SongViewModel, Song>(song);
        }

        public static PlaylistViewModel ToPlaylistViewModel(Playlist song)
        {
            Mapper.CreateMap<Playlist, PlaylistViewModel>();
            return Mapper.Map<Playlist, PlaylistViewModel>(song);
        }


        public static Playlist ToPlaylistModel(PlaylistViewModel playlist)
        {
            Mapper.CreateMap<PlaylistViewModel, Playlist>();
            return Mapper.Map<PlaylistViewModel, Playlist>(playlist);
        }

        public static ConversationViewModel ToConversationViewModel(Conversation conversation)
        {
            Mapper.CreateMap<Conversation, ConversationViewModel>();
            return Mapper.Map<Conversation, ConversationViewModel>(conversation);
        }

        public static Conversation ToConversationModel(ConversationViewModel conversationViewModel)
        {
            Mapper.CreateMap<ConversationViewModel, Conversation>();
            return Mapper.Map<ConversationViewModel, Conversation>(conversationViewModel);
        }


        public static Message ToMessageModel(MessageViewModel messageViewModel)
        {
            Mapper.CreateMap<MessageViewModel, Message>();
            return Mapper.Map<MessageViewModel, Message>(messageViewModel);
        }


        public static MessageViewModel ToMessageViewModel(Message message)
        {
            Mapper.CreateMap<Message, MessageViewModel>();
            return Mapper.Map<Message, MessageViewModel>(message);
        }

        public static WallItemViewModel ToWallItemViewModel(WallItem wallItem)
        {
            Mapper.CreateMap<WallItem, WallItemViewModel>();
            return Mapper.Map<WallItem, WallItemViewModel>(wallItem);
        }


        public static WallItem ToWallItemModel(WallItemViewModel wallItemView)
        {
            Mapper.CreateMap<WallItemViewModel, WallItem>();
            return Mapper.Map<WallItemViewModel, WallItem>(wallItemView);
        }

        static string ConvertStringArrayToString(string[] array)
        {
            var builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }

        public static SongViewModel ToSongFromVk(SongInfo song)
        {
            Mapper.CreateMap<SongInfo, SongViewModel>();
            var result = Mapper.Map<SongInfo, SongViewModel>(song);
            if (result.Duration != default(TimeSpan))
            {
                result.DurationFormatted = result.Duration.ToString(@"mm\:ss");

            }
            return result;
        }

        public static Song ToSongFromTagModel(File audioFile, string songId, string songPath, string songAlbumCoverPath, string albumInfoContent, string lyrics, string fileName, SongInfo songInfoFromVk)
        {
            return new Song
            {
                Year = audioFile.Tag.Year.ToString(),
                Artist = string.IsNullOrEmpty(ConvertStringArrayToString(audioFile.Tag.Artists).ToUtf8()) ? songInfoFromVk.Artist : ConvertStringArrayToString(audioFile.Tag.Artists).ToUtf8(),
                Genre = string.IsNullOrEmpty(ConvertStringArrayToString(audioFile.Tag.Genres).ToUtf8()) ? songInfoFromVk.Genre : ConvertStringArrayToString(audioFile.Tag.Genres).ToUtf8(),
                Album = audioFile.Tag.Album.ToUtf8(),
                AddDate = DateTime.Now,
                SongId = songId,
                BitRate = audioFile.Properties.AudioBitrate,
                Duration = audioFile.Properties.Duration,
                Title = string.IsNullOrEmpty(audioFile.Tag.Title.ToUtf8()) ? songInfoFromVk.Title.ToUtf8() : audioFile.Tag.Title.ToUtf8(),
                SongPath = songPath,
                SongAlbumCoverPath = songAlbumCoverPath,
                AlbumAndTrackInfo = albumInfoContent,
                Copyright = audioFile.Tag.Copyright,
                DiscCount = (int)audioFile.Tag.DiscCount,
                Composers = ConvertStringArrayToString(audioFile.Tag.Composers).ToUtf8(),
                Lyrics = lyrics.ToUtf8(),
                Disc = (int)audioFile.Tag.Disc,
                Performers = ConvertStringArrayToString(audioFile.Tag.Performers).ToUtf8(),
                FileName = fileName,

            };
        }
    }
}