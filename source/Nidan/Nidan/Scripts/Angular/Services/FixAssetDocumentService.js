(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FixAssetDocumentService', FixAssetDocumentService);

    FixAssetDocumentService.$inject = ['$http'];

    function FixAssetDocumentService($http) {
        var service = {
            retrieveFixAssetDocuments: retrieveFixAssetDocuments,
            createFixAssetDocument: createFixAssetDocument,
            downloadFixAssetDocument: downloadFixAssetDocument
        };

        return service;

        function retrieveFixAssetDocuments(studentCode) {
            var url = "/FixAsset/DocumentList",
                data = {
                    studentCode: studentCode
                };
            return $http.post(url, data);
        }

        function createFixAssetDocument(studentCode, documentTypeId, attachment) {

            var url = "/FixAsset/CreateDocument",
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

        function downloadFixAssetDocument(guid) {
            var url = "/FixAsset/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();