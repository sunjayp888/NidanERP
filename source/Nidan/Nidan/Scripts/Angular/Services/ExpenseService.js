(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ExpenseService', ExpenseService);

    ExpenseService.$inject = ['$http'];

    function ExpenseService($http) {
        var service = {
            retrieveExpenses: retrieveExpenses,
            retrieveCentres: retrieveCentres,
            deleteExpense: deleteExpense,
            searchExpense: searchExpense,
            retrieveExpensesByCashMemo: retrieveExpensesByCashMemo,
            expenseLimitCheck: expenseLimitCheck,
            dateInCurrentWeek: dateInCurrentWeek,
            searchExpenseByDateCentreId: searchExpenseByDateCentreId,
            searchExpenseByDate: searchExpenseByDate,
            searchExpenseHeaderGridByDate:searchExpenseHeaderGridByDate
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

        function searchExpenseByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Expense/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchExpenseHeaderGridByDate(CentreId,FromDate, ToDate, Paging, OrderBy) {
            var url = "/Expense/SearchExpenseHeaderGridByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    centreId: CentreId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

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

        function expenseLimitCheck(expenseHeaderId) {
            var url = "/Expense/ExpenseLimitCheck",
                data = {
                    expenseHeaderId: expenseHeaderId
                };
            return $http.post(url, data);
        }

        function dateInCurrentWeek(expenseId) {
            var url = "/Expense/IsDateInCurrentWeek",
                data = {
                    expenseId: expenseId
                };
            return $http.post(url, data);
        }

        function retrieveCentres() {

            var url = "/Expense/GetCentres";
            return $http.post(url);
        }

        function searchExpenseByDateCentreId(FromDate, ToDate, CentreId, Paging, OrderBy) {
            var url = "/Expense/SearchByDateCentreId",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    centreId: CentreId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();