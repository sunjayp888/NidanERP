(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CourseInstallmentController', CourseInstallmentController);

    CourseInstallmentController.$inject = ['$window', 'CourseInstallmentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CourseInstallmentController($window, CourseInstallmentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.courseInstallments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCourseInstallment = editCourseInstallment;
        vm.searchCourseInstallment = searchCourseInstallment;
        vm.viewCourseInstallment = viewCourseInstallment;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        initialise();

        function initialise() {
            order("CourseInstallmentId");
        }

        function retrieveCourseInstallments() {
            return CourseInstallmentService.retrieveCourseInstallments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.courseInstallments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.courseInstallments;
                });
        }

        function searchCourseInstallment(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CourseInstallmentService.searchCourseInstallment(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.courseInstallments = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.courseInstallments.length === 0 ? "No Records Found" : "";
                  return vm.courseInstallments;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCourseInstallment(vm.searchKeyword);
            } else {
                return retrieveCourseInstallments();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCourseInstallments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCourseInstallment(id) {
            $window.location.href = "/CourseInstallment/Edit/" + id;
        }

        function viewCourseInstallment(courseInstallmentId) {
            $window.location.href = "/CourseInstallment/View/" + courseInstallmentId;
        }

        function retrieveCourses(sectorId) {
            return CourseInstallmentService.retrieveCourses(sectorId).then(function () {
                vm.courses = response.data;
            });
        };

    }

})();
