(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('GovernmentMobilizationService', GovernmentMobilizationService);

    GovernmentMobilizationService.$inject = ['$http'];

    function GovernmentMobilizationService($http) {
        var service = {
            retrieveGovernmentMobilizations: retrieveGovernmentMobilizations,
            searchGovernmentMobilization: searchGovernmentMobilization,
            searchGovernmentMobilizationByDate: searchGovernmentMobilizationByDate,
        };

        return service;

        function retrieveGovernmentMobilizations(Paging, OrderBy) {

            var url = "/GovernmentMobilization/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchGovernmentMobilization(SearchKeyword, Paging, OrderBy) {
            var url = "/GovernmentMobilization/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchGovernmentMobilizationByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/GovernmentMobilization/SearchByDate",
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