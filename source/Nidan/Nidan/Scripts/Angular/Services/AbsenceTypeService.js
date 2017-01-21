(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AbsenceTypeService', AbsenceTypeService);

    AbsenceTypeService.$inject = ['$http'];

    function AbsenceTypeService($http) {
        var service = {
            retrieveAbsenceTypes: retrieveAbsenceTypes,
            canDeleteAbsenceType: canDeleteAbsenceType,
            deleteAbsenceType: deleteAbsenceType
        };

        return service;

        function retrieveAbsenceTypes(Paging, OrderBy) {

            var url = "/AbsenceType/List",
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