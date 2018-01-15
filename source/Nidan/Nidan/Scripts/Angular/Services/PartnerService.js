(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('PartnerService', PartnerService);

    PartnerService.$inject = ['$http'];

    function PartnerService($http) {
        var service = {
            retrievePartners: retrievePartners,
        };

        return service;

        function retrievePartners(Paging, OrderBy) {

            var url = "/Partner/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();