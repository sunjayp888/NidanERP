(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AbsenceTypeController', AbsenceTypeController);

    AbsenceTypeController.$inject = ['$window', 'AbsenceTypeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AbsenceTypeController($window, AbsenceTypeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.absenceTypes = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAbsenceType = editAbsenceType;
        vm.canDeleteAbsenceType = canDeleteAbsenceType;
        vm.deleteAbsenceType = deleteAbsenceType;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveAbsenceTypes() {
            return AbsenceTypeService.retrieveAbsenceTypes(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.absenceTypes = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.absenceTypes;
                });
        }

        function pageChanged() {
            return retrieveAbsenceTypes();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAbsenceTypes();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAbsenceType(id) {
            $window.location.href = "/AbsenceType/Edit/" + id;
        }

        function canDeleteAbsenceType(id) {
            vm.loadingActions = true;
            vm.CanDeleteAbsenceType = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            AbsenceTypeService.canDeleteAbsenceType(id).then(function (response) { vm.CanDeleteAbsenceType = response.data, vm.loadingActions = false });
        }
       
        function deleteAbsenceType(id) {
            return AbsenceTypeService.deleteAbsenceType(id).then(function () { initialise(); });
        };

    }

})();
