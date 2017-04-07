(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CourseInstallmentService', CourseInstallmentService);

    CourseInstallmentService.$inject = ['$http'];

    function CourseInstallmentService($http) {
        var service = {
            retrieveCourseInstallments: retrieveCourseInstallments,
            searchCourseInstallment: searchCourseInstallment
        };

        return service;

        function retrieveCourseInstallments(Paging, OrderBy) {

            var url = "/CourseInstallment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCourseInstallment(SearchKeyword, Paging, OrderBy) {
            var url = "/CourseInstallment/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();