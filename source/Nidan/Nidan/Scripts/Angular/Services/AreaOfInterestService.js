(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AreaOfInterestService', AreaOfInterestService);

    AreaOfInterestService.$inject = ['$http'];

    function AreaOfInterestService($http) {
        var service = {
            retrieveAreaOfInterests: retrieveAreaOfInterests,
            canDeleteAreaOfInterest: canDeleteAreaOfInterest,
            deleteAreaOfInterest: deleteAreaOfInterest
        };

        return service;

        function retrieveAreaOfInterests(Paging, OrderBy) {

            var url = "/AreaOfInterest/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteAreaOfInterest(id) {
            var url = "/AreaOfInterest/CanDeleteAreaOfInterest",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteAreaOfInterest(id) {
            var url = "/AreaOfInterest/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();