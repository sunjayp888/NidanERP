(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('StudentCounsellingDocumentService', StudentCounsellingDocumentService);

    StudentCounsellingDocumentService.$inject = ['$http'];

    function StudentCounsellingDocumentService($http) {
        var service = {
            retrieveStudentCounsellingDocuments: retrieveStudentCounsellingDocuments,
            createStudentCounsellingDocument: createStudentCounsellingDocument,
            downloadStudentCounsellingDocument: downloadStudentCounsellingDocument
        };

        return service;
       
        function retrieveStudentCounsellingDocuments(StudentCode) {
            var url = "/Counselling/DocumentList",
                data = {
                    studentCode: StudentCode
                };
            return $http.post(url, data);
        }

        function createStudentCounsellingDocument(studentCode, documentTypeId, attachment) {

            var url = "/Counselling/CreateDocument",
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

        function downloadStudentCounsellingDocument(guid) {
            var url = "/Counselling/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();