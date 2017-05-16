(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentreSchemeController', CentreSchemeController);

    CentreSchemeController.$inject = ['$window', '$filter', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function CentreSchemeController($window, $filter, CentreService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */

        var vm = this;
        vm.initialise = initialise;
        vm.absencePolicyAbsenceTypes = [];
        vm.centreSchemes = [];
        vm.ddScheme = 0;
        vm.centreSchemeCount = 0;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.assigning = false;
        vm.changeScheme = changeScheme;
        vm.retrieveUnassignedCentreSchemes = retrieveUnassignedCentreSchemes;
        vm.retrieveCentreSchemes = retrieveCentreSchemes;
        vm.unassignCentreScheme = unassignCentreScheme;
        vm.isCentreSchemeAssignToCentre = isCentreSchemeAssignToCentre;
        vm.unassignCentreSchemeClass = unassignCentreSchemeClass;
        vm.assignCentreScheme = assignCentreScheme;
        vm.editAbsencePolicyEntitlement = editAbsencePolicyEntitlement;
        vm.openAbsencePolicyEntitlementForm = openAbsencePolicyEntitlementForm;

        function initialise(centreId) {
            vm.centreId = centreId;
            order("Name").then(function () {
                retrieveCentreSchemes();
            });
        }

        function retrieveCentreSchemes() {
            return CentreService.retrieveCentreSchemes(vm.centreId)
                .then(function (response) {
                    vm.centreSchemeError = false;
                    vm.centreSchemes = response.data.Items;
                    if (vm.centreSchemes.length > 0) {
                        vm.centreSchemeCount = vm.centreSchemes.length;

                    } else {
                        vm.centreSchemeError = true;
                        vm.centreSchemeCount = 0;


                    }
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                });
        }

        function retrieveUnassignedCentreSchemes() {
            return CentreService.retrieveUnassignedCentreSchemes(vm.centreId)
                .then(function (response) {
                    vm.ddSchemes = response.data;
                    vm.ddScheme = response.data[0];
                    vm.assigning = vm.ddSchemes.length == 0;
                    return vm.ddSchemes;
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

        function unassignCentreSchemeClass(centreScheme) {
            return centreScheme.CanUnassign ? '' : 'link-disabled';
        }

        function unassignCentreScheme(centreScheme) {
            if (centreScheme.CanUnassign) {
                return CentreService.unassignCentreScheme(vm.centreId, centreScheme.SchemeId)
                    .then(function () {
                        initialise(vm.centreId);
                    });
            }
        }

        function isCentreSchemeAssignToCentre(schemeId) {
            vm.schemeId = schemeId;
            $filter('filter')(vm.centreSchemes, { SchemeId: vm.schemeId })[0]["CanUnassign"] = true;
        }

        function assignCentreScheme() {
            vm.assigning = true;
            return CentreService.assignCentreScheme(vm.centreId, vm.ddScheme.SchemeId)
                .then(function () {
                    retrieveCentreSchemes();
                    retrieveUnassignedCentreSchemes();
                });
        }

        function changeScheme(ddScheme) {
            vm.ddScheme = ddScheme;
        }

        function pageChanged() {
            return retrieveUnassignedCentreSchemes();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUnassignedCentreSchemes();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }
})();
