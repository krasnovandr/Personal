angular.module('AudioNetworkApp').factory('musicService', function ($http) {
    return {
        getSongs: function () {
            return $http.get('Music/GetSongs');
        },

        getMySongs: function () {
            return $http.get('Music/GetMySongs');
        },

        getMyPlaylists: function () {
            return $http.get('PlayList/GetMyPlaylists');
        },

        gePlaylists: function () {
            return $http.get('PlayList/GetPlaylists');
        },
        getCurrentPlaylistSongs: function (playlist) {
            return $http({ method: 'POST', url: 'PlayList/GetPlaylistSongs', data: playlist });
        },

        getUserSongs: function (userId) {
            return $http({ method: 'POST', url: 'Music/GetUserSongs', data: userId });
        },

        getUserPlaylists: function (userId) {
            return $http({ method: 'POST', url: 'PlayList/getUserPlaylists', data: userId });
        },

        getSong: function (songId) {
            return $http({ method: 'POST', url: 'Music/GetSong', data: songId });
        },

        saveCurrentPlaylist: function (playlist) {
            return $http({ method: 'POST', url: 'PlayList/SaveCurrentPlaylist', data: playlist });
        },
   
        removeSong: function (song) {
            return $http({ method: 'POST', url: 'Music/RemoveSong', data: song });
        },

        addToMyMusic: function (song) {
            return $http({ method: 'POST', url: 'Music/AddToMyMusic', data: song });
        },
        downloadSong: function (song) {
            return $http({ method: 'POST', url: 'Music/DownloadSong', data: song });
        },

        listenedSong: function (song) {
            return $http({ method: 'POST', url: 'Music/ListenedSong', data: song });
        },
        updateCurrentSong: function (song) {
            if (song) {
                var songData = {
                    songId: song.SongId
                };
            }
            return $http({ method: 'POST', url: 'Users/UpdateUserCurrentSong', data: songData });
        },

        updateConversationCurrentSong: function (song) {
            return $http({ method: 'POST', url: 'Conversation/UpdateConversationCurrentSong', data: song });
        },


        //updateConversationPlaylist: function (song) {
        //    return $http({ method: 'POST', url: 'Conversation/UpdateConversationCurrentSong', data: song });
        //},
    };
});