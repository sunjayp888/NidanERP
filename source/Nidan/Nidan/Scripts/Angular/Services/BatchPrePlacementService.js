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
            retrieveCandidatePrePlacementByBatchPrePlacementId: retrieveCandidatePrePlacementByBatchPrePlacementId,
            openCandidatePrePlacementActivityModalPopUp: openCandidatePrePlacementActivityModalPopUp,
            saveCandidatePrePlacementActivity: saveCandidatePrePlacementActivity,
            openCandidatePrePlacementUpdateModalPopUp: openCandidatePrePlacementUpdateModalPopUp,
            retrieveCandidatePrePlacementReportByBatchPrePlacementId: retrieveCandidatePrePlacementReportByBatchPrePlacementId
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

        function retrieveCandidatePrePlacementByBatchPrePlacementId(batchPrePlacementId, Paging, OrderBy) {

            var url = "/BatchPrePlacement/RetrieveCandidatePrePlacementByBatchPrePlacementId",
                data = {
                    batchPrePlacementId: batchPrePlacementId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function openCandidatePrePlacementActivityModalPopUp(id) {

            var url = "/BatchPrePlacement/GetBatchPrePlacement/" + id;
            return $http.post(url);
        }

        //saveCandidatePrePlacementActivity
        function saveCandidatePrePlacementActivity(candidatePrePlacement) {
            var url = "/BatchPrePlacement/SaveCandidatePrePlacementActivity",
                data = {
                    candidatePrePlacement: candidatePrePlacement
                };
            return $http.post(url, data);
        }

        //openCandidatePrePlacementUpdateModalPopUp
        function openCandidatePrePlacementUpdateModalPopUp(id) {

            var url = "/BatchPrePlacement/GetCandidatePrePlacement/" + id;
            return $http.post(url);
        }

        //retrieveCandidatePrePlacementReportByBatchPrePlacementId
        function retrieveCandidatePrePlacementReportByBatchPrePlacementId(batchPrePlacementId, Paging, OrderBy) {

            var url = "/BatchPrePlacement/RetrieveCandidatePrePlacementReportByBatchPrePlacementId",
                data = {
                    batchPrePlacementId: batchPrePlacementId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();