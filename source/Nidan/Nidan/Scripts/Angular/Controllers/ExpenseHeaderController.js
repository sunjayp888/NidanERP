(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ExpenseHeaderController', ExpenseHeaderController);

    ExpenseHeaderController.$inject = ['$window', 'ExpenseHeaderService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ExpenseHeaderController($window, ExpenseHeaderService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.expenseHeaders = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editExpenseHeader = editExpenseHeader;
        vm.canDeleteExpenseHeader = canDeleteExpenseHeader;
        vm.deleteExpenseHeader = deleteExpenseHeader;
        vm.searchExpenseHeader = searchExpenseHeader;
        vm.viewExpenseHeader = viewExpenseHeader;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "ExpenseHeaderId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("ExpenseHeaderId");
        }

        function retrieveExpenseHeaders() {
            return ExpenseHeaderService.retrieveExpenseHeaders(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenseHeaders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.expenseHeaders;
                });
        }

        function searchExpenseHeader(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ExpenseHeaderService.searchExpenseHeader(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenseHeaders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.expenseHeaders.length === 0 ? "No Records Found" : "";
                    return vm.expenseHeaders;
                });
        }

        function pageChanged() {
            return retrieveExpenseHeaders();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveExpenseHeaders();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editExpenseHeader(id) {
            $window.location.href = "/ExpenseHeader/Edit/" + id;
        }

        function canDeleteExpenseHeader(id) {
            vm.loadingActions = true;
            vm.CanDeleteExpenseHeader = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            ExpenseHeaderService.canDeleteExpenseHeader(id).then(function (response) { vm.CanDeleteExpenseHeader = response.data, vm.loadingActions = false });
        }

        function deleteExpenseHeader(id) {
            return ExpenseHeaderService.deleteExpenseHeader(id).then(function () { initialise(); });
        };

        function viewExpenseHeader(expenseHeaderId) {
            $window.location.href = "/ExpenseHeader/Edit/" + expenseHeaderId;
        }

    }

})();
