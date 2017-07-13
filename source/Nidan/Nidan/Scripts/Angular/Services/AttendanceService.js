(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AttendanceService', AttendanceService);

    AttendanceService.$inject = ['$http'];

    function AttendanceService($http) {
        var service = {
            retrieveAttendances: retrieveAttendances,
            searchAttendance: searchAttendance,
            searchAttendanceByDate: searchAttendanceByDate
        };

        return service;

        function retrieveAttendances(Paging, OrderBy) {

            var url = "/Attendance/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAttendance(SearchKeyword, Paging, OrderBy) {
            var url = "/Attendance/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAttendanceByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Attendance/SearchByDate",
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