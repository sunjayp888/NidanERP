(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FollowUpHistoryController', FollowUpHistoryController);

    FollowUpHistoryController.$inject = ['$window', 'FollowUpHistoryService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FollowUpHistoryController($window, FollowUpHistoryService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.followUpHistories = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchFollowUpHistory = searchFollowUpHistory;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "FollowUpHistoryId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("FollowUpHistoryId");
        }

        function retrieveFollowUpHistories() {
            return FollowUpHistoryService.retrieveFollowUpHistories(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUpHistories = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.followUpHistories;
                });
        }

        function searchFollowUpHistory(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return FollowUpHistoryService.searchFollowUpHistory(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUpHistories = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.followUpHistories.length === 0 ? "No Records Found" : "";
                    return vm.followUpHistories;
                });
        }

        function pageChanged() {
            return retrieveFollowUpHistories();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveFollowUpHistories();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
