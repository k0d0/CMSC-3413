(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectController', function (projects, projectService) {
            /* jshint validthis:true */
            var vm = this;
            vm.projects = projects;
            
            vm.newProject = {
                name: 'Test Project',
                description: 'Test project Desc',
                dueDate: new Date()
            }

            vm.refreshProjects = function () {
                projectService.getProjects().then(function (projects) {
                    vm.projects = projects;
                });
            }

            vm.save = function () {
                projectService.save(vm.newProject).then(function (project) {
                    toastr.success('We saved it!');
                    vm.refreshProjects();
                });
            }

            activate();

            function activate() {
            }
        });
})();
