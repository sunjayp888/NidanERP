(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidateFinalPlacementController', CandidateFinalPlacementController);

    CandidateFinalPlacementController.$inject = ['$window', 'CandidateFinalPlacementService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidateFinalPlacementController($window, CandidateFinalPlacementService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidateFinalPlacements = [];
        vm.batches = [];
        vm.retrieveBatches = retrieveBatches;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchCandidateFinalPlacement = searchCandidateFinalPlacement;
        vm.searchCandidateFinalPlacementByDate = searchCandidateFinalPlacementByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveCandidateFinalPlacementByBatchId = retrieveCandidateFinalPlacementByBatchId;
        vm.candidateFinalPlacementId;
        vm.batchId;
        vm.retrieveCandidateFinalPlacementByAdmissionId = retrieveCandidateFinalPlacementByAdmissionId;
        vm.admissionId;

        function initialise() {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveBatches();
            order("AdmissionId");
        }

        function retrieveCandidateFinalPlacementByBatchId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CandidateFinalPlacementService.retrieveCandidateFinalPlacementByBatchId(vm.batchId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFinalPlacements = response.data;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidateFinalPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidateFinalPlacements;
                });
        }

        function searchCandidateFinalPlacement(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return CandidateFinalPlacementService.searchCandidateFinalPlacement(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFinalPlacements = response.data;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidateFinalPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidateFinalPlacements;
                });
        }
        
        function searchCandidateFinalPlacementByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return CandidateFinalPlacementService.searchCandidateFinalPlacementByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFinalPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidateFinalPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidateFinalPlacements;
                });
        }

        function retrieveCandidateFinalPlacementByAdmissionId(admissionId) {
            vm.admissionId = admissionId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CandidateFinalPlacementService.retrieveCandidateFinalPlacementByAdmissionId(vm.admissionId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFinalPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidateFinalPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidateFinalPlacements;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCandidateFinalPlacement(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchCandidateFinalPlacementByDate(vm.fromDate, vm.toDate);
            }
            //else {
            //    return retrieveCandidateFinalPlacementByBatchId(vm.batchId);
            //}
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCandidateFinalPlacementByBatchId(vm.batchId);
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function retrieveBatches() {
            return CandidateFinalPlacementService.retrieveBatches().then(function (response) {
                vm.batches = response.data.Items;
                return vm.batches;
            });
        }
    }

})();
