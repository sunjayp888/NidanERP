(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CommercialAdmissionService', CommercialAdmissionService);

    CommercialAdmissionService.$inject = ['$http'];

    function CommercialAdmissionService($http) {
        var service = {
            retrieveCommercialAdmissions: retrieveCommercialAdmissions,
            canDeleteCommercialAdmission: canDeleteCommercialAdmission,
            // deleteCommercialAdmission: deleteCommercialAdmission
            searchCommercialAdmission: searchCommercialAdmission
        };

        return service;

        function retrieveCommercialAdmissions(Paging, OrderBy) {

            var url = "/CommercialAdmission/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCommercialAdmission(SearchKeyword, Paging, OrderBy) {
            var url = "/CommercialAdmission/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteCommercialAdmission(id) {
            var url = "/CommercialAdmission/CanDeleteCommercialAdmission",
                data = { id: id };

            return $http.post(url, data);
        }

        //function deleteCommercialAdmission(id) {
        //    var url = "/CommercialAdmission/Delete",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}
    }
})();