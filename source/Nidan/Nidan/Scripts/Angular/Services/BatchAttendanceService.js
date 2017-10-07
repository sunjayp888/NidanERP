(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchAttendanceService', BatchAttendanceService);

    BatchAttendanceService.$inject = ['$http'];

    function BatchAttendanceService($http) {
        var service = {
            retrieveBatchAttendances: retrieveBatchAttendances,
            searchBatchAttendance: searchBatchAttendance,
            retrieveBatchAttendancesByBatchId: retrieveBatchAttendancesByBatchId,
            searchBatchAttendanceByDate: searchBatchAttendanceByDate,
            retrieveBatches: retrieveBatches,
            markAttendance: markAttendance,
            getBiometricData: getBiometricData
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

        function retrieveBatchAttendancesByBatchId(batchId,date, Paging, OrderBy) {

            var url = "/BatchAttendance/AttendanceList",
                data = {
                    batchId: batchId,
                    date:date,
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

        function searchBatchAttendanceByDate(FromDate, ToDate, BatchId, Paging, OrderBy) {
            var url = "/BatchAttendance/SearchByDate",
            data = {
                //batchId:BatchId,
                fromDate: FromDate,
                toDate: ToDate,
                batchId: BatchId,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function retrieveBatches() {

            var url = "/BatchAttendance/GetBatches";
            return $http.post(url);
        }

        function markAttendance(subjectId, sessionId, attendances) {

            var url = "/BatchAttendance/MarkAttendance";
            var data = { attendances: attendances, subjectId: subjectId, sessionId: sessionId }
            return $http.post(url, data);
        }

        function getBiometricData(batchId, attendanceDate, Paging, OrderBy) {

            var url = "/BatchAttendance/GetBiometricDataList",
                data = {
                    batchId: batchId,
                    attendanceDate:attendanceDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

    }
})();