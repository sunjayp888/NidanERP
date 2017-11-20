(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchPlannerService', BatchPlannerService);

    BatchPlannerService.$inject = ['$http'];

    function BatchPlannerService($http) {
        var service = {
            retrieveBatchPlanners: retrieveBatchPlanners
        };

        return service;

        function retrieveBatchPlanners(Paging, OrderBy) {

            var url = "/BatchPlanner/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();