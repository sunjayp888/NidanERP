(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentrePettyCashController', CentrePettyCashController);

    CentrePettyCashController.$inject = ['$window', 'CentrePettyCashService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CentrePettyCashController($window, CentrePettyCashService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.centrePettyCashs = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCentrePettyCash = editCentrePettyCash;
        vm.searchCentrePettyCash = searchCentrePettyCash;
        vm.viewCentrePettyCash = viewCentrePettyCash;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveCentrePettyCashs() {
            return CentrePettyCashService.retrieveCentrePettyCashs(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.centrePettyCashs = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.centrePettyCashs;
                });
        }

        function searchCentrePettyCash(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CentrePettyCashService.searchCentrePettyCash(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.centrePettyCashs = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.centrePettyCashs.length === 0 ? "No Records Found" : "";
                    return vm.centrePettyCashs;
                });
        }

        function pageChanged() {
            return retrieveCentrePettyCashs();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCentrePettyCashs();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCentrePettyCash(id) {
            $window.location.href = "/CentrePettyCash/Edit/" + id;
        }

        function viewCentrePettyCash(centrePettyCashId) {
            $window.location.href = "/CentrePettyCash/Edit/" + centrePettyCashId;
        }

    }

})();
