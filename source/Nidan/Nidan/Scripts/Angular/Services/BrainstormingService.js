(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BrainstormingService', BrainstormingService);

    BrainstormingService.$inject = ['$http'];

    function BrainstormingService($http) {
        var service = {
            retrieveBrainstormings: retrieveBrainstormings
        };

        return service;

        function retrieveBrainstormings(Paging, OrderBy) {

            var url = "/Event/BrainstormingList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();