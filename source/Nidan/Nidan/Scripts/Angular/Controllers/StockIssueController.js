(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('StockIssueController', StockIssueController);

    StockIssueController.$inject = ['$window', 'StockIssueService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function StockIssueController($window, StockIssueService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.stockIssues = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchStockIssue = searchStockIssue;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "StockIssueId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("StockIssueId");
        }

        function retrieveStockIssues() {
            return StockIssueService.retrieveStockIssues(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.stockIssues = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.stockIssues;
                });
        }

        function pageChanged() {
            return retrieveStockIssues();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveStockIssues();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
