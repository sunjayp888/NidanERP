(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidateFeeController', CandidateFeeController);

    CandidateFeeController.$inject = ['$window', 'CandidateFeeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidateFeeController($window, CandidateFeeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidateFees = [];
        vm.paymentModes = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCandidateFee = editCandidateFee;
        // vm.viewCandidateFee = viewCandidateFee;
        vm.searchCandidateFee = searchCandidateFee;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.removeError = removeError;
        vm.candidateInstallmentId;
        vm.paymentModeId;
        vm.saveFee = saveFee;
        vm.openCandidateFeeModalPopUp = openCandidateFeeModalPopUp;
        vm.retrievePaymentModes = retrievePaymentModes;
        vm.initialise = initialise;

        function initialise(candidateInstallmentId) {
            vm.candidateInstallmentId = candidateInstallmentId;
            vm.orderBy.property = "InstallmentDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("InstallmentDate");
            retrievePaymentModes();
        }


        function retrieveCandidateFees() {
            return CandidateFeeService.retrieveCandidateFees(vm.candidateInstallmentId)
                .then(function (response) {
                    vm.candidateFees = response.data;
                    return vm.candidateFees;
                });
        }

        function searchCandidateFee(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CandidateFeeService.searchCandidateFee(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.candidateFees = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.candidateFees.length === 0 ? "No Records Found" : "";
                  return vm.candidateFees;
              });
        }

        function saveFee() {
            var candidateFee = {
                PaidAmount: $("#txtAmount").val(),
                PaymentModeId: vm.paymentModeId,
                ChequeNumber: $("#txtChequeNumber").val(),
                ChequeDate: $("#txtChequeDate").val(),
                BankName: $("#txtAmount").val(),
                FollowUpDate: $("#txtBankName").val()
            }

            return CandidateFeeService.saveFee(candidateFee)
               .then(function (response) {
                   retrieveCandidateFees();
               });
        }

        function retrievePaymentModes() {
            return CandidateFeeService.retrievePaymentModes()
               .then(function (response) {
                   vm.paymentModes = response.data;
                   return vm.paymentModes;
               });
        }

        function pageChanged() {
            return retrieveCandidateFees();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCandidateFees();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCandidateFee(id) {
            $window.location.href = "/CandidateFee/Edit/" + id;
        }

        function removeError() {
            vm.Errors.length = 0;
            vm.documentFile = null;
        }

        function openCandidateFeeModalPopUp(candidateFeeId) {
            return CandidateFeeService.retrieveCandidateFee(candidateFeeId)
              .then(function (response) {
                  $("#txtPaidAmount").val(response.data.PaidAmount);
              });
        }

    }

})();
