(function () {
    'use strict';

    angular
        .module('app')
        .controller('homeController', function homeController($scope, $http, $q, $state) {

            $scope.received = []
            $scope.settings = '';
            $scope.searchString = '';
            $scope.nextStatus = "completed";
            $scope.descOrder = "asc";
            $scope.dateOrder = "asc";
            $scope.modifying = false;
            getItems();

            $scope.newItem = {
                description: "",
                dueDate: '',
                tags: "",
                status: "INCOMPLETE"
            };

            $scope.modifiedItem = '';

            $scope.searchTags = function () {
                if ($scope.searchString == '')
                    getItems();
                else {
                    var deferred = $q.defer();
                    $http.get('api/todo/search/' + $scope.searchString).then(function (response) {
                        $scope.received = response.data.slice();

                        checkWindow();
                    }, function (response) {
                        if (response.statusText == "Unauthorized")
                            unauthorized()
                        //else
                        //    something;
                    });
                }
            }

            $scope.addItem = function () {
                $scope.error = '';

                $scope.newItem.dueDate = moment($scope.newItem.dueDate);
                $scope.newItem.dueDate.add($scope.newItem.dueDate.utcOffset(), 'minutes');
                //$scope.newItem.dueDate.utc();
                    //$scope.newItem.dueDate = moment.utc($scope.newItem.dueDate); //Make UTC
                    //$scope.newItem.dueDate.utc();
                

                $http.post('api/todo', $scope.newItem).then(function () {
                    if ($scope.nextStatus == 'completed')
                        getItems();
                    else
                        getCompletedItems();
                    $scope.newItem = '';
                }, function (response) {
                        if (response.statusText == "Unauthorized")
                            unauthorized()
                        else {
                            $scope.error = "add your todo item."
                        }
                    });
            }

            $scope.sortDesc = function () {
                $scope.error = '';

                var deferred = $q.defer
                $http.get('api/todo/desc/' + $scope.descOrder).then(function (response) {
                    $scope.received = response.data.slice();
                    checkWindow();

                    if ($scope.descOrder == 'asc')
                        $scope.descOrder = 'desc';
                    else
                        $scope.descOrder = 'asc';
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    //else
                    //    something
                });
            }

            $scope.sortDate = function () {
                $scope.error = '';

                var deferred = $q.defer
                $http.get('api/todo/date/' + $scope.dateOrder).then(function (response) {
                    $scope.received = response.data.slice();
                    checkWindow();
 
                    if ($scope.dateOrder == 'asc')
                        $scope.dateOrder = 'desc';
                    else
                        $scope.dateOrder = 'asc';
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    //else
                    //    something
                });
            }

            $scope.switchStatus = function () {
                if ($scope.nextStatus == "completed") {
                    getCompletedItems();
                    $scope.nextStatus = "incomplete"
                }
                else {
                    getItems();
                    $scope.nextStatus = "completed"
                }

                
            }

            $scope.openModifier = function (result) {
                $scope.modifying = true;
                $scope.modifiedItem = result;
                $scope.modifiedItem = {
                    id: result.id,
                    description: result.description,
                    dueDate: result.dueDate,
                    tags: result.tags,
                    status: result.status,
                    userName: result.userName,
                    warn : result.warn
                }

                $scope.modifiedItem.dueDate = moment($scope.modifiedItem.dueDate);
                $scope.modifiedItem.dueDate.subtract($scope.modifiedItem.dueDate.utcOffset(), 'minutes');
            }

            $scope.cancelModify = function () {
                $scope.modifying = false;
                $scope.modifiedItem = '';
            }

            $scope.modify = function () {
                $scope.error = '';

                $scope.modifiedItem.dueDate = moment($scope.modifiedItem.dueDate);
                $scope.modifiedItem.dueDate.add($scope.modifiedItem.dueDate.utcOffset(), 'minutes');
                //$scope.modifiedItem.dueDate.utc(); 

                var deferred = $q.defer();
                $http.put('api/todo/', $scope.modifiedItem).then(function () {

                    if ($scope.nextStatus == 'completed')
                        getItems();
                    else
                        getCompletedItems();

                    $scope.cancelModify();
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    else
                        $scope.error = "modify your item."
                });
            }

            $scope.delete = function (result) {
                $scope.error = '';

                var deferred = $q.defer();
                $http.delete('api/todo/'+result.id).then(function (response) {
                    getCompletedItems();
                    $scope.cancelModify
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    else
                        $scope.error = "delete your item."
                });
            }
            
            $scope.updateWindow = function () {
                $scope.error = '';

                var deferred = $q.defer();
                $http.put('api/setting/', $scope.settings).then(function () {

                    if ($scope.nextStatus == 'completed')
                        getItems();
                    else
                        getCompletedItems();
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    else
                        $scope.error = "update your settings.";
                });
            }
            
            function getCompletedItems() {
                $scope.error = '';

                var deferred = $q.defer();
                $http.get('api/todo/complete').then(function (response) {
                    $scope.received = response.data.slice();
                    checkWindow();

                    /*for (var i = 0; i < $scope.received.length; i++)
                        $scope.received[i].dueDate = moment($scope.received[i].dueDate + "-0500");   
                    */
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    //else
                    //    something;
                });
            }

            function getWarningWindow() {
                $scope.error = '';


                var deferred = $q.defer();
                $http.get('api/setting/').then(function (response) {
                    $scope.settings = response.data[0];
                    checkWindow();
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    else
                        $scope.error = "retrieve settings."
                });
            }

            function getItems() {
                $scope.error = '';

                var deferred = $q.defer();
                $http.get('api/todo/').then(function (response) {
                    $scope.received = response.data.slice();
                    getWarningWindow();
                }, function (response) {
                    if (response.statusText == "Unauthorized")
                        unauthorized()
                    //else
                    //    something;
                });
            }

            function checkWindow() {
                for (var i = 0; i < $scope.received.length; i++) {
                    
                    if ($scope.received[i].status != "COMPLETE") {
                        $scope.received[i].dueDate = moment($scope.received[i].dueDate);
                        $scope.received[i].dueDate.add($scope.received[i].dueDate.utcOffset(), 'minutes');

                        var now = moment();
                        now.add(now.utcOffset(), 'minutes');
                        if ($scope.received[i].dueDate.isBefore(now.add($scope.settings.warningWindow, 'hours'))) {
                            $scope.received[i].warn = "table warning";
                        }
                        now = moment();
                        now.add(now.utcOffset(), 'minutes');
                        if ($scope.received[i].dueDate.isBefore(now)) {
                            $scope.received[i].warn = "table danger";
                        }
                    }
                    else
                        $scope.received[i].warn = "table success";
                }
            }

            function unauthorized() {
                $state.go('login')
            }

        });
})();
