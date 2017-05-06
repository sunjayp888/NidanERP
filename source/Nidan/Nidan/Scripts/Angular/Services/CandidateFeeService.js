(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CandidateFeeService', CandidateFeeService);

    CandidateFeeService.$inject = ['$http'];

    function CandidateFeeService($http) {
        var service = {
            retrieveCandidateFees: retrieveCandidateFees,
            searchCandidateFee: searchCandidateFee
        };

        return service;

        function retrieveCandidateFees(paging, orderBy) {

            var url = "/CandidateFee/List",
                data = {
                    paging: paging,
                    orderBy: new Array(orderBy)
                };

            return $http.post(url, data);
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
        
    }
})();