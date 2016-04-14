(function() {
    "use strict";

    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController() {

        var vm = this;

        vm.trips = [
            {
                name: "Us Trip",
                created: Date.now()
            },
            {
                name: "World Trip",
                created: Date.now()
            }
        ];

        vm.newTrip = {};

        vm.addTrip = function() {
            alert(vm.newTrip.name);
        };
    };
})();