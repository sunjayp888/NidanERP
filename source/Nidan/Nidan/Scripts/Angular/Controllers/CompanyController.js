(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CompanyController', CompanyController);

    CompanyController.$inject = ['$window', 'CompanyService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CompanyController($window, CompanyService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.companies = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCompany = editCompany;
        vm.searchCompany = searchCompany;
        vm.searchCompanyByDate = searchCompanyByDate;
        vm.retrieveCompanies = retrieveCompanies;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("CreatedDate");
        }

        function retrieveCompanies() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return CompanyService.retrieveCompanies(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companies = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.companies;
                });
        }

        function searchCompany(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return CompanyService.searchCompany(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companies = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.companies.length === 0 ? "No Records Found" : "";
                    return vm.companies;
                });
        }

        function searchCompanyByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return CompanyService.searchCompanyByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.companies = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.companies.length === 0 ? "No Records Found" : "";
                    return vm.companies;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCompany(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchCompanyByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveCompanies();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCompanies();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCompany(id) {
            $window.location.href = "/Company/Edit/" + id;
        }

    }

})();
