(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PartnerController', PartnerController);

    PartnerController.$inject = ['$window', 'PartnerService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function PartnerController($window, PartnerService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.partners = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "PartnerId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrievePartners();
        }

        function retrievePartners() {
            return PartnerService.retrievePartners(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.partners = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.partners;
                });
        }

        function pageChanged() {
            return retrievePartners();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrievePartners();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
