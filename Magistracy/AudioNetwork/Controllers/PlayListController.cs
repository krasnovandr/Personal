using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using AudioNetwork.Services;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;

namespace AudioNetwork.Controllers
{
    public class PlayListController : Controller
    {
        private readonly IPlaylistService _playlistService;

        public PlayListController(
           IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        public ActionResult Playlists()
        {
            return View("Playlists");
        }

        public JsonResult GetMyPlaylists()
        {
            var userId = User.Identity.GetUserId();
            return Json(_playlistService.GetMyPlaylists(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserPlaylists(string userId)
        {
            return Json(_playlistService.GetMyPlaylists(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlaylists()
        {
            return Json(_playlistService.GetPlaylists(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPlaylistSongs(PlaylistViewModel playListModel)
        {
            var userId = User.Identity.GetUserId();
            return Json(_playlistService.GetSongs(userId, playListModel.PlaylistId));
        }

        [HttpPost]
        public JsonResult AddPlaylist(PlaylistViewModel playListModel)
        {
            var userId = User.Identity.GetUserId();
            _playlistService.AddPlayList(userId, playListModel);
            return Json(null);
        }

        [HttpPost]
        public JsonResult RemovePlaylist(PlaylistViewModel playListModel)
        {
            var userId = User.Identity.GetUserId();
            _playlistService.RemovePlaylist(userId, playListModel.PlaylistId);
            return Json(null);
        }

        [HttpPost]
        public JsonResult SaveCurrentPlaylist(string currentPlayList, List<Song> playlistSongs)
        {
            var userId = User.Identity.GetUserId();
            _playlistService.SaveCurrentPlaylist(userId, currentPlayList, playlistSongs);
            return Json(null);
        }
    }
}