(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BatchPrePlacementController', BatchPrePlacementController);

    BatchPrePlacementController.$inject = ['$window', 'BatchPrePlacementService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BatchPrePlacementController($window, BatchPrePlacementService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.batchPrePlacements = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBatchPrePlacement = editBatchPrePlacement;
        vm.searchBatchPrePlacement = searchBatchPrePlacement;
        vm.viewBatchPrePlacement = viewBatchPrePlacement;
        vm.searchBatchPrePlacementByDate = searchBatchPrePlacementByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveBatchPrePlacements() {
            return BatchPrePlacementService.retrieveBatchPrePlacements(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.batchPrePlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.batchPrePlacements;
                });
        }

       function searchBatchPrePlacement(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return BatchPrePlacementService.searchBatchPrePlacement(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.batchPrePlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.batchPrePlacements.length === 0 ? "No Records Found" : "";
                    return vm.batchPrePlacements;
                });
        }

        function searchBatchPrePlacementByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return BatchPrePlacementService.searchBatchPrePlacementByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.batchPrePlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.batchPrePlacements.length === 0 ? "No Records Found" : "";
                    return vm.batchPrePlacements;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchBatchPrePlacement(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchBatchPrePlacementByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveBatchPrePlacements();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBatchPrePlacements();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBatchPrePlacement(id) {
            $window.location.href = "/BatchPrePlacement/Edit/" + id;
        }

       function viewBatchPrePlacement(batchPrePlacementId) {
            $window.location.href = "/BatchPrePlacement/View/" + batchPrePlacementId;
        }

    }

})();
