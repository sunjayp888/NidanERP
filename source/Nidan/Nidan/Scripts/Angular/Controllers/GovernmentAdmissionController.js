(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('GovernmentAdmissionController', GovernmentAdmissionController);

    GovernmentAdmissionController.$inject = ['$window', 'GovernmentAdmissionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function GovernmentAdmissionController($window, GovernmentAdmissionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.governmentAdmissions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editGovernmentAdmission = editGovernmentAdmission;
        vm.canDeleteGovernmentAdmission = canDeleteGovernmentAdmission;
        //vm.deleteGovernmentAdmission = deleteGovernmentAdmission;
        vm.searchGovernmentAdmission = searchGovernmentAdmission;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("FirstName");
        }

        function retrieveGovernmentAdmissions() {
            return GovernmentAdmissionService.retrieveGovernmentAdmissions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.governmentAdmissions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.governmentAdmissions;
                });
        }

        function searchGovernmentAdmission(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return GovernmentAdmissionService.searchGovernmentAdmission(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.governmentAdmissions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.governmentAdmissions.length === 0 ? "No Records Found" : "";
                  return vm.governmentAdmissions;
              });
        }

        function pageChanged() {
            return retrieveGovernmentAdmissions();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveGovernmentAdmissions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editGovernmentAdmission(id) {
            $window.location.href = "/GovernmentAdmission/Edit/" + id;
        }

        function canDeleteGovernmentAdmission(id) {
            vm.loadingActions = true;
            vm.CanDeleteGovernmentAdmission = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            GovernmentAdmissionService.canDeleteGovernmentAdmission(id).then(function (response) { vm.CanDeleteGovernmentAdmission = response.data, vm.loadingActions = false });
        }

        //function deleteAGovernmentAdmission(id) {
        //    return GovernmentAdmissionService.deleteGovernmentAdmission(id).then(function () { initialise(); });
        //};

    }

})();
