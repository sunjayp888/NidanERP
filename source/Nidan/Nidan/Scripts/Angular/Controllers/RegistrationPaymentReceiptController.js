(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('RegistrationPaymentReceiptController', RegistrationPaymentReceiptController);

    RegistrationPaymentReceiptController.$inject = ['$window', 'RegistrationPaymentReceiptService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function RegistrationPaymentReceiptController($window, RegistrationPaymentReceiptService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.registrationPaymentReceipts = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editRegistrationPaymentReceipt = editRegistrationPaymentReceipt;
        vm.canDeleteRegistrationPaymentReceipt = canDeleteRegistrationPaymentReceipt;
        vm.deleteRegistrationPaymentReceipt = deleteRegistrationPaymentReceipt;
        vm.searchRegistrationPaymentReceipt = searchRegistrationPaymentReceipt;
        vm.viewRegistrationPaymentReceipt = viewRegistrationPaymentReceipt;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("RegistrationDate");
        }

        function retrieveRegistrationPaymentReceipts() {
            return RegistrationPaymentReceiptService.retrieveRegistrationPaymentReceipts(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.registrationPaymentReceipts = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.registrationPaymentReceipts;
                });
        }

        function searchRegistrationPaymentReceipt(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return RegistrationPaymentReceiptService.searchRegistrationPaymentReceipt(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.registrationPaymentReceipts = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.registrationPaymentReceipts.length === 0 ? "No Records Found" : "";
                  return vm.registrationPaymentReceipts;
              });
        }

        function pageChanged() {
            return retrieveRegistrationPaymentReceipts();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveRegistrationPaymentReceipts();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editRegistrationPaymentReceipt(id) {
            $window.location.href = "/RegistrationPaymentReceipt/Edit/" + id;
        }

        function canDeleteRegistrationPaymentReceipt(id) {
            vm.loadingActions = true;
            vm.CanDeleteRegistrationPaymentReceipt = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            RegistrationPaymentReceiptService.canDeleteRegistrationPaymentReceipt(id).then(function (response) { vm.CanDeleteRegistrationPaymentReceipt = response.data, vm.loadingActions = false });
        }

        function deleteRegistrationPaymentReceipt(id) {
            return RegistrationPaymentReceiptService.deleteRegistrationPaymentReceipt(id).then(function () { initialise(); });
        };

        function viewRegistrationPaymentReceipt(registrationPaymentReceiptId) {
            $window.location.href = "/RegistrationPaymentReceipt/Edit/" + registrationPaymentReceiptId;
        }

    }

})();
