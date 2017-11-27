(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ModuleExamSetService', ModuleExamSetService);

    ModuleExamSetService.$inject = ['$http'];

    function ModuleExamSetService($http) {
        var service = {
            retrieveModuleExamSets: retrieveModuleExamSets,
            searchModuleExamSet: searchModuleExamSet,
        };

        return service;

        function retrieveModuleExamSets(Paging, OrderBy) {

            var url = "/ModuleExamSet/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchModuleExamSet(SearchKeyword, Paging, OrderBy) {
            var url = "/ModuleExamSet/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();