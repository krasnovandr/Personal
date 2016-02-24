using System;

namespace AudioNetwork.Models
{
    public class UserSearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Genres { get; set; }
        public string Atrists { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public DateTime LastActivity { get; set; }
        public string LastActivityDate
        {
            get
            {
                return this.LastActivity.ToString("dd/MM/yyyy HH:mm");
            }
        }
        public string Email { get; set; }
        public string BestGenres { get; set; }
        public string BestAtrists { get; set; }
        public bool YourFriend { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthDateFormatted
        {
            get
            {
                return this.BirthDate.ToString("dd/MM/yyyy");
            }
        }



        public bool IsOnline
        {
            get
            {
                return LastActivity.AddMinutes(5) > DateTime.Now;
            }
        }
        public string AvatarFilePath { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string BestGenre { get; set; }
        public string WorstGenre { get; set; }
        public string BestForeignArtist { get; set; }
        public string BestNativeArtist { get; set; }
        public string LastEntrenchedSong { get; set; }
        public string BestVocalist { get; set; }
        public string BestCinemaSoundtrack { get; set; }
        public string BestGameSoundtrack { get; set; }
        public string BestAlarmClock { get; set; }
        public string BestSleeping { get; set; }
        public string BestRelaxSong { get; set; }
        public string SongAtThisMoment { get; set; }
        public string VkLogin { get; set; }
        public string VkPassword { get; set; }
        public SongViewModel CurrentSong { get; set; }
        public bool LoggedIn { get; set; }
    }
}