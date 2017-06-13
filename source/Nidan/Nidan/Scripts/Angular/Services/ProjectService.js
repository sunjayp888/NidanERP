﻿(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ProjectService', ProjectService);

    ProjectService.$inject = ['$http'];

    function ProjectService($http) {
        var service = {
            retrieveProjects: retrieveProjects,
            deleteOtherFeeProject: deleteOtherFeeProject
        };

        return service;

        function retrieveProjects(Paging, OrderBy) {

            var url = "/Project/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
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