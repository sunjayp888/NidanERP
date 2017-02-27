(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('DocumentService', DocumentService);

    DocumentService.$inject = ['$http'];

    function DocumentService($http) {
        var service = {
            retrieveDocuments: retrieveDocuments,
            canDeleteAbsenceType: canDeleteAbsenceType,
            deleteAbsenceType: deleteAbsenceType,
            retrieveStudentDocuments: retrieveStudentDocuments
        };

        return service;

        function retrieveDocuments(Paging, OrderBy) {

            var url = "/Document/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
        function retrieveStudentDocuments(StudentCode, Category, Paging, OrderBy) {
            var url = "/Document/StudentDocument",
                data = {
                    studentCode: StudentCode,
                    category: Category,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }


        function canDeleteAbsenceType(id) {
            var url = "/AbsenceType/CanDeleteAbsenceType",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteAbsenceType(id) {
            var url = "/AbsenceType/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();