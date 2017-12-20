(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CompanyFollowUpHistoryController', CompanyFollowUpHistoryController);

    CompanyFollowUpHistoryController.$inject = ['$window', 'CompanyFollowUpHistoryService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CompanyFollowUpHistoryController($window, CompanyFollowUpHistoryService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.companyFollowUpHistories = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CompanyFollowUpHistoryId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("CompanyFollowUpHistoryId");
        }

        function retrieveCompanyFollowUpHistories() {
            return CompanyFollowUpHistoryService.retrieveCompanyFollowUpHistories(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companyFollowUpHistories = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.companyFollowUpHistories;
                });
        }

        function pageChanged() {
            return retrieveCompanyFollowUpHistories();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCompanyFollowUpHistories();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
