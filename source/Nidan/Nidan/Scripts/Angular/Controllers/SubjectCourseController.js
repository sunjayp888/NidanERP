(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('SubjectCourseController', SubjectCourseController);

    SubjectCourseController.$inject = ['$window', 'SubjectCourseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function SubjectCourseController($window, SubjectCourseService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.courses = [];
        vm.selectedCourses = [];
        //vm.courseInstallments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        //vm.editCourse = editCourse;
        //vm.canDeleteCourse = canDeleteCourse;
        //vm.deleteCourse = deleteCourse;
        //vm.searchCourse = searchCourse;
        //vm.viewCourse = viewCourse;
        //vm.searchKeyword = "";
        //vm.searchMessage = "";
        //vm.retrieveSectors = retrieveSectors;
        //vm.retrieveCourseBySectorId = retrieveCourseBySectorId;
        //vm.deleteEnquiryCourse = deleteEnquiryCourse;
        vm.deleteSubjectCourse = deleteSubjectCourse;
        vm.type = "";

        initialise();

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveCourses() {
            return SubjectCourseService.retrieveCourses(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.courses = response.data;
                    //vm.paging.totalPages = response.data.TotalPages;
                    //vm.paging.totalResults = response.data.TotalResults;
                    return vm.courses;
                });
        }

        
        function pageChanged() {
            return retrieveCourses();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCourses();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function deleteSubjectCourse(subjectId, $item) {
            return SubjectCourseService.deleteSubjectCourse(subjectId, $item.CourseId)
                .then(function () {
                });
        }
    }

})();
