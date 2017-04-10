(function () {
    'use strict';

    angular
        .module('app')
        .factory('projectService', function ($http, $q) {

            function save(project) {
                return $http.post('api/project', project).then(function (response) {
                    return response.data;
                });
            }

            function getProjects() {               
                return $http.get('api/project').then(function (response) {
                    var projectsReadyForView = _.map(response.data, function (project) {
                        project.dueDate = moment(project.dueDate).local().format('lll');
                        return project;
                    });
                    return projectsReadyForView;
                });
            }

            var service = {
                getProjects: getProjects,
                save: save
            };
            
            return service;
        });

})();