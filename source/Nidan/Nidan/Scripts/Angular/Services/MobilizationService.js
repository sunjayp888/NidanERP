(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('MobilizationService', MobilizationService);

    MobilizationService.$inject = ['$http'];

    function MobilizationService($http) {
        var service = {
            retrieveMobilizations: retrieveMobilizations,
            canDeleteMobilization: canDeleteMobilization,
            deleteMobilization: deleteMobilization,
            searchMobilization: searchMobilization,
            searchMobilizationByDate: searchMobilizationByDate
        };

        return service;

        function retrieveMobilizations(Paging, OrderBy) {

            var url = "/Mobilization/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchMobilization(SearchKeyword, Paging, OrderBy) {
            var url = "/Mobilization/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchMobilizationByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Mobilization/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteMobilization(id) {
            var url = "/Mobilization/CanDeleteMobilization",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteMobilization(id) {
            var url = "/Mobilization/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();