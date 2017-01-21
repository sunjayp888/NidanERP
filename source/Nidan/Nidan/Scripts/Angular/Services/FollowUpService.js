(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FollowUpService', FollowUpService);

    FollowUpService.$inject = ['$http'];

    function FollowUpService($http) {
        var service = {
            retrieveFollowUps: retrieveFollowUps,
            canDeleteAbsenceType: canDeleteAbsenceType,
            deleteAbsenceType: deleteAbsenceType
        };

        return service;

        function retrieveFollowUps(Paging, OrderBy) {

            var url = "/FollowUp/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteAbsenceType(id) {
            var url = "/AbsenceType/CanDeleteAbsenceType",
                data = {id: id};

            return $http.post(url, data);
        }

          function deleteAbsenceType(id) {
            var url = "/AbsenceType/Delete",
                data = {id: id};

            return $http.post(url, data);
        }
    }
})();