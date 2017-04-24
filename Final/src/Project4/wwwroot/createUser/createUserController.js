(function () {
    'use strict';

    angular
        .module('app')
        .controller('createUserController', function createUserController($scope, $q, $state, $http) {
            $scope.user = {
                name: '',
                password: ''
            }

            $scope.createError = false;
            $scope.existsError = false;

            $scope.create = function () {
                // "/api/user/create
                $scope.createError = false;
                $scope.existsError = false;

                var deferred = $q.defer();
                $http.post('api/user/create', $scope.user).then(function () {
                    //success -redirect user
                    $http.post('api/login', $scope.user).then(function () {
                        $http.post('api/setting').then(function () {
                            $state.go('home');
                        })
                    })
                },
                function (response) {
                    //failure -display error
                    if (response.statusText == "Conflict")
                        $scope.existsError = true;
                    else
                        $scope.createError = true;
                });
            }
        });
})();
