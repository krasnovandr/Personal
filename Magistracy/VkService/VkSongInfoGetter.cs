using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VkNet;
using VkNet.Model.Attachments;
using VkService.Models;

namespace VkService
{
    public class VkSongInfoGetter
    {
        private readonly VkApi _vkApi;

        public VkSongInfoGetter(VkApi vkApi)
        {
            this._vkApi = vkApi;
        }

        public VkSongInfoGetter(string login, string password)
        {
            var vkAuthorization = new VkAuthorization(login, password);
            _vkApi = vkAuthorization.Authorize();
        }


        public string GetSongLyrics(long? lyricsId)
        {
            if (lyricsId.HasValue == false)
            {
                return string.Empty;
            }

            var lyrics = _vkApi.Audio.GetLyrics(lyricsId.Value);

            return lyrics != null ? lyrics.Text : string.Empty;
        }

        public SongInfo GetSongInfo(string songTitle)
        {
            int totalCount;
            var audios = _vkApi.Audio.Search(songTitle, out totalCount, true, findLyrics: true);


            var foundAudio = audios.FirstOrDefault();
            var info = new SongInfo();

            if (foundAudio != null)
            {
                info.Artist = foundAudio.Artist;
                info.Genre = foundAudio.Genre.ToString();
                info.Lyrics = GetSongLyrics(foundAudio.LyricsId);
                info.SongPath = foundAudio.Url.AbsoluteUri;
                info.Title = foundAudio.Title;
                GetLyricsFromAllAudios(audios, info);

                return info;
            }



            return null;
        }

        public List<SongInfo> GetSongs(bool findLyrics)
        {
            List<SongInfo> songs = new List<SongInfo>();
            if (_vkApi == null)
            {
                return songs;
            }

            var userId = _vkApi.UserId;

            var lyricGetter = new VkSongInfoGetter(_vkApi);
            if (userId != null)
            {
                var vkSongs = _vkApi.Audio.Get((long)userId);
                //vkSongs = vkSongs.Take(5);
                foreach (var vkSong in vkSongs)
                {
                    var songInfo = new SongInfo
                    {
                        Artist = vkSong.Artist,
                        Genre = vkSong.Genre.ToString(),
                        SongId = Guid.NewGuid().ToString(),
                        SongPath = vkSong.Url.AbsoluteUri,

                        Title = vkSong.Title,
                    };
                    songInfo.Duration = TimeSpan.FromSeconds(vkSong.Duration);
                    if (findLyrics)
                    {
                        songInfo.Lyrics = lyricGetter.GetSongLyrics(vkSong.LyricsId);
                    }

                    songs.Add(songInfo);
                }
            }
            return songs;
        }

        private void GetLyricsFromAllAudios(IEnumerable<Audio> audios, SongInfo info)
        {
            foreach (var audio in audios)
            {
                var lyrics = GetSongLyrics(audio.LyricsId);

                if (string.IsNullOrEmpty(lyrics) == false && lyrics.Length > 300)
                {
                    info.Lyrics = lyrics;
                    break;
                }


                //if (string.IsNullOrEmpty(audio.Title) == false && string.IsNullOrEmpty(info.Title))
                //{
                //    info.Title = audio.Title;
                //}

                //if (string.IsNullOrEmpty(audio.Genre.ToString()) == false && string.IsNullOrEmpty(info.Genre))
                //{
                //    info.Genre = audio.Genre.ToString();
                //}

                //if (string.IsNullOrEmpty(audio.Artist) == false && string.IsNullOrEmpty(info.Artist))
                //{
                //    info.Artist = audio.Artist;
                //}
            }
        }
    }
}
