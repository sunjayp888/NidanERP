(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ActivityAssigneeGroupController', ActivityAssigneeGroupController);

    ActivityAssigneeGroupController.$inject = ['$window', 'ActivityAssigneeGroupService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ActivityAssigneeGroupController($window, ActivityAssigneeGroupService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.activityAssigneeGroups = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "ActivityAssigneeGroupId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveActivityAssigneeGroups();
        }

        function retrieveActivityAssigneeGroups() {
            return ActivityAssigneeGroupService.retrieveActivityAssigneeGroups(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activityAssigneeGroups = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.activityAssigneeGroups;
                });
        }

        function pageChanged() {
            return retrieveActivityAssigneeGroups();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveActivityAssigneeGroups();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
