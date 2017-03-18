(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CounsellingDocumentService', CounsellingDocumentService);

    CounsellingDocumentService.$inject = ['$http'];

    function CounsellingDocumentService($http) {
        var service = {
            createCounsellingDocument: createCounsellingDocument,
            retrieveCounsellingDocuments: retrieveCounsellingDocuments,
            deleteCounsellingDocument: deleteCounsellingDocument,
            retrieveStudentDocuments: retrieveStudentDocuments
        };

        return service;

        function createCounsellingDocument(jobTitleId, name, attachment) {

            var url = "/JobTitleDocument/Create",
                data = {
                    JobTitleId: jobTitleId,
                    Name: name,
                    Attachment: attachment
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

        function retrieveCounsellingDocuments(jobTitleId, Paging) {

            var url = "/JobTitleDocument/List",
                data = {
                    id: jobTitleId,
                    paging: Paging
                };

            return $http.post(url, data);
        }

        function deleteCounsellingDocument(id) {
            var url = "/JobTitleDocument/Delete",
              data = {
                  id: id
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

    }
})();