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
        vm.ddBatch = 0;
        vm.rooms = [];
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
        vm.retrieveRoomByHours = retrieveRoomByHours;
        vm.searchBatchByDate = searchBatchByDate;
        vm.viewBatch = viewBatch;
        vm.changeBatch = changeBatch;
        vm.ddBatch = 0;
        vm.centreBatchCount = 0;
        vm.retrieveBatchByCentre = retrieveBatchByCentre;
        vm.hours = "";
        //vm.searchKeyword = "";
        //vm.searchMessage = "";
        vm.initialise=initialise();

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

        function retrieveBatchByCentre(centreId) {
            vm.centreId = centreId;
            return BatchService.retrieveBatchByCentre(vm.centreId)
                .then(function (response) {
                    vm.centreBatchError = false;
                    vm.batches = response.data.Items;
                    if (vm.batches.length > 0) {
                        vm.centreBatchCount = vm.batches.length;

                    } else {
                        vm.batches = true;
                        vm.centreBatchCount = 0;
                    }
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                });
        }

        function changeBatch(ddBatch) {
            vm.ddBatch = ddBatch;
        }

        function searchBatchByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return BatchService.searchBatchByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.batches = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.batches.length === 0 ? "No Records Found" : "";
                  return vm.batches;
              });
        }

        function retrieveRoomByHours() {
            return BatchService.retrieveRoomByHours(vm.hours).then(function (response) {
                vm.rooms = response.data;
                return vm.rooms;
            });
        }

        function pageChanged() {
            if (vm.fromDate && vm.toDate) {
                searchBatchByDate(vm.fromDate, vm.toDate);
            } else {
                return retrieveBatches();
            }
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
            $window.location.href = "/Batch/View/" + batchId;
        }

    }

})();
