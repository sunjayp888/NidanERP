(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BatchController', BatchController);

    BatchController.$inject = ['$window', 'BatchService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BatchController($window, BatchService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.batches = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBatch = editBatch;
        //vm.canDeleteMobilization = canDeleteMobilization;
        //vm.deleteMobilization = deleteMobilization;
        //vm.searchMobilization = searchMobilization;
        vm.viewBatch = viewBatch;
        //vm.searchKeyword = "";
        //vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveBatches() {
            return BatchService.retrieveBatches(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.batches = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.batches;
                });
        }

        //function searchMobilization(searchKeyword) {
        //    vm.searchKeyword = searchKeyword;
        //    return MobilizationService.searchMobilization(vm.searchKeyword, vm.paging, vm.orderBy)
        //      .then(function (response) {
        //          vm.mobilizations = response.data.Items;
        //          vm.paging.totalPages = response.data.TotalPages;
        //          vm.paging.totalResults = response.data.TotalResults;
        //          vm.searchMessage = vm.mobilizations.length === 0 ? "No Records Found" : "";
        //          return vm.mobilizations;
        //      });
        //}

        function pageChanged() {
            return retrieveBatches();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBatches();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBatch(id) {
            $window.location.href = "/Batch/Edit/" + id;
        }

        //function canDeleteMobilization(id) {
        //    vm.loadingActions = true;
        //    vm.CanDeleteMobilization = false;
        //    $('.dropdown-menu').slideUp('fast');
        //    $('.' + id).toggle();
        //    MobilizationService.canDeleteMobilization(id).then(function (response) { vm.CanDeleteMobilization = response.data, vm.loadingActions = false });
        //}

        //function deleteMobilization(id) {
        //    return MobilizationService.deleteMobilization(id).then(function () { initialise(); });
        //};

        function viewBatch(batchId) {
            $window.location.href = "/Batch/Edit/" + batchId;
        }

    }

})();
