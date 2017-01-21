(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('PersonnelService', PersonnelService);

    PersonnelService.$inject = ['$http'];

    function PersonnelService($http) {
        var service = {
            retrievePersonnel: retrievePersonnel,
            searchPersonnel: searchPersonnel
        };

        return service;

        function retrievePersonnel(Paging, OrderBy) {
            var url = "/Personnel/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchPersonnel(SearchKeyword, Paging, OrderBy) {
            var url = "/Personnel/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();