(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CourseController', CourseController);

    CourseController.$inject = ['$window', 'CourseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CourseController($window, CourseService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.courses = [];
        vm.selectedCourses = [];
        vm.courseInstallments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCourse = editCourse;
        vm.canDeleteCourse = canDeleteCourse;
        vm.deleteCourse = deleteCourse;
        vm.searchCourse = searchCourse;
        vm.viewCourse = viewCourse;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.retrieveSectors = retrieveSectors;
        vm.retrieveCourseBySectorId = retrieveCourseBySectorId;
        vm.deleteEnquiryCourse = deleteEnquiryCourse;
        vm.deleteSubjectCourse = deleteSubjectCourse;
        vm.setDefaultSector = setDefaultSector;
        vm.type = "";

        initialise();

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");

        }

        function retrieveCourses() {
            return CourseService.retrieveCourses(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.courses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.courses;
                });
        }

        function retrieveCourseBySectorId() {
            var selectedSectorId = $('#Enquiry_SectorId').val();
                return CourseService.retrieveCourseBySectorId(vm.type).then(function(response) {
                    vm.courses = response.data;
                    $('#Enquiry_SectorId option').eq(selectedSectorId).prop('selected', true);
                    return vm.courses;
                });
        }

        function searchCourse(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CourseService.searchCourse(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.courses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.courses.length === 0 ? "No Records Found" : "";
                    return vm.courses;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchCourse(vm.searchKeyword);
            } else {
                return retrieveCourses();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCourses();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCourse(id) {
            $window.location.href = "/Course/Edit/" + id;
        }

        function canDeleteCourse(id) {
            vm.loadingActions = true;
            vm.CanDeleteCourse = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            CourseService.canDeleteCourse(id).then(function (response) { vm.CanDeleteCourse = response.data, vm.loadingActions = false });
        }

        function deleteCourse(id) {
            return CourseService.deleteCourse(id).then(function () { initialise(); });
        };

        function viewCourse(courseId) {
            $window.location.href = "/Course/Edit/" + courseId;
        }

        function retrieveSectors(schemeId) {
            return CourseService.retrieveSectors(schemeId).then(function () {
                vm.sectors = response.data;
            });
        };

        function deleteEnquiryCourse(enquiryId, $item) {
            return CourseService.deleteEnquiryCourse(enquiryId, $item.CourseId)
                .then(function () {
                });
        }

        function deleteSubjectCourse(subjectId, $item) {
            return CourseService.deleteSubjectCourse(subjectId, $item.CourseId)
                .then(function () {
                });
        }

        function setDefaultSector(id) {
            vm.type = id;
          //  $('#Enquiry_SectorId option').eq(id).prop('selected', true);
            if (vm.type != 0) {
                retrieveCourseBySectorId();
            }
        }
    }

})();
