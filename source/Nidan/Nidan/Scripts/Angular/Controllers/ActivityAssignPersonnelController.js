(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ActivityAssignPersonnelController', ActivityAssignPersonnelController);

    ActivityAssignPersonnelController.$inject = ['$window', '$filter', 'ActivityAssigneeGroupService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function ActivityAssignPersonnelController($window, $filter, ActivityAssigneeGroupService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */

        var vm = this;
        vm.initialise = initialise;
        vm.absencePolicyAbsenceTypes = [];
        vm.activityAssignPersonnels = [];
        vm.ddPersonnel = 0;
        vm.activityAssignPersonnelCount = 0;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.assigning = false;
        vm.changePersonnel = changePersonnel;
        vm.retrieveUnassignedActivityAssignPersonnels = retrieveUnassignedActivityAssignPersonnels;
        vm.retrieveActivityAssignPersonnels = retrieveActivityAssignPersonnels;
        vm.unassignActivityAssignPersonnel = unassignActivityAssignPersonnel;
        vm.isActivityAssignPersonnelAssignToActivityAssigneeGroup = isActivityAssignPersonnelAssignToActivityAssigneeGroup;
        vm.unassignActivityAssignPersonnelClass = unassignActivityAssignPersonnelClass;
        vm.assignActivityAssignPersonnel = assignActivityAssignPersonnel;
        vm.editAbsencePolicyEntitlement = editAbsencePolicyEntitlement;
        vm.openAbsencePolicyEntitlementForm = openAbsencePolicyEntitlementForm;
        //vm.updateAbsencePolicyEntitlement = updateAbsencePolicyEntitlement;


        function initialise(centreId, activityAssigneeGroupId) {
            vm.centreId = centreId;
            vm.activityAssigneeGroupId = activityAssigneeGroupId;
            order("Name").then(function () {
                retrieveActivityAssignPersonnels();
            });
        }

        function retrieveActivityAssignPersonnels() {
            return ActivityAssigneeGroupService.retrieveActivityAssignPersonnels(vm.centreId, vm.activityAssigneeGroupId)
                .then(function (response) {
                    vm.activityAssignPersonnelError = false;
                    vm.activityAssignPersonnels = response.data.Items;
                    if (vm.activityAssignPersonnels.length > 0) {
                        vm.activityAssignPersonnelCount = vm.activityAssignPersonnels.length;

                    } else {
                        vm.activityAssignPersonnelError = true;
                        vm.activityAssignPersonnelCount = 0;


                    }
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                });
        }

        function retrieveUnassignedActivityAssignPersonnels() {
            return ActivityAssigneeGroupService.retrieveUnassignedActivityAssignPersonnels(vm.centreId, vm.activityAssigneeGroupId)
                .then(function (response) {
                    vm.ddPersonnels = response.data;
                    vm.ddPersonnel = response.data[0];
                    vm.assigning = vm.ddPersonnels.length == 0;
                    return vm.ddPersonnels;
                });
        }

        function editAbsencePolicyEntitlement(absencePolicyEntitlementId) {
            return ActivityAssigneeGroupService.editAbsencePolicyEntitlement(vm.centreId, vm.activityAssigneeGroupId, absencePolicyEntitlementId)
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

        function unassignActivityAssignPersonnelClass(activityAssignPersonnel) {
            return activityAssignPersonnel.CanUnassign ? '' : 'link-disabled';
        }

        function unassignActivityAssignPersonnel(activityAssignPersonnel) {
            if (activityAssignPersonnel.CanUnassign) {
                return ActivityAssigneeGroupService.unassignActivityAssignPersonnel(vm.centreId, vm.activityAssigneeGroupId, activityAssignPersonnel.PersonnelId)
                    .then(function () {
                        initialise(vm.centreId, vm.activityAssigneeGroupId);
                    });
            }
        }

        function isActivityAssignPersonnelAssignToActivityAssigneeGroup(personnelId) {
            vm.personnelId = personnelId;
            $filter('filter')(vm.activityAssignPersonnels, { PersonnelId: vm.personnelId })[0]["CanUnassign"] = true;
        }

        function assignActivityAssignPersonnel() {
            vm.assigning = true;
            return ActivityAssigneeGroupService.assignActivityAssignPersonnel(vm.centreId, vm.activityAssigneeGroupId, vm.ddPersonnel.PersonnelId)
                .then(function () {
                    retrieveActivityAssignPersonnels();
                    retrieveUnassignedActivityAssignPersonnels();
                });
        }

        function changePersonnel(ddPersonnel) {
            vm.ddPersonnel = ddPersonnel;
        }

        function pageChanged() {
            return retrieveUnassignedActivityAssignPersonnels();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUnassignedActivityAssignPersonnels();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }
})();
