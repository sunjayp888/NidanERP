(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidatePrePlacementActivityService', CandidatePrePlacementActivityService);

    CandidatePrePlacementActivityService.$inject = ['$http'];

    function CandidatePrePlacementActivityService($http) {
        var service = {
            searchCandidatePrePlacementActivity: searchCandidatePrePlacementActivity,
            retrieveCandidatePrePlacementActivityByBatchId: retrieveCandidatePrePlacementActivityByBatchId,
            searchCandidatePrePlacementActivityByDate: searchCandidatePrePlacementActivityByDate,
            retrieveBatches: retrieveBatches
        };

        return service;

        function retrieveCandidatePrePlacementActivityByBatchId(batchId, Paging, OrderBy) {

            var url = "/CandidatePrePlacementActivity/CandidatePrePlacementActivityByBatchId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidatePrePlacementActivity(SearchKeyword, Paging, OrderBy) {
            var url = "/CandidatePrePlacementActivity/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidatePrePlacementActivityByDate(FromDate, ToDate, BatchId, Paging, OrderBy) {
            var url = "/CandidatePrePlacementActivity/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    batchId: BatchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveBatches() {

            var url = "/CandidatePrePlacementActivity/GetBatches";
            return $http.post(url);
        }
    }
})();