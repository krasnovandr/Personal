angular.module('AudioNetworkApp').factory('authorizationService', function ($http) {
    return {
        getLoginProviders: function () {
            return $http.get('Account/GetLoginProviders');
        },

        //getWallItem: function (wallItemId) {
        //    return $http({ method: 'POST', url: 'Wall/GetWallItem', data: wallItemId });
        //},
    
        externalAuthentification: function (providerData) {
            return $http({ method: 'POST', url: 'Account/ExternalLogin', data: providerData });
        },
        login: function (loginData) {
            return $http({ method: 'POST', url: 'Account/Login', data: loginData });
        },
        register: function (registerData) {
            return $http({ method: 'POST', url: 'Account/Register', data: registerData });
        },
        logout: function () {
            return $http({ method: 'POST', url: 'Account/LogOff'});
        },
        checkLogin: function () {
            return $http({ method: 'POST', url: 'Account/CheckLogin' });
        },
        confirmRegistration: function (confirmData) {
            return $http({ method: 'POST', url: 'Account/ConfirmEmail', data: confirmData });
        },
        repeatMail: function (confirmData) {
            return $http({ method: 'POST', url: 'Account/RepeatMail', data: confirmData });
        },
     

        //removeWallItem: function (wallItem) {
        //    return $http({ method: 'POST', url: 'Wall/RemoveWallItem', data: wallItem });
        //},

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