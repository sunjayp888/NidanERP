(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidateFinalPlacementService', CandidateFinalPlacementService);

    CandidateFinalPlacementService.$inject = ['$http'];

    function CandidateFinalPlacementService($http) {
        var service = {
            searchCandidateFinalPlacement: searchCandidateFinalPlacement,
            retrieveCandidateFinalPlacementByBatchId: retrieveCandidateFinalPlacementByBatchId,
            searchCandidateFinalPlacementByDate: searchCandidateFinalPlacementByDate,
            retrieveBatches: retrieveBatches,
            retrieveCandidateFinalPlacementByAdmissionId: retrieveCandidateFinalPlacementByAdmissionId
        };

        return service;

        function retrieveCandidateFinalPlacementByBatchId(batchId, Paging, OrderBy) {

            var url = "/CandidateFinalPlacement/CandidateFinalPlacementByBatchId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidateFinalPlacement(SearchKeyword, Paging, OrderBy) {
            var url = "/CandidateFinalPlacement/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidateFinalPlacementByDate(FromDate, ToDate, BatchId, Paging, OrderBy) {
            var url = "/CandidateFinalPlacement/SearchByDate",
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

            var url = "/CandidateFinalPlacement/GetBatches";
            return $http.post(url);
        }

        function retrieveCandidateFinalPlacementByAdmissionId(admissionId, Paging, OrderBy) {

            var url = "/CandidateFinalPlacement/CandidateFinalPlacementByAdmissionId",
                data = {
                    admissionId: admissionId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();