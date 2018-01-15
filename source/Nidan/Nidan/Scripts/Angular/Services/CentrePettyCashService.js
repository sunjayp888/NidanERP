(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CentrePettyCashService', CentrePettyCashService);

    CentrePettyCashService.$inject = ['$http'];

    function CentrePettyCashService($http) {
        var service = {
            retrieveCentrePettyCashs: retrieveCentrePettyCashs,
            searchCentrePettyCash: searchCentrePettyCash,
            searchCentrePettyCashByDate: searchCentrePettyCashByDate
        };

        return service;

        function retrieveCentrePettyCashs(Paging, OrderBy) {

            var url = "/CentrePettyCash/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCentrePettyCash(SearchKeyword, Paging, OrderBy) {
            var url = "/CentrePettyCash/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCentrePettyCashByDate(fromDate, toDate, paging, orderBy) {
            var url = "/CentrePettyCash/SearchByDate",
                data = {
                    fromDate: fromDate,
                    toDate: toDate,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };

            return $http.post(url, data);
        }
    }
})();