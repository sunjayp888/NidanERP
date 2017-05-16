(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentreSectorController', CentreSectorController);

    CentreSectorController.$inject = ['$window', '$filter', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function CentreSectorController($window, $filter, CentreService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */

        var vm = this;
        vm.initialise = initialise;
        vm.absencePolicyAbsenceTypes = [];
        vm.centreSectors = [];
        vm.ddSector = 0;
        vm.centreSectorCount = 0;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.assigning = false;
        vm.changeSector = changeSector;
        vm.retrieveUnassignedCentreSectors = retrieveUnassignedCentreSectors;
        vm.retrieveCentreSectors = retrieveCentreSectors;
        vm.unassignCentreSector = unassignCentreSector;
        vm.isCentreSectorAssignToCentre = isCentreSectorAssignToCentre;
        vm.unassignCentreSectorClass = unassignCentreSectorClass;
        vm.assignCentreSector = assignCentreSector;
        vm.editAbsencePolicyEntitlement = editAbsencePolicyEntitlement;
        vm.openAbsencePolicyEntitlementForm = openAbsencePolicyEntitlementForm;

        function initialise(centreId) {
            vm.centreId = centreId;
            order("Name").then(function () {
                retrieveCentreSectors();
            });
        }

        function retrieveCentreSectors() {
            return CentreService.retrieveCentreSectors(vm.centreId)
                .then(function (response) {
                    vm.centreSectorError = false;
                    vm.centreSectors = response.data.Items;
                    if (vm.centreSectors.length > 0) {
                        vm.centreSectorCount = vm.centreSectors.length;

                    } else {
                        vm.centreSectorError = true;
                        vm.centreSectorCount = 0;


                    }
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                });
        }

        function retrieveUnassignedCentreSectors() {
            return CentreService.retrieveUnassignedCentreSectors(vm.centreId)
                .then(function (response) {
                    vm.ddSectors = response.data;
                    vm.ddSector = response.data[0];
                    vm.assigning = vm.ddSectors.length == 0;
                    return vm.ddSectors;
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

        function unassignCentreSectorClass(centreSector) {
            return centreSector.CanUnassign ? '' : 'link-disabled';
        }

        function unassignCentreSector(centreSector) {
            if (centreSector.CanUnassign) {
                return CentreService.unassignCentreSector(vm.centreId, centreSector.SectorId)
                    .then(function () {
                        initialise(vm.centreId);
                    });
            }
        }

        function isCentreSectorAssignToCentre(sectorId) {
            vm.sectorId = sectorId;
            $filter('filter')(vm.centreSectors, { SectorId: vm.sectorId })[0]["CanUnassign"] = true;
        }

        function assignCentreSector() {
            vm.assigning = true;
            return CentreService.assignCentreSector(vm.centreId, vm.ddSector.SectorId)
                .then(function () {
                    retrieveCentreSectors();
                    retrieveUnassignedCentreSectors();
                });
        }

        function changeSector(ddSector) {
            vm.ddSector = ddSector;
        }

        function pageChanged() {
            return retrieveUnassignedCentreSectors();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUnassignedCentreSectors();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }
})();
