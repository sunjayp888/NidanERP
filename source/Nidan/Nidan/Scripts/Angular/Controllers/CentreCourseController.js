(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentreCourseController', CentreCourseController);

    CentreCourseController.$inject = ['$window', '$filter', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function CentreCourseController($window, $filter, CentreService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */

        var vm = this;
        vm.initialise = initialise;
        vm.absencePolicyAbsenceTypes = [];
        vm.centreCourses = [];
        vm.ddCourse = 0;
        vm.centreCourseCount = 0;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.assigning = false;
        vm.changeCourse = changeCourse;
        vm.retrieveUnassignedCentreCourses = retrieveUnassignedCentreCourses;
        vm.retrieveCentreCourses = retrieveCentreCourses;
        vm.unassignCentreCourse = unassignCentreCourse;
        vm.isCentreCourseAssignToCentre = isCentreCourseAssignToCentre;
        vm.unassignCentreCourseClass = unassignCentreCourseClass;
        vm.assignCentreCourse = assignCentreCourse;
        vm.editAbsencePolicyEntitlement = editAbsencePolicyEntitlement;
        vm.openAbsencePolicyEntitlementForm = openAbsencePolicyEntitlementForm;
        //vm.updateAbsencePolicyEntitlement = updateAbsencePolicyEntitlement;


        function initialise(centreId) {
            vm.centreId = centreId;
            order("Name").then(function () {
                retrieveCentreCourses();
            });
        }

        function retrieveCentreCourses() {
            return CentreService.retrieveCentreCourses(vm.centreId)
               .then(function (response) {
                   vm.centreCourseError = false;
                   vm.centreCourses = response.data.Items;
                   if (vm.centreCourses.length > 0) {
                       vm.centreCourseCount = vm.centreCourses.length;
                      
                   } else {
                       vm.centreCourseError = true;
                       vm.centreCourseCount = 0;
                     

                   }
                   vm.paging.totalPages = response.data.TotalPages;
                   vm.paging.totalResults = response.data.TotalResults;
               });
        }

        function retrieveUnassignedCentreCourses() {
            return CentreService.retrieveUnassignedCentreCourses(vm.centreId)
               .then(function (response) {
                   vm.ddCourses = response.data;
                   vm.ddCourse = response.data[0];
                   vm.assigning = vm.ddCourses.length == 0;
                   return vm.ddCourses;
               });
        }

        function editAbsencePolicyEntitlement(absencePolicyEntitlementId) {
            return CentreService.editAbsencePolicyEntitlement(vm.centreId, absencePolicyEntitlementId)
               .then(function (response) {
                   jQuery("#absencePolicyEntitlementModalBody").html(response.data);
                   $('#absencePolicyEntitlementErrorSummary').hide();
                   $("#absencePolicyEntitlementModal").modal('show');
               });
        }

        function openAbsencePolicyEntitlementForm(absencePolicyEntitlementId, absenceType) {
            vm.absenceType = absenceType;
            editAbsencePolicyEntitlement(absencePolicyEntitlementId);
        }

        function unassignCentreCourseClass(centreCourse) {
            return centreCourse.CanUnassign ? '' : 'link-disabled';
        }

        function unassignCentreCourse(centreCourse) {
            if (centreCourse.CanUnassign) {
                return CentreService.unassignCentreCourse(vm.centreId, centreCourse.CourseId)
                    .then(function () {
                        initialise(vm.centreId);
                    });
            }
        }

        function isCentreCourseAssignToCentre(courseId) {
            vm.courseId = courseId;
            $filter('filter')(vm.centreCourses, { CourseId: vm.courseId })[0]["CanUnassign"] = true;
        }

        function assignCentreCourse() {
            vm.assigning = true;
            return CentreService.assignCentreCourse(vm.centreId, vm.ddCourse.CourseId)
              .then(function () {
                  retrieveCentreCourses();
                  retrieveUnassignedCentreCourses();
              });
        }

        function changeCourse(ddCourse) {
            vm.ddCourse = ddCourse;
        }

        function pageChanged() {
            return retrieveUnassignedCentreCourses();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUnassignedCentreCourses();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }
})();
