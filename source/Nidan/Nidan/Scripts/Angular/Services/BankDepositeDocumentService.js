(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BankDepositeDocumentService', BankDepositeDocumentService);

    BankDepositeDocumentService.$inject = ['$http'];

    function BankDepositeDocumentService($http) {
        var service = {
            retrieveBankDepositeDocuments: retrieveBankDepositeDocuments,
            createBankDepositeDocument: createBankDepositeDocument,
            downloadBankDepositeDocument: downloadBankDepositeDocument
        };

        return service;

        function retrieveBankDepositeDocuments(studentCode) {
            var url = "/BankDeposite/DocumentList",
                data = {
                    studentCode: studentCode
                };
            return $http.post(url, data);
        }

        function createBankDepositeDocument(studentCode, documentTypeId, attachment) {

            var url = "/BankDeposite/CreateDocument",
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

        function downloadBankDepositeDocument(guid) {
            var url = "/BankDeposite/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();