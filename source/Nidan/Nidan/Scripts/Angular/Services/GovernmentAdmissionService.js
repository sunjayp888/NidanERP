(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('GovernmentAdmissionService', GovernmentAdmissionService);

    GovernmentAdmissionService.$inject = ['$http'];

    function GovernmentAdmissionService($http) {
        var service = {
            retrieveGovernmentAdmissions: retrieveGovernmentAdmissions,
            canDeleteGovernmentAdmission: canDeleteGovernmentAdmission,
            // deleteGovernmentAdmission: deleteGovernmentAdmission
            searchGovernmentAdmission: searchGovernmentAdmission
        };

        return service;

        function retrieveGovernmentAdmissions(Paging, OrderBy) {

            var url = "/GovernmentAdmission/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchGovernmentAdmission(SearchKeyword, Paging, OrderBy) {
            var url = "/GovernmentAdmission/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteGovernmentAdmission(id) {
            var url = "/GovernmentAdmission/CanDeleteGovernmentAdmission",
                data = { id: id };

            return $http.post(url, data);
        }

        //function deleteGovernmentAdmission(id) {
        //    var url = "/GovernmentAdmission/Delete",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}
    }
})();