(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ModuleService', ModuleService);

    ModuleService.$inject = ['$http'];

    function ModuleService($http) {
        var service = {
            retrieveModules: retrieveModules,
            canDeleteModule: canDeleteModule,
            deleteModule: deleteModule,
            searchModule: searchModule
        };

        return service;

        function retrieveModules(Paging, OrderBy) {

            var url = "/Module/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchModule(SearchKeyword, Paging, OrderBy) {
            var url = "/Module/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteModule(id) {
            var url = "/Module/CanDeleteModule",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteModule(id) {
            var url = "/Module/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();