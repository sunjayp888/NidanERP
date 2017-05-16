(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidateInstallmentService', CandidateInstallmentService);

    CandidateInstallmentService.$inject = ['$http'];

    function CandidateInstallmentService($http) {
        var service = {
            retrieveCandidateInstallments: retrieveCandidateInstallments,
            searchCandidateInstallment: searchCandidateInstallment,
            retrieveCandidateFeeList: retrieveCandidateFeeList,
            viewCandidateFee: viewCandidateFee
        };

        return service;

        function retrieveCandidateInstallments(Paging, OrderBy) {

            var url = "/CandidateFee/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateFeeList(Paging, OrderBy) {

            var url = "/Registration/CandidateFeeList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function viewCandidateFee(candidateInstallmentId) {

            var url = "/CandidateFee/Detail",
                data = {
                    candidateInstallmentId: candidateInstallmentId
                };

            return $http.post(url, data);
        }

        function searchCandidateInstallment(SearchKeyword, Paging, OrderBy) {
            var url = "/CandidateFee/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

    }
})();