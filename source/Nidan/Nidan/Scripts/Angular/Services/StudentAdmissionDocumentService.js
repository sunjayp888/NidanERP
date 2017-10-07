(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('StudentAdmissionDocumentService', StudentAdmissionDocumentService);

    StudentAdmissionDocumentService.$inject = ['$http'];

    function StudentAdmissionDocumentService($http) {
        var service = {
            retrieveStudentAdmissionDocuments: retrieveStudentAdmissionDocuments,
            createStudentAdmissionDocument: createStudentAdmissionDocument,
            downloadStudentAdmissionDocument: downloadStudentAdmissionDocument
        };

        return service;
       
        function retrieveStudentAdmissionDocuments(StudentCode) {
            var url = "/Admission/DocumentList",
                data = {
                    studentCode: StudentCode
                };
            return $http.post(url, data);
        }

        function createStudentAdmissionDocument(studentCode, documentTypeId, attachment) {

            var url = "/Admission/CreateDocument",
                data = {
                    documentTypeId: documentTypeId,
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

        function downloadStudentAdmissionDocument(guid) {
            var url = "/Admission/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();