(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('StockIssueService', StockIssueService);

    StockIssueService.$inject = ['$http'];

    function StockIssueService($http) {
        var service = {
            retrieveStockIssues: retrieveStockIssues
        };

        return service;

        function retrieveStockIssues(Paging, OrderBy) {

            var url = "/StockIssue/StockIssueList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();