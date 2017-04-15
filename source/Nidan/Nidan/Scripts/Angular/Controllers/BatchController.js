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
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        vm.fees = [];
        vm.retrieveFees = retrieveFees;
        vm.downPayments = [];
        vm.retrieveFees = retrieveDownPayments;
        vm.durations = [];
        vm.retrieveDurations = retrieveDurations;
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


        function retrieveCourses(courseInstallmentId) {
            return BatchService.retrieveCourses(courseInstallmentId).then(function () {
                vm.courses = response.data;
            });
        };

        function retrieveFees(courseInstallmentId) {
            return BatchService.retrieveFees(courseInstallmentId).then(function () {
                vm.fees = response.data;
            });
        };

        function retrieveDownPayments(courseInstallmentId) {
            return BatchService.retrieveDownPayments(courseInstallmentId).then(function () {
                vm.downPayments = response.data;
            });
        };

        function retrieveDurations(courseInstallmentId) {
            return BatchService.retrieveDurations(courseInstallmentId).then(function () {
                vm.durations = response.data;
            });
        };

        function viewBatch(batchId) {
            $window.location.href = "/Batch/Edit/" + batchId;
        }

    }

})();
