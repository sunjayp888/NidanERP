(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AreaOfInterestController', AreaOfInterestController);

    AreaOfInterestController.$inject = ['$window', 'AreaOfInterestService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AreaOfInterestController($window, AreaOfInterestService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.areaOfInterests = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAreaOfInterest = editAreaOfInterest;
        vm.canDeleteAreaOfInterest = canDeleteAreaOfInterest;
        vm.deleteAreaOfInterest = deleteAreaOfInterest;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveAreaOfInterests() {
            return AreaOfInterestService.retrieveAreaOfInterests(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.areaOfInterests = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.areaOfInterests;
                });
        }

        function pageChanged() {
            return retrieveAreaOfInterests();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAreaOfInterests();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAreaOfInterest(id) {
            $window.location.href = "/AreaOfInterest/Edit/" + id;
        }

        function canDeleteAreaOfInterest(id) {
            vm.loadingActions = true;
            vm.CanDeleteAreaOfInterest = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            AreaOfInterestService.canDeleteAreaOfInterest(id).then(function (response) { vm.CanDeleteAreaOfInterest = response.data, vm.loadingActions = false });
        }

        function deleteAreaOfInterest(id) {
            return AreaOfInterestService.deleteAreaOfInterest(id).then(function () { initialise(); });
        };

    }

})();
