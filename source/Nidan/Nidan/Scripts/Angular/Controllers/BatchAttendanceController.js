(function () {
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
        vm.retrieveBatches = retrieveBatches;
        vm.searchBatchAttendanceByDate = searchBatchAttendanceByDate;
        vm.retrieveBatchAttendancesByBatchId = retrieveBatchAttendancesByBatchId;
        vm.type = "";
        vm.initialise = initialise;
        vm.selectAll = selectAll;
        vm.allItemsSelected = false;
        vm.selectEntity = selectEntity;
        vm.markAttendance = markAttendance;
        vm.getBiometricData = getBiometricData;
        vm.MarkDate;
        vm.fromDate;
        vm.toDate;
        vm.batchId;

        function initialise() {
            vm.orderBy.property = "StudentCode";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("StudentCode");
        }

        function selectAll() {
            for (var i = 0; i < vm.batchAttendances.length; i++) {
                vm.batchAttendances[i].IsPresent = vm.allItemsSelected;
                vm.batchAttendances[i].InTime = $("#BatchAttendance_Attendance_InHour").val();
                vm.batchAttendances[i].OutTime = $("#BatchAttendance_Attendance_OutHour").val();
                vm.batchAttendances[i].AttendanceDate = $("#BatchAttendance_Attendance_AttendanceDate").val();
            }
        };

        function retrieveBatchAttendances() {
            return BatchAttendanceService.retrieveBatchAttendances(vm.paging, vm.orderBy)
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

        function searchBatchAttendanceByDate(fromDate, toDate, batchId) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.batchId = batchId;
            vm.orderBy.property = "AttendanceDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            //vm.batchId = batchId;
            return BatchAttendanceService.searchBatchAttendanceByDate(vm.fromDate, vm.toDate, vm.batchId, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.batchAttendances = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.batchAttendances.length === 0 ? "No Records Found" : "";
                  return vm.batchAttendances;
              });
        }

        function retrieveBatchAttendancesByBatchId() {
            vm.orderBy.property = "StudentCode";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BatchAttendanceService.retrieveBatchAttendancesByBatchId(vm.type,vm.MarkDate, vm.paging, vm.orderBy)
                    .then(function (response) {
                        vm.batchAttendances = response.data.Items;
                        vm.paging.totalPages = response.data.TotalPages;
                        vm.paging.totalResults = response.data.TotalResults;
                        return vm.batchAttendances;
                    });
        }

        function getBiometricData() {
            vm.orderBy.property = "StudentCode";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.batchId = $("#BatchAttendance_BatchId").val();
            vm.attendanceDate = $("#BatchAttendance_Attendance_AttendanceDate").val();
            return BatchAttendanceService.getBiometricData(vm.batchId, vm.attendanceDate, vm.paging, vm.orderBy)
                    .then(function (response) {
                        vm.batchAttendances = response.data.Items;
                        vm.paging.totalPages = response.data.TotalPages;
                        vm.paging.totalResults = response.data.TotalResults;
                        return vm.batchAttendances;
                    });
        }
      
        function pageChanged() {
            vm.fromDate = $("#fromDate").val();
            vm.toDate = $("#toDate").val();
            var path = window.location.pathname.split('/');
            if (path[2] == "BatchAttendance") {
                searchBatchAttendanceByDate(fromDate, toDate, batchId);
            }
            if (path[3] == "Create" ) {
                retrieveBatchAttendancesByBatchId();
            }
            
             
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

        function retrieveBatches() {
            return BatchAttendanceService.retrieveBatches().then(function (response) {
                vm.batches = response.data.Items;
                return vm.batches;
            });
        }

        function selectEntity() {
            // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
            for (var i = 0; i < vm.batchAttendances.length; i++) {
                if (!vm.batchAttendances[i].IsPresent) {
                    vm.batchAttendances[i].InTime = $("#BatchAttendance_Attendance_InHour").val();
                    vm.batchAttendances[i].OutTime = $("#BatchAttendance_Attendance_OutHour").val();
                    vm.batchAttendances[i].AttendanceDate = $("#BatchAttendance_Attendance_AttendanceDate").val();
                    vm.allItemsSelected = false;
                    return;
                }
            }

            //If not the check the "allItemsSelected" checkbox
            vm.allItemsSelected = true;
        };

        function markAttendance() {
            var subjectId = $("#BatchAttendance_SubjectId").val();
            var sessionId = $("#BatchAttendance_SessionId").val();
            return BatchAttendanceService.markAttendance(subjectId, sessionId,vm.batchAttendances).then(function (response) {
                vm.batches = response.data.Items;
                return vm.batches;
            });
        }

        }

})();
