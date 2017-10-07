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
        vm.viewCounselling = viewCounselling;
        vm.searchCounselling = searchCounselling;
        vm.searchCounsellingByDate = searchCounsellingByDate;
        vm.searchKeyword = "";
        vm.fromDate = "";
        vm.toDate = "";
        vm.searchMessage = "";
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        initialise();

        function initialise() {
            vm.orderBy.property = "ConversionProspect";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("ConversionProspect");
        }

        function retrieveCounsellings() {
            vm.orderBy.property = "ConversionProspect";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            return CounsellingService.retrieveCounsellings(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.counsellings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.counsellings;
                });
        }

        function searchCounselling(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CounsellingService.searchCounselling(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.counsellings = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.counsellings.length === 0 ? "No Records Found" : "";
                  return vm.counsellings;
              });
        }

        function searchCounsellingByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return CounsellingService.searchCounsellingByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.counsellings = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.counsellings.length === 0 ? "No Records Found" : "";
                  return vm.counsellings;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCounselling(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchCounsellingByDate(vm.fromDate, vm.toDate);
            } else {
                return retrieveCounsellings();
            }
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

        function viewCounselling(counsellingId) {
            $window.location.href = "/Counselling/View/" + counsellingId;
        }

        function retrieveCourses(sectorId) {
            return CounsellingService.retrieveCourses(sectorId).then(function () {
                vm.courses = response.data;
            });
        };
    }

})();
