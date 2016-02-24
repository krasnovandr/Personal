using System.Web.Mvc;
using AudioNetwork.Models;
using AudioNetwork.Services;
using Microsoft.AspNet.Identity;

namespace AudioNetwork.Controllers
{
    public class MusicController : Controller
    {
        private readonly IMusicService _musicService;

        public MusicController(
            IMusicService musicService)
        {
            _musicService = musicService;
        }

        public ActionResult Player()
        {
            return View("Player");
        }

        public ActionResult ViewSong()
        {
            return View("Song");
        }

        public ActionResult ViewSongsModal()
        {
            return View("SongsModal");
        }

        public JsonResult GetSongs()
        {
            return Json(_musicService.GetSongs(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserSongs(string userId)
        {
            return Json(_musicService.GetUserSongs(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSong(string songId)
        {
            return Json(_musicService.GetSong(songId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMySongs()
        {
            var userId = User.Identity.GetUserId();
            return Json(_musicService.GetUserSongs(userId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSong(SongViewModel song)
        {
            var userId = User.Identity.GetUserId();
            _musicService.RemoveSong(song.SongId, userId);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddToMyMusic(SongViewModel song)
        {
            var userId = User.Identity.GetUserId();
            _musicService.AddSongToUser(song.SongId, userId);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListenedSong(string songId)
        {
            var userId = User.Identity.GetUserId();

            _musicService.ListenedSong(songId, userId);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadSong(string songId)
        {
            var userid = User.Identity.GetUserId();
            //foreach (var song in songs)
            //{
            var song = _musicService.GetSong(songId);
            var songPath = Server.MapPath(song.SongPath);
            //}
            return File(song.SongPath, "application/force-download", song.Artist + song.Title);
        }
    }
}