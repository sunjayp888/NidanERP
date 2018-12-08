(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BankDepositeController', BankDepositeController);

    BankDepositeController.$inject = ['$window', 'BankDepositeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BankDepositeController($window, BankDepositeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.bankDeposites = [];
        vm.centres = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBankDeposite = editBankDeposite;
        vm.searchBankDeposite = searchBankDeposite;
        vm.viewBankDeposite = viewBankDeposite;
        vm.updateIsCleared = updateIsCleared;
        vm.updateIsBounced = updateIsBounced;
        vm.searchBankDepositeByDate = searchBankDepositeByDate;
        vm.searchBankDepositeByDateByCentreId = searchBankDepositeByDateByCentreId;
        vm.retrieveCentres = retrieveCentres;
        vm.searchBankDeposite = searchBankDeposite;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.paymentMode = [];
        vm.paymentModeId;
        vm.centreId;
        vm.retrievePaymentModes = retrievePaymentModes;
        initialise();

        function initialise() {
            vm.orderBy.property = "BankDepositeId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("BankDepositeId");
            retrievePaymentModes();
        }

        function retrieveBankDeposites() {
            retrieveCentres();
            return BankDepositeService.retrieveBankDeposites(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.bankDeposites = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.bankDeposites;
                });
        }

        function searchBankDeposite(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return BankDepositeService.searchBankDeposite(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.bankDeposites = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.bankDeposites.length === 0 ? "No Records Found" : "";
                    return vm.bankDeposites;
                });
        }

        function searchBankDepositeByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return BankDepositeService.searchBankDepositeByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.bankDeposites = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.bankDeposites.length === 0 ? "No Records Found" : "";
                    return vm.bankDeposites;
                });
        }

        function searchBankDepositeByDateByCentreId(centreId, fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.centreId = $('#dropCentre').val();
            vm.searchKeyword = null;
            return BankDepositeService.searchBankDepositeByDateByCentreId(vm.centreId, vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.bankDeposites = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.bankDeposites.length === 0 ? "No Records Found" : "";
                    return vm.bankDeposites;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchBankDeposite(vm.searchKeyword);
            }
            else if (vm.centreId && vm.fromDate && vm.toDate) {
                searchBankDepositeByDateByCentreId(vm.centreId, vm.fromDate, vm.toDate);
            }
            else if (vm.fromDate && vm.toDate) {
                searchBankDepositeByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveBankDeposites();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBankDeposites();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBankDeposite(id) {
            $window.location.href = "/BankDeposite/Edit/" + id;
        }

        function viewBankDeposite(bankDepositeId) {
            $window.location.href = "/BankDeposite/View/" + bankDepositeId;
        }

        function updateIsCleared(bankDepositeId) {
            return BankDepositeService.updateIsCleared(bankDepositeId).then(function () {
                pageChanged();
            });
        }

        function updateIsBounced(bankDepositeId) {
            return BankDepositeService.updateIsBounced(bankDepositeId).then(function () {
                pageChanged();
            });
        }


        function retrievePaymentModes() {
            return BankDepositeService.retrievePaymentModes()
                .then(function (response) {
                    vm.paymentModes = response.data;
                    return vm.paymentModes;
                });
        }

        function retrieveCentres() {
            return BankDepositeService.retrieveCentres().then(function (response) {
                vm.centres = response.data;
                return vm.centres;
            });
        }
    }

})();
