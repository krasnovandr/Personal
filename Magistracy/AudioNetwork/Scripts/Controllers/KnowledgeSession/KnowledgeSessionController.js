angular.module('AudioNetworkApp')
    .controller('KnowledgeSessionController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService) {

        $scope.friendsPhase = false;
        $scope.theme = '';
        $scope.friends = [];
        $scope.members = [];
        $scope.firstRound = false;
        $scope.mySessions = [];

        knowledgeSessionService.getUserSessions($rootScope.logState.Id).success(function (result) {
            $scope.mySessions = result;
        });

        $scope.createSession = function (theme) {
            var knowledgeSession = {
                Theme: theme
            };
            knowledgeSessionService.create(knowledgeSession).success(function (result) {
                $scope.friendsPhase = true;
                $rootScope.currentSession.Id = result;
                $scope.userData = {
                    id: $rootScope.logState.Id
                };
                userService.getUserFriends($scope.userData).success(function (userFriends) {
                    $scope.friends = userFriends;
                });
            });
        };
        $scope.startFirstRound = function () {
            knowledgeSessionService.addMembers($scope.members, $rootScope.currentSession.Id).success(function (userFriends) {
                $location.path('/KnowledgeSession/FirstRound/' + $rootScope.currentSession.Id);

            });
        };

        $rootScope.myHub  = $.connection.knowledgeSessionHub;
        $rootScope.myHub.client.updateClient = function (message) {
            alert('Вы были добавлены в новую сессию обмена ресурсами');
        };

        $.connection.hub.start().done(function () {

         
        });

        $scope.viewSession = function (currentSession) {
            if (currentSession.SessionState == 0) {
                $location.path('/KnowledgeSession/FirstRound/' + currentSession.Id);
            }
            if (currentSession.SessionState == 1) {
                $location.path('/KnowledgeSession/FirstRoundMainBoard/' + currentSession.Id);
            }
        };

        $scope.addUserToSession = function (friendIndex, user) {
            $scope.members.push(user);
            $scope.friends.splice(friendIndex, 1);
        };

        $scope.removeUserFromSession = function (membersIndex, user) {
            $scope.members.splice(membersIndex, 1);
            $scope.friends.push(user);
        };
    });