//Examples for Search Hisotry/Breadcrumbs found online (goBack())
//History & Breadcrumbs currently unavailable
(function () {
    angular.module('pokedex.directives',[])
    .directive('navigation', Navigation);

    Navigation.$inject = ['History', '$rootScope', '$state'];
    function Navigation(History, $rootScope, $state) {
        var directive = {
            templateUrl: "pages/breadcrumbs.html",
            restrict: "E",
            link: link
        }

        return directive;

        //Should say: hey go back to previous page
        function link($scope, $element, $attrs, $controller){
            $scope.back = function(steps){
                History.popState(steps);
                window.history.go(steps * -1);
            }

            $scope.Navigation = History.getHistory();
            $rootScope.$on('history.changed',function(){
                $scope.Navigation = History.getHistory();
            })
        }
    }

})();
