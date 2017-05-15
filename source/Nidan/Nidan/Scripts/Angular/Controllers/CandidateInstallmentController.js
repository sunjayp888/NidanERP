(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidateInstallmentController', CandidateInstallmentController);

    CandidateInstallmentController.$inject = ['$window', 'CandidateInstallmentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidateInstallmentController($window, CandidateInstallmentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidateInstallments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCandidateInstallment = editCandidateInstallment;
        vm.viewCandidateInstallment = viewCandidateInstallment;
        vm.searchCandidateInstallment = searchCandidateInstallment;
        vm.retrieveCandidateFeeList = retrieveCandidateFeeList;
        vm.viewCandidateFee = viewCandidateFee;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CandidateInstallmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("CandidateInstallmentId");
        }

        function retrieveCandidateInstallments() {
            return CandidateInstallmentService.retrieveCandidateInstallments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateInstallments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateInstallments;
                });
        }

        function searchCandidateInstallment(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CandidateInstallmentService.searchCandidateInstallment(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.candidateInstallments = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.candidateInstallments.length === 0 ? "No Records Found" : "";
                  return vm.candidateInstallments;
              });
        }

        function retrieveCandidateFeeList() {
            return CandidateInstallmentService.retrieveCandidateFeeList(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateFees;
                });
        }

        function pageChanged() {
            return retrieveCandidateInstallments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            retrieveCandidateFeeList();
            return retrieveCandidateInstallments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCandidateInstallment(id) {
            $window.location.href = "/CandidateInstallment/Edit/" + id;
        }

        function viewCandidateFee(candidateInstallmentId) {
            $window.location.href = "/CandidateFee/Detail/" + candidateInstallmentId;
        }


        function viewCandidateInstallment(candidateInstallmentId) {
            $window.location.href = "/CandidateInstallment/Edit/" + candidateInstallmentId;
        }

    }

})();
