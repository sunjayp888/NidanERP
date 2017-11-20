(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ExpenseController', ExpenseController);

    ExpenseController.$inject = ['$window', 'ExpenseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ExpenseController($window, ExpenseService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.expenses = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editExpense = editExpense;
        //vm.canDeleteExpense = canDeleteExpense;
        vm.deleteExpense = deleteExpense;
        vm.searchExpense = searchExpense;
        vm.viewExpense = viewExpense;
        //vm.addExpense = addExpense;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.retrieveExpensesByCashMemo = retrieveExpensesByCashMemo;
        vm.initialise = initialise;
        vm.cashMemo = "";

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveExpenses() {
            return ExpenseService.retrieveExpenses(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.expenses;
                });
        }

        function retrieveExpensesByCashMemo(cashMemo) {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            vm.cashMemo = cashMemo == undefined ? $("#Expense_CashMemoNumbers").val() : cashMemo;
            return ExpenseService.retrieveExpensesByCashMemo(vm.cashMemo, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.expenses;
                });
        }

        function searchExpense(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ExpenseService.searchExpense(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.expenses.length === 0 ? "No Records Found" : "";
                    return vm.expenses;
                });
        }

        function pageChanged() {
            return retrieveExpenses();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveExpenses();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editExpense(id) {
            $window.location.href = "/Expense/Edit/" + id;
        }

        //function canDeleteOtherFee(id) {
        //    vm.loadingActions = true;
        //    vm.CanDeleteOtherFee = false;
        //    $('.dropdown-menu').slideUp('fast');
        //    $('.' + id).toggle();
        //    OtherFeeService.canDeleteOtherFee(id).then(function (response) { vm.CanDeleteOtherFee = response.data, vm.loadingActions = false });
        //}

        function viewExpense(cashMemo) {
            $window.location.href = "/Expense/Create/" + cashMemo;
        }

        function deleteExpense(centreId, expenseId, cashMemo) {
            return ExpenseService.deleteExpense(centreId, expenseId).then(function () {
                retrieveExpensesByCashMemo(cashMemo);
            });

        }
    }

})();
