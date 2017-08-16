(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AdmissionService', AdmissionService);

    AdmissionService.$inject = ['$http'];

    function AdmissionService($http) {
        var service = {
            retrieveAdmissions: retrieveAdmissions,
            canDeleteAdmission: canDeleteAdmission,
            deleteAdmission: deleteAdmission,
            searchAdmission: searchAdmission,
            retrieveDocumentsType: retrieveDocumentsType,
            searchAdmissionByDate: searchAdmissionByDate
        };

        return service;

        function retrieveAdmissions(Paging, OrderBy) {

            var url = "/Admission/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAdmission(SearchKeyword, Paging, OrderBy) {
            var url = "/Admission/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAdmissionByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Admission/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function retrieveDocumentsType(studentCode) {

            var url = "/Admission/DocumentList",
                data = {
                    studentCode: studentCode
                };

            return $http.post(url, data);
        }

        function canDeleteAdmission(id) {
            var url = "/Admission/CanDeleteAdmission",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteAdmission(id) {
            var url = "/Admission/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();