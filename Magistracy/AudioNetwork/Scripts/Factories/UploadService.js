angular.module('AudioNetworkApp').factory('uploadService', function ($http) {
    return {
        addUserVkInfo: function (userVkLogin) {
            return $http({ method: 'POST', url: 'Upload/GetSongsVk', data: userVkLogin });
        },
        saveSong: function (song) {
            return $http({ method: 'POST', url: 'Upload/SaveSong', data: song });
        },
        saveSongs: function (songs) {
            return $http({ method: 'POST', url: 'Upload/SaveSongs', data: songs });
        },
        removeSong: function (song) {
            return $http({ method: 'POST', url: 'Upload/RemoveSongVk', data: song });
        },
        downloadZip: function (userId) {
            return $http({ method: 'POST', url: 'Upload/DownloadZip', data: userId });
        },
    
        //[HttpGet]
        //public ActionResult GetWall(string userId)
        //{
        //    var wall = _wallRepository.GetWall(userId);
        //    var wallView = new List<WallItemViewModel>();
        //    wallView.AddRange(wall.Select(ModelConverters.ToWallItemViewModel));
        //    return Json(wallView, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetWallIem(int wallItemId)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var wallItem = _wallRepository.GetWallIem(userId, wallItemId);
        //    return Json(ModelConverters.ToWallItemViewModel(wallItem), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult AddWallItem(WallItemViewModel wallItemView)
        //{
        //    var userId = User.Identity.GetUserId();
        //    _wallRepository.AddWallItem(userId, ModelConverters.ToWallItemModel(wallItemView));
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult RemoveWallItem(int wallItemId)
        //{
        //    var userId = User.Identity.GetUserId();
        //    _wallRepository.RemoveWallItem(userId, wallItemId);
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

    };
});