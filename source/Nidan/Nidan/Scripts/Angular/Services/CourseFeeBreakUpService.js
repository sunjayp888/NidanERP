(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CourseFeeBreakUpService', CourseFeeBreakUpService);

    CourseFeeBreakUpService.$inject = ['$http'];

    function CourseFeeBreakUpService($http) {
        var service = {
            retrieveCourseFeeBreakUps: retrieveCourseFeeBreakUps,
            searchCourseFeeBreakUp: searchCourseFeeBreakUp
        };

        return service;

        function retrieveCourseFeeBreakUps(Paging, OrderBy) {

            var url = "/CourseFeeBreakUp/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCourseFeeBreakUp(SearchKeyword, Paging, OrderBy) {
            var url = "/CourseFeeBreakUp/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();