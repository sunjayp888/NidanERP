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
            retrieveModuleExamQuestionSets: retrieveModuleExamQuestionSets
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

        function retrieveModuleExamQuestionSets(moduleExamSetId, Paging, OrderBy) {

            var url = "/ModuleExamSet/ModuleExamQuestion",
                data = {
                    moduleExamSetId: moduleExamSetId,
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