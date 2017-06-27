(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchAttendanceService', BatchAttendanceService);

    BatchAttendanceService.$inject = ['$http'];

    function BatchAttendanceService($http) {
        var service = {
            retrieveBatchAttendances: retrieveBatchAttendances,
            searchBatchAttendance: searchBatchAttendance
        };

        return service;

        function retrieveBatchAttendances(Paging, OrderBy) {

            var url = "/BatchAttendance/AttendanceList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBatchAttendance(SearchKeyword, Paging, OrderBy) {
            var url = "/BatchAttendance/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();