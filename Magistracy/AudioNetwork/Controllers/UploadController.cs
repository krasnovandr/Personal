using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AudioNetwork.Helpers;
using AudioNetwork.Models;
using AudioNetwork.Services;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RecognitionService.Models;

namespace AudioNetwork.Controllers
{
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadService;
        private readonly IPlaylistService _playlistService;
        private readonly IMusicService _musicService;

        public UploadController(
            IUploadService uploadService,
          IPlaylistService playlistService, IMusicService musicService)
        {
            _uploadService = uploadService;
            _playlistService = playlistService;
            _musicService = musicService;
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
            var model = new SongRecognitionModel();
            if (file != null && file.ContentLength > 0)
            {
                var absoluteSongPath = Server.MapPath(FilePathContainer.SongVirtualPath);

                var songId = Guid.NewGuid().ToString();
                var pathSong = Path.Combine(absoluteSongPath, songId + ".wav");

                file.SaveAs(pathSong);

                var recognition = new MusicRecognition.Services.RecognitionService();
                var resultStringFormat = recognition.Recognize(pathSong, 0, 20);

                var resultJson = JsonConvert.DeserializeObject<RecognizeResult>(resultStringFormat);
                if (resultJson.status.msg == "Success")
                {
                    var songData = resultJson.metadata.music.FirstOrDefault();
                    if (songData != null)
                    {
                        var artist = songData.artists.FirstOrDefault();
                        string artistName = null;
                        if (artist != null)
                        {
                            artistName = artist.Name;
                        }
                        var pictureInfo = SongPictureGetter.CheckContent(artistName, songData.album.Name, songData.title);
                        model.Artist = artistName;
                        model.Album = songData.album.Name;
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