(function () {
    'use strict';

    angular
        .module('app')
        .config(function ($stateProvider, $urlRouterProvider) {
            //
            // For any unmatched url, redirect to /state1
            $urlRouterProvider.otherwise("/");

            // Now set up the states
            $stateProvider
              .state('home', {
                  url: "/",
                  templateUrl: "home/home.html",
                  controller: 'homeController'
              })

             .state ('login', {
                url: "/login",
                templateUrl: "login/login.html",
                controller: 'loginController'
             })

            .state('createUser', {
                url: "/create",
                templateUrl: "createUser/create.html",
                controller: 'createUserController'
            })

        });

})();