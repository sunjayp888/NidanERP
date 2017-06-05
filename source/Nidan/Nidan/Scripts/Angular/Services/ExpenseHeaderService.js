(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ExpenseHeaderService', ExpenseHeaderService);

    ExpenseHeaderService.$inject = ['$http'];

    function ExpenseHeaderService($http) {
        var service = {
            retrieveExpenseHeaders: retrieveExpenseHeaders,
            canDeleteExpenseHeader: canDeleteExpenseHeader,
            deleteExpenseHeader: deleteExpenseHeader,
            searchExpenseHeader: searchExpenseHeader
        };

        return service;

        function retrieveExpenseHeaders(Paging, OrderBy) {

            var url = "/ExpenseHeader/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchExpenseHeader(SearchKeyword, Paging, OrderBy) {
            var url = "/ExpenseHeader/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteExpenseHeader(id) {
            var url = "/ExpenseHeader/CanDeleteExpenseHeader",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteExpenseHeader(id) {
            var url = "/ExpenseHeader/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();