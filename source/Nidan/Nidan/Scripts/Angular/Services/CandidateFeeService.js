(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidateFeeService', CandidateFeeService);

    CandidateFeeService.$inject = ['$http'];

    function CandidateFeeService($http) {
        var service = {
            retrieveCandidateFees: retrieveCandidateFees,
            searchCandidateFee: searchCandidateFee,
            retrievePaymentModes: retrievePaymentModes,
            retrieveCandidateFee: retrieveCandidateFee,
            saveFee:saveFee
        };

        return service;

        function retrieveCandidateFees(id) {

            var url = "/CandidateFee/CandidateFeeList/" + id;
            return $http.post(url);
        }

        function retrieveCandidateFee(id) {

            var url = "/CandidateFee/GetFeeCandidate/" + id;
            return $http.post(url);
        }

        function retrievePaymentModes() {

            var url = "/CandidateFee/PaymentMode";
            return $http.post(url);
        }

        function searchCandidateFee(SearchKeyword, Paging, OrderBy) {
            var url = "/CandidateFee/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
        
        function saveFee(candidateFee) {
            var url = "/CandidateFee/SaveFee",
            data = {
                candidateFee: candidateFee
            };
            return $http.post(url, data);
        }
    }
})();