(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CourseFeeBreakUpController', CourseFeeBreakUpController);

    CourseFeeBreakUpController.$inject = ['$window', 'CourseFeeBreakUpService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CourseFeeBreakUpController($window, CourseFeeBreakUpService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.courseFeeBreakUps = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCourseFeeBreakUp = editCourseFeeBreakUp;
        vm.searchCourseFeeBreakUp = searchCourseFeeBreakUp;
        vm.viewCourseFeeBreakUp = viewCourseFeeBreakUp;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveCourseFeeBreakUps() {
            return CourseFeeBreakUpService.retrieveCourseFeeBreakUps(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.courseFeeBreakUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.courseFeeBreakUps;
                });
        }

        function searchCourseFeeBreakUp(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CourseFeeBreakUpService.searchCourseFeeBreakUp(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.courseFeeBreakUps = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.courseFeeBreakUps.length === 0 ? "No Records Found" : "";
                  return vm.courseFeeBreakUps;
              });
        }

        function pageChanged() {
            return retrieveCourseFeeBreakUps();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCourseFeeBreakUps();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCourseFeeBreakUp(id) {
            $window.location.href = "/CourseFeeBreakUp/Edit/" + id;
        }

        function viewCourseFeeBreakUp(courseFeeBreakUpId) {
            $window.location.href = "/CourseFeeBreakUp/Edit/" + courseFeeBreakUpId;
        }
    }

})();
