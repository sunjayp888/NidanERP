(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentreCourseInstallmentController', CentreCourseInstallmentController);

    CentreCourseInstallmentController.$inject = ['$window', '$filter', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function CentreCourseInstallmentController($window, $filter, CentreService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */

        var vm = this;
        vm.initialise = initialise;
        vm.absencePolicyAbsenceTypes = [];
        vm.centreCourseInstallments = [];
        vm.ddCourseInstallment = 0;
        vm.centreCourseInstallmentCount = 0;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.assigning = false;
        vm.changeCourseInstallment = changeCourseInstallment;
        vm.retrieveUnassignedCentreCourseInstallments = retrieveUnassignedCentreCourseInstallments;
        vm.retrieveCentreCourseInstallments = retrieveCentreCourseInstallments;
        vm.unassignCentreCourseInstallment = unassignCentreCourseInstallment;
        vm.isCentreCourseInstallmentAssignToCentre = isCentreCourseInstallmentAssignToCentre;
        vm.unassignCentreCourseInstallmentClass = unassignCentreCourseInstallmentClass;
        vm.assignCentreCourseInstallment = assignCentreCourseInstallment;
        vm.editAbsencePolicyEntitlement = editAbsencePolicyEntitlement;
        vm.openAbsencePolicyEntitlementForm = openAbsencePolicyEntitlementForm;
        //vm.updateAbsencePolicyEntitlement = updateAbsencePolicyEntitlement;


        function initialise(centreId) {
            vm.centreId = centreId;
            order("Name").then(function () {
                retrieveCentreCourseInstallments();
            });
        }

        function retrieveCentreCourseInstallments() {
            return CentreService.retrieveCentreCourseInstallments(vm.centreId)
                .then(function (response) {
                    vm.centreCourseInstallmentError = false;
                    vm.centreCourseInstallments = response.data.Items;
                    if (vm.centreCourseInstallments.length > 0) {
                        vm.centreCourseInstallmentCount = vm.centreCourseInstallments.length;

                    } else {
                        vm.centreCourseInstallmentError = true;
                        vm.centreCourseInstallmentCount = 0;


                    }
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                });
        }

        function retrieveUnassignedCentreCourseInstallments() {
            return CentreService.retrieveUnassignedCentreCourseInstallments(vm.centreId)
                .then(function (response) {
                    vm.ddCourseInstallments = response.data;
                    vm.ddCourseInstallment = response.data[0];
                    vm.assigning = vm.ddCourseInstallments.length == 0;
                    return vm.ddCourseInstallments;
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

        function unassignCentreCourseInstallmentClass(centreCourseInstallment) {
            return centreCourseInstallment.CanUnassign ? '' : 'link-disabled';
        }

        function unassignCentreCourseInstallment(centreCourseInstallment) {
            if (centreCourseInstallment.CanUnassign) {
                return CentreService.unassignCentreCourseInstallment(vm.centreId, centreCourseInstallment.CourseInstallmentId)
                    .then(function () {
                        initialise(vm.centreId);
                    });
            }
        }

        function isCentreCourseInstallmentAssignToCentre(courseInstallmentId) {
            vm.courseInstallmentId = courseInstallmentId;
            $filter('filter')(vm.centreCourseInstallments, { CourseInstallmentId: vm.courseInstallmentId })[0]["CanUnassign"] = true;
        }

        function assignCentreCourseInstallment() {
            vm.assigning = true;
            return CentreService.assignCentreCourseInstallment(vm.centreId, vm.ddCourseInstallment.CourseInstallmentId)
                .then(function () {
                    retrieveCentreCourseInstallments();
                    retrieveUnassignedCentreCourseInstallments();
                });
        }

        function changeCourseInstallment(ddCourseInstallment) {
            vm.ddCourseInstallment = ddCourseInstallment;
        }

        function pageChanged() {
            return retrieveUnassignedCentreCourseInstallments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUnassignedCentreCourseInstallments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }
})();
