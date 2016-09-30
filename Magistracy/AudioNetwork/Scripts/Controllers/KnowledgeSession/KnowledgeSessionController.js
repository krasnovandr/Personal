angular.module('AudioNetworkApp')
    .controller('KnowledgeSessionController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, urlMakerService) {

      $scope.friendsPhase = false;
      $scope.theme = '';
      $scope.friends = [];
      $scope.members = [];
      $scope.firstRound = false;
      $scope.mySessions = [];
      $scope.session = {};

      knowledgeSessionService.getUserSessions($rootScope.logState.Id).success(function (result) {
        $scope.mySessions = result;
      });

      $scope.createSession = function (theme) {
        var knowledgeSession = {
          Theme: theme
        };
        knowledgeSessionService.create(knowledgeSession).success(function (result) {
          if (result != 0) {
            $scope.sessionId = result;
            knowledgeSessionService.getSession($scope.sessionId).success(function (currentSession) {
              $scope.session = currentSession;
            });
            $scope.friendsPhase = true;
          
            $scope.userData = {
              id: $rootScope.logState.Id
            };
            userService.getUserFriends($scope.userData).success(function (userFriends) {
              $scope.friends = userFriends;
            });
          }

        });
      };
      $scope.startFirstRound = function () {
        knowledgeSessionService.addMembers($scope.members, $scope.sessionId).success(function (userFriends) {
            urlMakerService.viewSession($scope.sessionId);

        });
      };

      //$rootScope.myHub = $.connection.knowledgeSessionHub;
      //$rootScope.myHub.client.updateClient = function (message) {
      //  alert('Вы были добавлены в новую сессию обмена ресурсами');
      //};

      //$.connection.hub.start().done(function () {


      //});

      $scope.viewSession = function (currentSession) {
          urlMakerService.viewSession(currentSession.Id);
        //if (currentSession.SessionState == 0) {
        //  //$location.path('/KnowledgeSession/Round/' + currentSession.Id).search({ level: '1' });
        //  urlMakerService.viewRound(currentSession.Id, 1, 111);
        //}
        //if (currentSession.SessionState == 1) {
        //  //$location.path('/KnowledgeSession/RoundLevelVote/' + currentSession.Id).search({ level: '1' });
        //  urlMakerService.viewRoundLevelVote(currentSession.Id, 1, 111);
        //}
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