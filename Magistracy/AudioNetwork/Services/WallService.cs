using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using DataLayer.Repositories;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace AudioNetwork.Services
{
    public interface IWallService
    {
        List<WallItemViewModel> GetWall(string userId);
        WallItemViewModel GetWallItem(string userId, int wallItemId);
        void AddWallItem(WallItemViewModel wallItemView);
        void RemoveWallItem(string userId, int wallItemId);
        string GetWallItemImage(int wallItemId);
        IEnumerable<WallItemViewModel> GetUserNews(string userId);

        IEnumerable<FriendUpdateViewModel> GetFriendUpdates(string userId);
        void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike);
        List<WallItemLikeDislikeViewModel> GetWallItemLikeDislikes(int wallItemId);
    }

    public class WallService : IWallService
    {
        private readonly IWallRepository _wallRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMusicService _musicService;

        private string _curentId;
        private string _curentHeader;

        public WallService(
            IWallRepository wallRepository,
            IUserRepository userRepository,
            IMusicService musicService,
            IUserService userService)
        {
            _wallRepository = wallRepository;
            _musicService = musicService;
            _userService = userService;
            _userRepository = userRepository;
        }

        public void SetLikeDislike(int wallItemId, string userId, bool like, bool dislike)
        {
            _wallRepository.SetLikeDislike(wallItemId, userId, like, dislike);
        }

        public List<WallItemLikeDislikeViewModel> GetWallItemLikeDislikes(int wallItemId)
        {
            var result = new List<WallItemLikeDislikeViewModel>();
            var resultFromDb = _wallRepository.GetWallItemLikeDislikes(wallItemId);

            foreach (var item in resultFromDb)
            {
                result.Add(new WallItemLikeDislikeViewModel
                {
                    Date = item.Date,
                    User = ModelConverters.ToUserViewModel(_userRepository.GetUser(item.UserId)),
                    Like = item.Like,
                    DisLike = item.DisLike,
                });
            }

            return result;
        }

        public List<WallItemViewModel> GetWall(string userId)
        {

            var wall = _wallRepository.GetWall(userId);
            var wallView = new List<WallItemViewModel>();
            wallView.AddRange(wall.Select(ModelConverters.ToWallItemViewModel));
            foreach (var wallItem in wallView)
            {
                wallItem.AddByUser = ModelConverters.ToUserViewModel(_userRepository.GetUser(wallItem.AddByUserId));
                wallItem.ItemSongs = new List<SongViewModel>();
                wallItem.ItemSongs.AddRange(_wallRepository.GetWallItemSongs(userId, wallItem.WallItemId).Select(ModelConverters.ToSongViewModel));
                wallItem.ImagePath = this.GetWallItemImage(wallItem.WallItemId);
                wallItem.LikesList = GetWallItemLikeDislikes(wallItem.WallItemId);
            }
            return wallView.OrderByDescending(m => m.AddDate).ToList();
        }

        public WallItemViewModel GetWallItem(string userId, int wallItemId)
        {
            var wallItem = ModelConverters.ToWallItemViewModel(_wallRepository.GetWallIem(userId, wallItemId));
            wallItem.AddByUser = ModelConverters.ToUserViewModel(_userRepository.GetUser(wallItem.AddByUserId));

            return wallItem;

        }

        private bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
        public void AddWallItem(WallItemViewModel wallItemView)
        {
            var isUri = false;
            if (string.IsNullOrEmpty(wallItemView.Note) == false)
            {
                isUri = IsUrlValid(wallItemView.Note);
            }


            var songIds = new List<string>();
            if (wallItemView.ItemSongs != null)
            {
                songIds = wallItemView.ItemSongs.Select(m => m.SongId).ToList();
            }

            var imagePath = string.Empty;
            if (isUri)
            {
                _curentId = Guid.NewGuid() + FilePathContainer.SongAlbumCoverFileFormat;
                Capture(wallItemView.Note);
                wallItemView.Header = _curentHeader;
                imagePath = FilePathContainer.WallPictureRelative + _curentId;
            }
            _wallRepository.AddWallItem(wallItemView.IdUserWall, ModelConverters.ToWallItemModel(wallItemView), songIds, imagePath);

        }

        public void RemoveWallItem(string userId, int wallItemId)
        {
            _wallRepository.RemoveWallItem(userId, wallItemId);
        }

        public string GetWallItemImage(int wallItemId)
        {
            return _wallRepository.GetWallItemImage(wallItemId);
        }

        public IEnumerable<WallItemViewModel> GetUserNews(string userId)
        {
            var result = new List<WallItemViewModel>();
            var friends = _userRepository.GetFriends(userId);

            foreach (var friend in friends)
            {
                var wall = GetWall(friend.Id);
                foreach (var wallItem in wall)
                {
                    if (wallItem.AddByUserId == friend.Id)
                    {
                        result.Add(wallItem);
                    }
                }

            }

            return result.OrderByDescending(m => m.AddDate);
        }

        public IEnumerable<FriendUpdateViewModel> GetFriendUpdates(string userId)
        {
            var result = new List<FriendUpdateViewModel>();
            var friends = _userService.GetFriends(userId);

            foreach (var friend in friends)
            {
                var friendSongs = _musicService.GetSongsUploadBy(friend.Id);
                var groups = friendSongs.GroupBy(y => (int)(y.AddDate.Ticks / TimeSpan.TicksPerMinute / 5)).ToList();

                foreach (var group in groups)
                {
                    var songs = group.ToList();
                    var song = songs.FirstOrDefault();
                    if (song == null)
                    {
                        continue;
                    }

                    result.Add(new FriendUpdateViewModel
                    {
                        Friend = friend,
                        AddDate = song.AddDate,
                        Songs = songs
                    });


                }
            }
            return result.OrderByDescending(m => m.AddDate);

        }



        protected void Capture(string url)
        {
            var thread = new Thread(delegate()
            {
                using (var browser = new WebBrowser())
                {
                    browser.ScriptErrorsSuppressed = true;
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = false;
                    browser.Navigate(url);
                    browser.Width = 640;
                    browser.Height = 480;
                    browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser == null)
            {
                return;
            }

            _curentHeader = browser.DocumentTitle;

            using (var bitmap = new Bitmap(browser.Width, browser.Height))
            {
                browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
                bitmap.Save(HostingEnvironment.MapPath(FilePathContainer.WallPicturePhysicalPath) + _curentId);
            }
        }
    }
}