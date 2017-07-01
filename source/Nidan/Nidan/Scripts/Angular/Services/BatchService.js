(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchService', BatchService);

    BatchService.$inject = ['$http'];

    function BatchService($http) {
        var service = {
            retrieveBatches: retrieveBatches,
            searchBatchByDate: searchBatchByDate,
            retrieveRoomByHours: retrieveRoomByHours,
            retrieveBatchByCentre: retrieveBatchByCentre
        };

        return service;

        function retrieveBatches(Paging, OrderBy) {

            var url = "/Batch/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBatchByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Batch/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function retrieveRoomByHours(hours) {
            var url = "/Batch/GetRoom",
                data = {
                    hours: hours
                };
            return $http.post(url, data);
        }

        function retrieveBatchByCentre(centreId) {
            var url = "/Batch/CentreBatch",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

    }
})();