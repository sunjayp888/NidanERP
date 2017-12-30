(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidatePrePlacementActivityController', CandidatePrePlacementActivityController);

    CandidatePrePlacementActivityController.$inject = ['$window', 'CandidatePrePlacementActivityService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidatePrePlacementActivityController($window, CandidatePrePlacementActivityService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidatePrePlacementActivities = [];
        vm.batches = [];
        vm.retrieveBatches = retrieveBatches;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchCandidatePrePlacementActivity = searchCandidatePrePlacementActivity;
        vm.searchCandidatePrePlacementActivityByDate = searchCandidatePrePlacementActivityByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveCandidatePrePlacementActivityByBatchId = retrieveCandidatePrePlacementActivityByBatchId;
        vm.candidatePrePlacementActivityId;
        vm.batchId;

        function initialise() {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveBatches();
            order("AdmissionId");
        }

        function retrieveCandidatePrePlacementActivityByBatchId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CandidatePrePlacementActivityService.retrieveCandidatePrePlacementActivityByBatchId(vm.batchId,vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacementActivities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePrePlacementActivities.length === 0 ? "No Records Found" : "";
                    return vm.candidatePrePlacementActivities;
                });
        }

        function searchCandidatePrePlacementActivity(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return CandidatePrePlacementActivityService.searchCandidatePrePlacementActivity(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacementActivities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePrePlacementActivities.length === 0 ? "No Records Found" : "";
                    return vm.candidatePrePlacementActivities;
                });
        }
        
        function searchCandidatePrePlacementActivityByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return CandidatePrePlacementActivityService.searchCandidatePrePlacementActivityByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidatePrePlacementActivities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.candidatePrePlacementActivities.length === 0 ? "No Records Found" : "";
                    return vm.candidatePrePlacementActivities;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCandidatePrePlacementActivity(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchCandidatePrePlacementActivityByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveCandidatePrePlacementActivityByBatchId(vm.batchId);
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCandidatePrePlacementActivityByBatchId(vm.batchId);
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function retrieveBatches() {
            return CandidatePrePlacementActivityService.retrieveBatches().then(function (response) {
                vm.batches = response.data.Items;
                return vm.batches;
            });
        }
    }

})();
