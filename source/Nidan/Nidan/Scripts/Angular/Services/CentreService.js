(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CentreService', CentreService);

    CentreService.$inject = ['$http'];

    function CentreService($http) {
        var service = {
            retrieveCentres: retrieveCentres
        };

        return service;

        function retrieveCentres(Paging, OrderBy) {

            var url = "/Centre/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();