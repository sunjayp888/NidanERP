(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('StockPurchaseController', StockPurchaseController);

    StockPurchaseController.$inject = ['$window', 'StockPurchaseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function StockPurchaseController($window, StockPurchaseService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.stockPurchases = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editStockPurchase = editStockPurchase;
        vm.searchStockPurchase = searchStockPurchase;
        vm.searchStockPurchaseByDate = searchStockPurchaseByDate;
        vm.viewStockPurchase = viewStockPurchase;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "StockPurchaseDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("StockPurchaseDate");
        }

        function retrieveStockPurchases() {
            return StockPurchaseService.retrieveStockPurchases(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.stockPurchases = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.stockPurchases;
                });
        }

        function pageChanged() {
            retrieveStockPurchases();

        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveStockPurchases();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editStockPurchase(id) {
            $window.location.href = "/StockPurchase/Edit/" + id;
        }

        function searchStockPurchaseByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return StockPurchaseService.searchStockPurchaseByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.stockPurchases = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.stockPurchases.length === 0 ? "No Records Found" : "";
                  return vm.stockPurchases;
              });
        }

        function searchStockPurchase(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return StockPurchaseService.searchStockPurchase(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.stockPurchases = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.stockPurchases.length === 0 ? "No Records Found" : "";
                  return vm.stockPurchases;
              });
        }

        function viewStockPurchase(stockPurchaseId) {
            $window.location.href = "/StockPurchase/Edit/" + stockPurchaseId;
        }
    }

})();
