(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('StockPurchaseController', StockPurchaseController);

    StockPurchaseController.$inject = [
        '$window', 'StockPurchaseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'
    ];

    function StockPurchaseController($window, StockPurchaseService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.stockPurchases = [];
        vm.stockIssues = [];
        vm.StockPurchaseId;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editStockPurchase = editStockPurchase;
        vm.searchStockPurchase = searchStockPurchase;
        vm.searchStockPurchaseByDate = searchStockPurchaseByDate;
        vm.viewStockPurchase = viewStockPurchase;
        vm.retrieveStockIssuesByStockPurchaseId = retrieveStockIssuesByStockPurchaseId;
        vm.retrieveStockPurchaseByStationary = retrieveStockPurchaseByStationary;
        vm.retrieveStockPurchaseBySector = retrieveStockPurchaseBySector;
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

        function retrieveStockIssuesByStockPurchaseId(StockPurchaseId) {
            vm.StockPurchaseId;
            vm.orderBy.property = "IssuedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return StockPurchaseService.retrieveStockIssuesByStockPurchaseId(StockPurchaseId, vm.paging, vm.orderBy)
            .then(function (response) {
                vm.stockIssues = response.data.Items;
                vm.paging.totalPages = response.data.TotalPages;
                vm.paging.totalResults = response.data.TotalResults;
                return vm.stockIssues;
            });
        }

        function retrieveStockPurchaseBySector() {
            vm.orderBy.property = "StockPurchaseDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return StockPurchaseService.retrieveStockPurchaseBySector(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.stockPurchases = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.stockPurchases;
                });
        }

        function retrieveStockPurchaseByStationary() {
            vm.orderBy.property = "StockPurchaseDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return StockPurchaseService.retrieveStockPurchaseByStationary(vm.paging, vm.orderBy)
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
