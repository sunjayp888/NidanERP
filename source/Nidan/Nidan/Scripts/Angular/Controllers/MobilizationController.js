(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('MobilizationController', MobilizationController);

    MobilizationController.$inject = ['$window', 'MobilizationService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function MobilizationController($window, MobilizationService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobilizations = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editMobilization = editMobilization;
        vm.canDeleteMobilization = canDeleteMobilization;
        vm.deleteMobilization = deleteMobilization;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveMobilizations() {
            return MobilizationService.retrieveMobilizations(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobilizations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.mobilizations;
                });
        }

        function pageChanged() {
            return retrieveMobilizations();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveMobilizations();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editMobilization(id) {
            $window.location.href = "/Mobilization/Edit/" + id;
        }

        function canDeleteMobilization(id) {
            vm.loadingActions = true;
            vm.CanDeleteMobilization = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            MobilizationService.canDeleteMobilization(id).then(function (response) { vm.CanDeleteMobilization = response.data, vm.loadingActions = false });
        }

        function deleteMobilization(id) {
            return MobilizationService.deleteMobilization(id).then(function () { initialise(); });
        };

    }

})();
