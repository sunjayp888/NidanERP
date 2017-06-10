(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('OtherFeeController', OtherFeeController);

    OtherFeeController.$inject = ['$window', 'OtherFeeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function OtherFeeController($window, OtherFeeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.otherFees = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editOtherFee = editOtherFee;
        //vm.canDeleteOtherFee = canDeleteOtherFee;
        vm.deleteOtherFee = deleteOtherFee;
        vm.searchOtherFee = searchOtherFee;
        vm.viewOtherFee = viewOtherFee;
        //vm.addExpense = addExpense;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.retrieveOtherFeesByCashMemo = retrieveOtherFeesByCashMemo;
        vm.initialise = initialise;
        vm.cashMemo = "";

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveOtherFees() {
            return OtherFeeService.retrieveOtherFees(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.otherFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.otherFees;
                });
        }

        function retrieveOtherFeesByCashMemo(cashMemo) {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            vm.cashMemo = cashMemo == undefined ? $("#CashMemo").val() : cashMemo;
            return OtherFeeService.retrieveOtherFeesByCashMemo(vm.cashMemo, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.otherFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.otherFees;
                });
        }

        function searchOtherFee(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return OtherFeeService.searchOtherFee(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.otherFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.otherFees.length === 0 ? "No Records Found" : "";
                    return vm.otherFees;
                });
        }

        function pageChanged() {
            return retrieveOtherFees();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveOtherFees();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editOtherFee(id) {
            $window.location.href = "/OtherFee/Edit/" + id;
        }

        //function canDeleteOtherFee(id) {
        //    vm.loadingActions = true;
        //    vm.CanDeleteOtherFee = false;
        //    $('.dropdown-menu').slideUp('fast');
        //    $('.' + id).toggle();
        //    OtherFeeService.canDeleteOtherFee(id).then(function (response) { vm.CanDeleteOtherFee = response.data, vm.loadingActions = false });
        //}

        function viewOtherFee(otherFeeId) {
            $window.location.href = "/OtherFee/Edit/" + otherFeeId;
        }

        function deleteOtherFee(centreId, otherfeeId, cashMemo) {
            return OtherFeeService.deleteOtherFee(centreId, otherfeeId).then(function () {
                retrieveOtherFeesByCashMemo(cashMemo);
            });

        }

        //function addExpense() {
        //    return OtherFeeService.addExpense.then(function() {

        //    });
        //}
    }

})();
