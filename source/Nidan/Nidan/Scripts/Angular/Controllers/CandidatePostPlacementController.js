(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidatePostPlacementController', CandidatePostPlacementController);

    CandidatePostPlacementController.$inject = ['$window', 'CandidatePostPlacementService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidatePostPlacementController($window, CandidatePostPlacementService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidatePostPlacements = [];
        vm.batches = [];
        vm.retrieveBatches = retrieveBatches;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchCandidatePostPlacement = searchCandidatePostPlacement;
        vm.searchCandidatePostPlacementByDate = searchCandidatePostPlacementByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveCandidatePostPlacementByBatchId = retrieveCandidatePostPlacementByBatchId;
        vm.candidatePostPlacementId;
        vm.batchId;
        vm.retrieveCandidatePostPlacementByAdmissionId = retrieveCandidatePostPlacementByAdmissionId;
        vm.admissionId;

        function initialise() {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveBatches();
            order("AdmissionId");
        }

        function retrieveCandidatePostPlacementByBatchId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CandidatePostPlacementService.retrieveCandidatePostPlacementByBatchId(vm.batchId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePostPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePostPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidatePostPlacements;
                });
        }

        function searchCandidatePostPlacement(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return CandidatePostPlacementService.searchCandidatePostPlacement(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePostPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePostPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidatePostPlacements;
                });
        }

        function searchCandidatePostPlacementByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return CandidatePostPlacementService.searchCandidatePostPlacementByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePostPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePostPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidatePostPlacements;
                });
        }

        function retrieveCandidatePostPlacementByAdmissionId(admissionId) {
            vm.admissionId = admissionId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CandidatePostPlacementService.retrieveCandidatePostPlacementByAdmissionId(vm.admissionId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePostPlacements = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePostPlacements.length === 0 ? "No Records Found" : "";
                    return vm.candidatePostPlacements;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCandidatePostPlacement(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchCandidatePostPlacementByDate(vm.fromDate, vm.toDate);
            }
            //else {
            //    return retrieveCandidateFinalPlacementByBatchId(vm.batchId);
            //}
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCandidatePostPlacementByBatchId(vm.batchId);
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function retrieveBatches() {
            return CandidatePostPlacementService.retrieveBatches().then(function (response) {
                vm.batches = response.data.Items;
                return vm.batches;
            });
        }
    }

})();
