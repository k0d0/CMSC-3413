(function(){
    var app = angular.module('pokedex',[
        'pokedex.controllers',
        'pokedex.services',
        'pokedex.directives',
        'ui.bootstrap',
        'ui.router'
    ]);

    app.config(RouterConfiguration);

    RouterConfiguration.$inject = ['$stateProvider', '$urlRouterProvider'];
    function RouterConfiguration($stateProvider, $urlRouterProvider){
         $stateProvider.state('pokemons', {
            url: "/pokemons",
            templateUrl: "pages/pokemons.html",
            controller: 'PokemonsController'
        });

         $stateProvider.state('pokemon', {
            url: "/pokemon/:id",
            views: {
                '' :{
                    templateUrl: "pages/pokemon.html",
                    controller: 'PokemonDetailController'
                },
                'evo@pokemon':{
                    templateUrl: "pages/evo.html"
                },
                'moves@pokemon':{
                    templateUrl: "pages/moves.html",
                    controller: 'MovesController'
                }
            }
        });

         $urlRouterProvider.otherwise("/pokemons");
    }

})();
