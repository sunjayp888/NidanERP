(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CounsellingController', CounsellingController);

    CounsellingController.$inject = ['$window', 'CounsellingService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CounsellingController($window, CounsellingService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.counsellings = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCounselling = editCounselling;
        initialise();

        function initialise() {
            order("CounselledBy");
        }

        function retrieveCounsellings() {
            return CounsellingService.retrieveCounsellings(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.counsellings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.counsellings;
                });
        }

        function pageChanged() {
            return retrieveCounsellings();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCounsellings();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCounselling(id) {
            $window.location.href = "/Counselling/Edit/" + id;
        }
    }

})();
