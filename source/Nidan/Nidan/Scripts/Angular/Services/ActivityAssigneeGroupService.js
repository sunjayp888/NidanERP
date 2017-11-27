(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ActivityAssigneeGroupService', ActivityAssigneeGroupService);

    ActivityAssigneeGroupService.$inject = ['$http'];

    function ActivityAssigneeGroupService($http) {
        var service = {
            retrieveActivityAssigneeGroups: retrieveActivityAssigneeGroups,
            retrieveActivityAssignPersonnels: retrieveActivityAssignPersonnels,
            retrieveUnassignedActivityAssignPersonnels: retrieveUnassignedActivityAssignPersonnels,
            unassignActivityAssignPersonnel: unassignActivityAssignPersonnel,
            assignActivityAssignPersonnel: assignActivityAssignPersonnel
        };

        return service;

        function retrieveActivityAssigneeGroups(Paging, OrderBy) {

            var url = "/ActivityAssigneeGroup/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveActivityAssignPersonnels(centreId, activityAssigneeGroupId) {
            var url = "/ActivityAssigneeGroup/ActivityAssignPersonnel",
                data = {
                    centreId: centreId,
                    activityAssigneeGroupId: activityAssigneeGroupId
                };

            return $http.post(url, data);
        }

        function retrieveUnassignedActivityAssignPersonnels(centreId, activityAssigneeGroupId) {
            var url = "/ActivityAssigneeGroup/UnassignedPersonnels",
                data = {
                    centreId: centreId,
                    activityAssigneeGroupId: activityAssigneeGroupId
                };

            return $http.post(url, data);
        }

        function unassignActivityAssignPersonnel(centreId, activityAssigneeGroupId, personnelId) {
            var url = "/ActivityAssigneeGroup/UnassignedActivityAsignGroupPersonnel",
                data = {
                    centreId: centreId,
                    activityAssigneeGroupId: activityAssigneeGroupId,
                    personnelId: personnelId
                };
            return $http.post(url, data);
        }

        function assignActivityAssignPersonnel(centreId, activityAssigneeGroupId, personnelId) {
            var url = "/ActivityAssigneeGroup/AssignPersonnel",
                data = {
                    centreId: centreId,
                    activityAssigneeGroupId: activityAssigneeGroupId,
                    personnelId: personnelId
                };

            return $http.post(url, data);
        }
    }
})();