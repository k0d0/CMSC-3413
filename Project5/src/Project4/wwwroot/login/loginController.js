(function () {
    'use strict';

    angular
        .module('app')
        .controller('loginController', function loginController($scope, $q, $http, $state) {
            $scope.loginError = false;
            $scope.user = {
                UserName: "",
                Password : ""
            };

            $scope.login = function () {
                // "/api/login/
                var deferred = $q.defer();
                $http.post('api/login/', $scope.user).then(function () {
                    //success -redirect user
                    $scope.loginError = false;
                    $state.go('home');
                },
                function () {
                    //failure -display error
                    $scope.loginError = true;
                });
            }

            $scope.create = function () {
                $state.go('createUser');
            }

        });
})();
