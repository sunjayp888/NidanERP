(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FollowUpController', FollowUpController);

    FollowUpController.$inject = ['$window', 'FollowUpService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FollowUpController($window, FollowUpService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.followUps = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFollowUp = editFollowUp;
        vm.canDeleteFollowUp = canDeleteFollowUp;
        vm.deleteFollowUp = deleteFollowUp;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveFollowUps() {
            return FollowUpService.retrieveFollowUps(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.followUps;
                });
        }

        function pageChanged() {
            return retrieveFollowUps();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveFollowUps();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editFollowUp(id) {
            $window.location.href = "/FollowUp/Edit/" + id;
        }

        function canDeleteFollowUp(id) {
            vm.loadingActions = true;
            vm.CanDeleteFollowUp = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            FollowUpService.canDeleteFollowUp(id).then(function (response) { vm.CanDeleteFollowUp = response.data, vm.loadingActions = false });
        }
       
        function deleteFollowUp(id) {
            return FollowUpService.deleteFollowUp(id).then(function () { initialise(); });
        };

    }

})();
