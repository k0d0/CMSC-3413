(function () {
    angular.module('pokedex.controllers', [])
        .controller('PokemonsController', PokemonsController)
        .controller('PokemonDetailController', PokemonDetailController)
        .controller('MovesController', MovesController);

    PokemonsController.$inject = ['$scope', 'Pokemons', 'History'];
    function PokemonsController($scope, Pokemons, History) {
        History.clear();

        $scope.pokemons = [];
        $scope.loading = true;
        Pokemons.getAll().then(function (pokemons) {
            $scope.pokemons = pokemons;
            $scope.loading = false;
        }, function () {
            $scope.pokemons = [];
            $scope.loading = false;
        });
    }

    PokemonDetailController.$inject = ['$scope', '$state', 'Pokemons', 'History'];
    function PokemonDetailController($scope, $state, Pokemons, History) {
        var pokeId = $state.params.id;

        $scope.$state = $state;

        $scope.pokemon = {};
        Pokemons.get(pokeId).then(function (pokemon) {
            $scope.pokemon = pokemon;
            History.pushState(pokemon.name)
        }, function () {
            $scope.pokemon = {};
        });
    }

    MovesController.$inject = ['$scope', 'Pokemons'];
    function MovesController($scope, Pokemons) {
        $scope.movement = null;
        $scope.isActive = function (moveId) {
            if ($scope.movement) {
                return $scope.movement.id == parseInt(moveId);
            }
            return false;
        }
        $scope.verMovimento = function (moveId) {
            Pokemons.getMove(moveId).then(function (move) {
                $scope.movement = move;
            }, function () {
                $scope.movement = null;
            })
        }
    }
})();
