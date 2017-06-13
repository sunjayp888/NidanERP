(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OtherFeeProjectService', OtherFeeProjectService);

    OtherFeeProjectService.$inject = ['$http'];

    function OtherFeeProjectService($http) {
        var service = {
            createOtherFeeProject: createOtherFeeProject,
            retrieveOtherFeeProjects: retrieveOtherFeeProjects,
            deleteOtherFeeProject: deleteOtherFeeProject
        };

        return service;

        function createOtherFeeProject(otherFeeId, projectId) {

            var url = "/OtherFeeProject/Create",
                data = {
                    OtherFeeId: otherFeeId,
                    ProjectId: projectId
                };

            return $http.post(url, data);
        }

        function retrieveOtherFeeProjects(otherFeeId) {

            var url = "/OtherFeeProject/List",
                data = {
                    otherFeeId: otherFeeId
                };

            return $http.post(url, data);
        }

        function deleteOtherFeeProject(otherFeeId, projectId) {
            var url = "/OtherFeeProject/Delete",
              data = {
                  otherFeeId: otherFeeId,
                  projectId: projectId
              };
            return $http.post(url, data);
        }
    }
})();