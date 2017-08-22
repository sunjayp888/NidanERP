(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('TrainerDocumentService', TrainerDocumentService);

    TrainerDocumentService.$inject = ['$http'];

    function TrainerDocumentService($http) {
        var service = {
            retrieveTrainerDocuments: retrieveTrainerDocuments,
            createTrainerDocument: createTrainerDocument,
            downloadTrainerDocument: downloadTrainerDocument
        };

        return service;

        function retrieveTrainerDocuments(StudentCode) {
            var url = "/Trainer/DocumentList",
                data = {
                    studentCode: StudentCode
                };
            return $http.post(url, data);
        }

        function createTrainerDocument(studentCode, documentTypeId, attachment) {

            var url = "/Trainer/CreateDocument",
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

        function downloadTrainerDocument(guid) {
            var url = "/Trainer/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();