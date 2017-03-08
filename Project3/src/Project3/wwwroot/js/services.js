(function () {
    angular.module('pokedex.services', [])
        .factory('Pokemons', Pokemons)
        .factory('History', History)
        .constant('PokeapiURL', 'http://pokeapi.co/api/v1/');


    Pokemons.$inject = ['$http', '$q', 'PokeapiURL'];
    function Pokemons($http, $q, PokeapiURL) {
        var service = {
            getAll: getAll,
            getMove: getMove,
            get: get
        }

        return service;

        function get(id) {
            var defered = $q.defer();
            var url = PokeapiURL + 'pokemon/' + id;
            $http.get(url, { cache: true })
                .success(function (response) {
                    var evolutions = response.evolutions;
                    var seenEvolutions = {};
                    evolutions = evolutions.filter(function (evo) {
                        return seenEvolutions.hasOwnProperty(evo.to) ? false : (seenEvolutions[evo.to] = true);
                    });
                    response.evolutions = evolutions.map(buildPokemon);
                    defered.resolve(buildPokemon(response))
                })
                .error(function () {
                    defered.reject([]);
                });
            return defered.promise;
        }

        function getMove(id) {
            var defered = $q.defer();
            var url = PokeapiURL + 'move/' + id;
            $http.get(url, { cache: true })
                .success(function (response) {
                    defered.resolve(response)
                })
                .error(function () {
                    defered.reject([]);
                });
            return defered.promise;
        }

        function buildPokemon(pokemon) {
            var parts = pokemon.resource_uri.split('/');
            var id = parts[parts.length - 2];
            pokemon.id = parseInt(id);
            var name = '';
            if (!pokemon.name) {
                name = pokemon.to.toLowerCase();
            } else {
                name = pokemon.name.toLowerCase();
            }
            pokemon.img = "https://img.pokemondb.net/sprites/black-white/normal/" + name + ".png"
            return pokemon;
        }

        function filtrarMegaPokemons(pokemon) {
            return pokemon.id < 10000;
        }

        function filtrarNewPokemons(pokemon) {
            return pokemon.id < 650;
        }

        function comparatorPokemons(pokemonA, pokemonB) {
            return pokemonA.id < pokemonB.id ? -1 : 1;
        }

        function getAll() {
            var defered = $q.defer();
            var url = PokeapiURL + 'pokedex/1/';
            $http.get(url, { cache: true }).success(function (response) {
                var pokemons = response.pokemon;
                pokemons = pokemons.map(buildPokemon);
                pokemons = pokemons.filter(filtrarMegaPokemons);
                pokemons = pokemons.filter(filtrarNewPokemons);
                pokemons = pokemons.sort(comparatorPokemons);
                defered.resolve(pokemons);
            }).error(function () {
                defered.reject([]);
            });
            return defered.promise;
        }
    };

    History.$inject = ['$rootScope'];
    function History($rootScope) {
        var service = {
            pushState: pushState,
            popState: popState,
            getHistory: getHistory,
            clear: clear
        }

        var history = [];
        if (localStorage.history) {
            history = JSON.parse(localStorage.history);
        }

        return service;

        function pushState(name) {
            var state = window.location.hash;
            var actual = stateActual();
            if (!actual || actual.state !== state) {
                history.push({ name: name, state: state });
                save();
            }
        }

        function stateActual() {
            return history[history.length - 1];
        }

        function popState(steps) {
            for (var i = steps; i >= 0; i--) {
                history.pop()
            }
            save();
        }

        function clear() {
            history = [];
            save();
        }

        function save() {
            localStorage.setItem('history', JSON.stringify(history));
            $rootScope.$emit('history.changed');
        }

        function getHistory() {
            return angular.copy(history);
        }

    }
})();
