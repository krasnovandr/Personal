using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNet.Identity;
using MusicRecognition.Interfaces;
using MusicRecognition.Models;
using Newtonsoft.Json;
using ServiceLayer.Helpers;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace AudioNetwork.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadService;
        private readonly IPlaylistService _playlistService;
        private readonly IMusicService _musicService;
        private readonly IRecognitionService _recognitionService;

        public UploadController(
            IUploadService uploadService,
            IPlaylistService playlistService,
            IMusicService musicService,
            IRecognitionService recognitionService)
        {
            _uploadService = uploadService;
            _playlistService = playlistService;
            _musicService = musicService;
            _recognitionService = recognitionService;
        }

        public ActionResult Upload()
        {
            return View("Upload");
        }

        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var imageId = Guid.NewGuid().ToString();
                var userId = User.Identity.GetUserId();
                var imagePath = FilePathContainer.ImagePathRelative + imageId + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(FilePathContainer.ForImagePhysicalPath), imageId + Path.GetExtension(file.FileName));
                file.SaveAs(path);
                _uploadService.UploadUserImage(imagePath, userId);
            }

            return Json(null);
        }

        public JsonResult UploadConversationImage(HttpPostedFileBase file, string conversationId)
        {
            if (file != null && file.ContentLength > 0)
            {
                var imageId = Guid.NewGuid().ToString();
                var imagePath = FilePathContainer.ConversationCoversPathRelative + imageId + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(FilePathContainer.ForConversationCoversPhysicalPath), imageId + Path.GetExtension(file.FileName));
                file.SaveAs(path);

                _uploadService.UploadConversationImage(imagePath, conversationId);
            }

            return Json(null);
        }

        public ActionResult UploadForRecognitionSong(HttpPostedFileBase file)
        {
            var model = new SongRecognitionModelView();
            if (file != null && file.ContentLength > 0)
            {
                var absoluteSongPath = Server.MapPath(FilePathContainer.SongVirtualPath);

                var songId = Guid.NewGuid().ToString();
                var pathSong = Path.Combine(absoluteSongPath, songId + ".wav");

                file.SaveAs(pathSong);

                var resultJson = _recognitionService.Recognise(pathSong, 0, 20);
                if (resultJson != null)
                {
                    var songData = resultJson.metadata.music.FirstOrDefault();
                    if (songData != null)
                    {
                        var artist = songData.artists.FirstOrDefault();
                        string artistName = string.Empty;
                        string albumName = string.Empty;
                        if (artist != null)
                        {
                            artistName = artist.Name;
                        }
                        if (songData.album != null)
                        {
                            albumName = songData.album.Name;
                        }
                        var pictureInfo = SongPictureGetter.CheckContent(artistName,albumName , songData.title);
                        model.Artist = artistName;
                        model.Album = albumName;
                        if (pictureInfo != null)
                        {
                            model.AlbumCoverPath = pictureInfo.PicturePath;
                        }
                        model.Title = songData.title;
                    }
                }
            }

            return Json(model);
        }

        public ActionResult UploadSong(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var absoluteSongPath = Server.MapPath(FilePathContainer.SongVirtualPath);
                var absoluteSongCoverPath = Server.MapPath(FilePathContainer.SongAlbumCoverPathPhysical);
                var userId = User.Identity.GetUserId();

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var songId = Guid.NewGuid().ToString();
                var pathSong = Path.Combine(absoluteSongPath, songId + fileExtension);

                file.SaveAs(pathSong);

                _uploadService.UploadSong(fileExtension, fileName, pathSong, songId, absoluteSongCoverPath, userId);
            }

            return Json(new { Success = true });
        }

        public JsonResult GetSongsVk(VkUserModel model)
        {
            var userId = User.Identity.GetUserId();
            return Json(_uploadService.GetSongVk(userId, model), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveSongVk(string songId)
        {
            var userId = User.Identity.GetUserId();

            return Json(new { Success = _playlistService.RemoveSongFromPlaylist(songId, userId + "VK") }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSong(SongViewModel song)
        {
            var userid = User.Identity.GetUserId();
            _uploadService.SaveSongFromVkUpdatePicture(song, userid);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSongs(List<SongViewModel> songs)
        {
            var userid = User.Identity.GetUserId();
            foreach (var song in songs)
            {
                _uploadService.SaveSongFromVkUpdatePicture(song, userid);

            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }


        public FileResult DownloadZip()
        {
            var id = User.Identity.GetUserId();
            var songs = _musicService.GetUserSongs(id);
            //  var song = songs.FirstOrDefault();

            var baseOutputStream = new MemoryStream();
            var zipOutput = new ZipOutputStream(baseOutputStream)
            {
                IsStreamOwner = false
            };

            /*  
            * Higher compression level will cause higher usage of reources 
            * If not necessary do not use highest level 9 
            */

            zipOutput.SetLevel(9);
            // byte[] buffer = new byte[4096];
            foreach (var song in songs)
            {
                var path = Server.MapPath(FilePathContainer.SongVirtualPath) + song.id + FilePathContainer.SongDefaultFormat;
                SharpZipLibHelper.AddFileToZip(zipOutput, path, song.Artist + song.Title + FilePathContainer.SongDefaultFormat);

            }

            zipOutput.Finish();
            zipOutput.Close();

            /* Set position to 0 so that cient start reading of the stream from the begining */
            baseOutputStream.Position = 0;

            /* Set custom headers to force browser to download the file instad of trying to open it */
            return new FileStreamResult(baseOutputStream, "application/x-zip-compressed")
            {
                FileDownloadName = "Songs.zip",

            };

        }


    }
}