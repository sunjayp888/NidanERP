(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchPrePlacementService', BatchPrePlacementService);

    BatchPrePlacementService.$inject = ['$http'];

    function BatchPrePlacementService($http) {
        var service = {
            retrieveBatchPrePlacements: retrieveBatchPrePlacements,
            searchBatchPrePlacement: searchBatchPrePlacement,
            searchBatchPrePlacementByDate: searchBatchPrePlacementByDate,
        };

        return service;

        function retrieveBatchPrePlacements(Paging, OrderBy) {

            var url = "/BatchPrePlacement/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBatchPrePlacement(SearchKeyword, Paging, OrderBy) {
            var url = "/BatchPrePlacement/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBatchPrePlacementByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/BatchPrePlacement/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        }
})();