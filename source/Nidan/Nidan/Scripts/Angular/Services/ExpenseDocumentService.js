(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ExpenseDocumentService', ExpenseDocumentService);

    ExpenseDocumentService.$inject = ['$http'];

    function ExpenseDocumentService($http) {
        var service = {
            retrieveExpenseDocuments: retrieveExpenseDocuments,
            createExpenseDocument: createExpenseDocument,
            downloadExpenseDocument: downloadExpenseDocument
        };

        return service;
       
        function retrieveExpenseDocuments(StudentCode) {
            var url = "/Expense/DocumentList",
                data = {
                    studentCode: StudentCode
                };
            return $http.post(url, data);
        }

        function createExpenseDocument(studentCode, documentTypeId, attachment) {

            var url = "/Expense/CreateDocument",
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

        function downloadExpenseDocument(guid) {
            var url = "/Expense/DownloadDocument",
                data = {
                    id: guid
                };
            return $http.post(url, data);
        }
    }
})();