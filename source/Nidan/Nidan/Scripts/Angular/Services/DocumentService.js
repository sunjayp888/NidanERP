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
            retrieveStudentDocuments: retrieveStudentDocuments,
            retrieveDocumentsType: retrieveDocumentsType,
            createDocument:createDocument
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
        function retrieveStudentDocuments(StudentCode, Paging, OrderBy) {
            var url = "/Document/StudentDocument",
                data = {
                    studentCode: StudentCode,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveDocumentsType() {
            var url = "/Document/DocumentTypes";
            return $http.post(url);
        }

        function createDocument(studentCode, documentTypeId, attachment) {

            var url = "/Document/CreateDocument",
                data = {
                    DocumentTypeId: documentTypeId,
                    Attachment: attachment,
                    StudentCode: studentCode
                };

            //return $http.post(url, data);

            var getModelAsFormData = function (data) {
                var dataAsFormData = new FormData();
                angular.forEach(data, function (value, key) {
                    dataAsFormData.append(key, value);
                });
                return dataAsFormData;
            };

            return $http({
                url: url,
                method: "POST",
                data: getModelAsFormData(data),
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
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