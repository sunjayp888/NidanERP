(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('MobilizationService', MobilizationService);

    MobilizationService.$inject = ['$http'];

    function MobilizationService($http) {
        var service = {
            retrieveMobilizations: retrieveMobilizations,
            canDeleteMobilization: canDeleteMobilization,
            deleteMobilization: deleteMobilization
        };

        return service;

        function retrieveMobilizations(Paging, OrderBy) {

            var url = "/Mobilization/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteMobilization(id) {
            var url = "/Mobilization/CanDeleteMobilization",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteMobilization(id) {
            var url = "/Mobilization/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();