(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BatchPlannerController', BatchPlannerController);

    BatchPlannerController.$inject = ['$window', 'BatchPlannerService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BatchPlannerController($window, BatchPlannerService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.batchPlanners = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBatchPlanner = editBatchPlanner;
        vm.viewBatchPlanner = viewBatchPlanner;
        initialise();

        function initialise() {
            vm.orderBy.property = "BatchPlannerId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("BatchPlannerId");
        }

        function retrieveBatchPlanners() {
            return BatchPlannerService.retrieveBatchPlanners(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.batchPlanners = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.batchPlanners;
                });
        }

        function pageChanged() {
            return retrieveBatchPlanners();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBatchPlanners();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBatchPlanner(id) {
            $window.location.href = "/BatchPlanner/Edit/" + id;
        }

        function viewBatchPlanner(batchPlannerId) {
            $window.location.href = "/BatchPlanner/View/" + batchPlannerId;
        }
    }
})();
