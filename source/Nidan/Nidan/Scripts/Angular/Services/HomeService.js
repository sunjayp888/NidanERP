(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('HomeService', HomeService);

    HomeService.$inject = ['$http'];

    function HomeService($http) {
        var service = {
            retrieveStatistics: retrieveStatistics,
            retrieveStatisticsByCentre: retrieveStatisticsByCentre,
            retrieveBarGraphStatistics: retrieveBarGraphStatistics,
            retrieveCentres: retrieveCentres,
            change: change
        };

        return service;

        function retrieveStatistics() {

            var url = "/Home/Statistics";
            return $http.post(url);
        }

        function retrieveStatisticsByCentre(centreId) {

            var url = "/Home/StatisticsByCentre",
                data = {
                    id:centreId
                };

            return $http.post(url, data);
        }

        function retrieveBarGraphStatistics() {

            var url = "/Home/StatisticsBarGraph";
            return $http.post(url);
        }

        function retrieveCentres(Paging, OrderBy) {

            var url = "/Home/GetCentres",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function change(centreId) {

            var url = "/Home/StatisticsByCentre",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }
    }
})();