(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('PlacementDocumentService', PlacementDocumentService);

    PlacementDocumentService.$inject = ['$http'];

    function PlacementDocumentService($http) {
        var service = {
            retrievePlacementDocuments: retrievePlacementDocuments,
            createPlacementDocument: createPlacementDocument,
            downloadPlacementDocument: downloadPlacementDocument
        };

        return service;

        function retrievePlacementDocuments(studentCode) {
            var url = "/CandidateFinalPlacement/DocumentList",
                data = {
                    studentCode: studentCode
                };
            return $http.post(url, data);
        }

        function createPlacementDocument(studentCode, documentTypeId, attachment) {

            var url = "/CandidateFinalPlacement/CreateDocument",
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

        function downloadPlacementDocument(guid) {
            var url = "/CandidateFinalPlacement/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();