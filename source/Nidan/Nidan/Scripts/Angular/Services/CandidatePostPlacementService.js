(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidatePostPlacementService', CandidatePostPlacementService);

    CandidatePostPlacementService.$inject = ['$http'];

    function CandidatePostPlacementService($http) {
        var service = {
            searchCandidatePostPlacement: searchCandidatePostPlacement,
            retrieveCandidatePostPlacementByBatchId: retrieveCandidatePostPlacementByBatchId,
            searchCandidatePostPlacementByDate: searchCandidatePostPlacementByDate,
            retrieveBatches: retrieveBatches,
            retrieveCandidatePostPlacementByAdmissionId: retrieveCandidatePostPlacementByAdmissionId
        };

        return service;

        function retrieveCandidatePostPlacementByBatchId(batchId, Paging, OrderBy) {

            var url = "/CandidatePostPlacement/CandidatePostPlacementByBatchId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidatePostPlacement(SearchKeyword, Paging, OrderBy) {
            var url = "/CandidatePostPlacement/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCandidatePostPlacementByDate(FromDate, ToDate, BatchId, Paging, OrderBy) {
            var url = "/CandidatePostPlacement/SearchByDate",
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

            var url = "/CandidatePostPlacement/GetBatches";
            return $http.post(url);
        }

        function retrieveCandidatePostPlacementByAdmissionId(admissionId, Paging, OrderBy) {

            var url = "/CandidatePostPlacement/CandidatePostPlacementByAdmissionId",
                data = {
                    admissionId: admissionId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();