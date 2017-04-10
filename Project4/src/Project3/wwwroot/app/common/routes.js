(function () {
    'use strict';

    angular
        .module('app')
        .config(function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            $stateProvider.state('project', {
                url: '/',
                controller: 'ProjectController',
                controllerAs: 'project',
                templateUrl: 'app/project/project.html',
                resolve: {
                    projects: function (projectService) {
                        return projectService.getProjects();
                    }
                }
            })
        });

})();
