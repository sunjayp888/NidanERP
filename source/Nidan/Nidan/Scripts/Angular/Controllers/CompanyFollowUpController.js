(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CompanyFollowUpController', CompanyFollowUpController);

    CompanyFollowUpController.$inject = ['$window', 'CompanyFollowUpService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CompanyFollowUpController($window, CompanyFollowUpService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.companyFollowUps = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCompanyFollowUp = editCompanyFollowUp;
        vm.searchCompanyFollowUp = searchCompanyFollowUp;
        vm.searchCompanyFollowUpByDate = searchCompanyFollowUpByDate;
        vm.retrieveCompanyFollowUps = retrieveCompanyFollowUps;
        vm.viewCompanyFollowUp = viewCompanyFollowUp;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "FollowUpDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("FollowUpDate");
        }

        function retrieveCompanyFollowUps() {
            return CompanyFollowUpService.retrieveCompanyFollowUps(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companyFollowUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.companyFollowUps;
                });
        }

        function pageChanged() {
            //var path = window.location.pathname.split('/');
            if (vm.searchKeyword) {
                searchFollowUp(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchFollowUpByDate(vm.fromDate, vm.toDate);
            } else {
                retrieveCompanyFollowUps();
            }

        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCompanyFollowUps();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCompanyFollowUp(id) {
            $window.location.href = "/FollowUp/Edit/" + id;
        }

        function searchCompanyFollowUpByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return CompanyFollowUpService.searchCompanyFollowUpByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companyFollowUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.companyFollowUps.length === 0 ? "No Records Found" : "";
                    return vm.companyFollowUps;
                });
        }

        function searchCompanyFollowUp(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return CompanyFollowUpService.searchCompanyFollowUp(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companyFollowUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.companyFollowUps.length === 0 ? "No Records Found" : "";
                    return vm.companyFollowUps;
                });
        }

        function viewCompanyFollowUp(followUpId) {
            $window.location.href = "/CompanyFollowUp/Edit/" + followUpId;
        }
    }

})();
