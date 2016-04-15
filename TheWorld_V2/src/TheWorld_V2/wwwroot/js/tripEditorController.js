(function() {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips/" + vm.tripName + "/stops")
            .then(function (response) {
                debugger;
                angular.copy(response.data, vm.stops);
            }, function(error) {
                vm.errorMessage = "Failed to load stops";
            }).finally(function() {
                vm.isBusy = false;
            });
    }
})();