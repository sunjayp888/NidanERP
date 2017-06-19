(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ReportController', ReportController);

    ReportController.$inject = ['$window', 'ReportService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ReportController($window, ReportService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.reports = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.viewReport = viewReport;
        vm.searchReport = searchReport;
        vm.retrieveReport = retrieveReport;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "ReportId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("ReportId");
        }

        function retrieveReports() {
            return ReportService.retrieveReports(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.reports;
                });
        }

        function searchReport(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ReportService.searchReport(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.reports = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                  return vm.reports;
              });
        }

        function retrieveReport() {
            return ReportService.retrieveReport(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.reports;
                });
        }

        function pageChanged() {
            return retrieveReports();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            retrieveReport();
            return retrieveReports();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
