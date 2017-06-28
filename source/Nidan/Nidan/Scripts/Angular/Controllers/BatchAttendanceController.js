﻿(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BatchAttendanceController', BatchAttendanceController);

    BatchAttendanceController.$inject = ['$window', 'BatchAttendanceService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BatchAttendanceController($window, BatchAttendanceService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.batchAttendances = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBatchAttendance = editBatchAttendance;
        vm.searchBatchAttendance = searchBatchAttendance;
        vm.viewBatchAttendance = viewBatchAttendance;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.batches = [];
        //vm.retrieveBatches = retrieveBatches;
        vm.subjects = [];
        vm.retrieveSubjects = retrieveSubjects;
        vm.sessions = [];
        vm.retrieveSessions = retrieveSessions;
        vm.retrieveBatchAttendancesByBatchId = retrieveBatchAttendancesByBatchId;
        vm.type = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "BatchAttendanceId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("BatchAttendanceId");
        }

        function retrieveBatchAttendances() {
            return BatchAttendanceService.retrieveBatchAttendances(vm.paging, vm.orderBy)
                    .then(function (response) {
                        vm.batchAttendances = response.data.Items;
                        vm.paging.totalPages = response.data.TotalPages;
                        vm.paging.totalResults = response.data.TotalResults;
                        return vm.batchAttendances;
                    });
        }

        function retrieveBatchAttendancesByBatchId() {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BatchAttendanceService.retrieveBatchAttendancesByBatchId(vm.type, vm.paging, vm.orderBy)
                    .then(function (response) {
                        vm.batchAttendances = response.data.Items;
                        vm.paging.totalPages = response.data.TotalPages;
                        vm.paging.totalResults = response.data.TotalResults;
                        return vm.batchAttendances;
                    });
        }

        function searchBatchAttendance(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return BatchAttendanceService.searchBatchAttendance(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.batchAttendances = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.batchAttendances.length === 0 ? "No Records Found" : "";
                  return vm.batchAttendances;
              });
        }

        function pageChanged() {
            return retrieveBatchAttendances();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBatchAttendances();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBatchAttendance(id) {
            $window.location.href = "/BatchAttendance/Edit/" + id;
        }

        function viewBatchAttendance(batchId) {
            $window.location.href = "/BatchAttendance/AttendanceList/" + batchId;
        }

        function retrieveSessions(subjectId) {
            return BatchAttendanceService.retrieveSessions(subjectId).then(function () {
                vm.sessions = response.data;
            });
        };

        function retrieveSubjects(batchId) {
            return BatchAttendanceService.retrieveSubjects(batchId).then(function () {
                vm.subjects = response.data;
            });
        };
    }

})();
