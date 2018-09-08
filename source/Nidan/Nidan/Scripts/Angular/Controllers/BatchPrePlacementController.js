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
        vm.candidatePrePlacements = [];
        vm.batchPrePlacementId;
        vm.prePlacementActivityId;
        vm.candidatePrePlacementId;
        vm.candidatePrePlacementReportId;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBatchPrePlacement = editBatchPrePlacement;
        vm.searchBatchPrePlacement = searchBatchPrePlacement;
        vm.viewBatchPrePlacement = viewBatchPrePlacement;
        vm.searchBatchPrePlacementByDate = searchBatchPrePlacementByDate;
        vm.retrieveCandidatePrePlacementByBatchPrePlacementId = retrieveCandidatePrePlacementByBatchPrePlacementId;
        vm.openCandidatePrePlacementActivityModalPopUp = openCandidatePrePlacementActivityModalPopUp;
        vm.saveCandidatePrePlacementActivity = saveCandidatePrePlacementActivity;
        vm.openCandidatePrePlacementUpdateModalPopUp = openCandidatePrePlacementUpdateModalPopUp;
        vm.retrieveCandidatePrePlacementReportByBatchPrePlacementId = retrieveCandidatePrePlacementReportByBatchPrePlacementId;
        vm.openCandidatePrePlacementReportUpdateModalPopUp = openCandidatePrePlacementReportUpdateModalPopUp;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.candidatePrePlacementReports = [];
        vm.candidatePrePlacementData = [];
        vm.admissionId;
        vm.studentCode;
        vm.viewCandidatePrePlacementData = viewCandidatePrePlacementData;
        vm.searchCandidatePrePlacementData = searchCandidatePrePlacementData;
        vm.saveCandidatePrePlacementReport = saveCandidatePrePlacementReport;
        vm.initialise = initialise;
            
        function initialise() {
            vm.orderBy.property = "ScheduledStartDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("ScheduledStartDate");
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
            } else if (vm.batchPrePlacementId) {
                retrieveCandidatePrePlacementReportByBatchPrePlacementId(vm.batchPrePlacementId);
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

        function retrieveCandidatePrePlacementByBatchPrePlacementId(batchPrePlacementId) {
            vm.orderBy.property = "ScheduledStartDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.batchPrePlacementId = batchPrePlacementId;
            return BatchPrePlacementService.retrieveCandidatePrePlacementByBatchPrePlacementId(vm.batchPrePlacementId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidatePrePlacements;
                });
        }

        function openCandidatePrePlacementActivityModalPopUp(batchPrePlacementId) {
            vm.batchPrePlacementId = batchPrePlacementId;
            return BatchPrePlacementService.openCandidatePrePlacementActivityModalPopUp(batchPrePlacementId);
        }

        function saveCandidatePrePlacementActivity() {
            var candidatePrePlacement = {
                CandidatePrePlacementId: vm.candidatePrePlacementId === 0 ? 0 : vm.candidatePrePlacementId,
                BatchPrePlacementId: vm.batchPrePlacementId,
                PrePlacementActivityId: $("#CandidatePrePlacement_PrePlacementActivityId").val(),
                ScheduledStartDate: $("#txtScheduledStartDate").val(),
                ScheduledEndDate: $("#txtScheduledEndDate").val(),
                Remark: $("#txtRemark").val()
            }
            return BatchPrePlacementService.saveCandidatePrePlacementActivity(candidatePrePlacement)
                .then(function (response) {
                    retrieveCandidatePrePlacementByBatchPrePlacementId(vm.batchPrePlacementId);
                });
        }

        function saveCandidatePrePlacementReport(candidatePrePlacementId, studentCode) {
            var candidatePrePlacementReport = {
                CandidatePrePlacementReportId: vm.candidatePrePlacementReportId === 0 ? 0 : vm.candidatePrePlacementReportId,
                AdmissionId: vm.admissionId,
                ActualStartDate: $("#txtActualStartDate").val(),
                ActualEndDate: $("#txtActualEndDate").val(),
                MarkObtained: $("#txtMarkObtained").val(),
                TotalMark: $("#txtTotalMark").val(),
                Remark: $("#txtRemark").val(),
                CandidatePrePlacementId: candidatePrePlacementId,
                StudentCode: vm.studentCode
            }
            return BatchPrePlacementService.saveCandidatePrePlacementReport(candidatePrePlacementReport)
                .then(function (response) {
                    viewCandidatePrePlacementData(vm.admissionId);
                });
        }

        function openCandidatePrePlacementUpdateModalPopUp(candidatePrePlacementId) {
            vm.candidatePrePlacementId = candidatePrePlacementId;
            return BatchPrePlacementService.openCandidatePrePlacementUpdateModalPopUp(candidatePrePlacementId);
        }

        function openCandidatePrePlacementReportUpdateModalPopUp(candidatePrePlacementReportId, candidatePrePlacementId,studentCode) {
            vm.candidatePrePlacementReportId = candidatePrePlacementReportId;
            vm.candidatePrePlacementId = candidatePrePlacementId;
            vm.studentCode = studentCode;
            return BatchPrePlacementService.openCandidatePrePlacementReportUpdateModalPopUp(candidatePrePlacementReportId);
        }

        function retrieveCandidatePrePlacementReportByBatchPrePlacementId(batchPrePlacementId) {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.batchPrePlacementId = batchPrePlacementId;
            return BatchPrePlacementService.retrieveCandidatePrePlacementReportByBatchPrePlacementId(vm.batchPrePlacementId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacementReports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidatePrePlacementReports;
                });
        }

        function viewCandidatePrePlacementData(admissionId) {
            vm.admissionId = admissionId;
            window.location.href = "/BatchPrePlacement/CandidatePrePlacementData?admissionId=" + admissionId;
        }

        function searchCandidatePrePlacementData(admissionId) {
            vm.admissionId = admissionId == undefined ? getUrlParameter("admissionId") : admissionId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BatchPrePlacementService.searchCandidatePrePlacementData(vm.admissionId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacementData = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidatePrePlacementData;
                });
        }

        function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
        }
    }

})();
