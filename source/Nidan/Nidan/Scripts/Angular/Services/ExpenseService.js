(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ExpenseService', ExpenseService);

    ExpenseService.$inject = ['$http'];

    function ExpenseService($http) {
        var service = {
            retrieveExpenses: retrieveExpenses,
            //canDeleteOtherFee: canDeleteOtherFee,
            deleteExpense: deleteExpense,
            searchExpense: searchExpense,
            retrieveExpensesByCashMemo: retrieveExpensesByCashMemo
        };

        return service;

        function retrieveExpenses(Paging, OrderBy) {

            var url = "/Expense/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchExpense(SearchKeyword, Paging, OrderBy) {
            var url = "/Expense/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        //function canDeleteOtherFee(id) {
        //    var url = "/OtherFee/CanDeleteOtherFee",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}

        function deleteExpense(centreId, expenseId, cashMemo) {
            var url = "/Expense/Delete",
                data = {
                    centreId: centreId,
                    expenseId: expenseId,
                    cashMemo: cashMemo
                };

            return $http.post(url, data);
        }

        function retrieveExpensesByCashMemo(cashMemo, Paging, OrderBy) {
            var url = "/Expense/ListByCashMemo",
                data = {
                    cashMemo: cashMemo,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

    }
})();