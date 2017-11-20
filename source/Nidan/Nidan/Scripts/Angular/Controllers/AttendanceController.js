(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AttendanceController', AttendanceController);

    AttendanceController.$inject = ['$window', 'AttendanceService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AttendanceController($window, AttendanceService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.attendances = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAttendance = editAttendance;
        vm.searchAttendance = searchAttendance;
        vm.viewAttendance = viewAttendance;
        vm.searchAttendanceByDate = searchAttendanceByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "AttendanceDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("AttendanceDate");
        }

        function retrieveAttendances() {
            return AttendanceService.retrieveAttendances(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.attendances = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.attendances;
                });
        }

        function searchAttendance(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return AttendanceService.searchAttendance(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.attendances = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.attendances.length === 0 ? "No Records Found" : "";
                  return vm.attendances;
              });
        }

        function searchAttendanceByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return AttendanceService.searchAttendanceByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.attendances = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.attendances.length === 0 ? "No Records Found" : "";
                  return vm.attendances;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchAttendance(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchAttendanceByDate(vm.fromDate, vm.toDate);
            } else {
                return retrieveAttendances();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAttendances();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAttendance(id) {
            $window.location.href = "/Attendance/Edit/" + id;
        }

        function viewAttendance(attendanceId) {
            $window.location.href = "/Attendance/Edit/" + attendanceId;
        }

    }

})();
