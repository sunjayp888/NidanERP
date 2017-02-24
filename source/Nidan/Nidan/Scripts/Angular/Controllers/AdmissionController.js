(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AdmissionController', AdmissionController);

    AdmissionController.$inject = ['$window', 'AdmissionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AdmissionController($window, AdmissionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.admissions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAdmission = editAdmission;
        vm.canDeleteAdmission = canDeleteAdmission;
        //vm.deleteAdmission = deleteAdmission;
        vm.searchAdmission = searchAdmission;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("FirstName");
        }

        function retrieveAdmissions() {
            return AdmissionService.retrieveAdmissions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.admissions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.admissions;
                });
        }

        function searchAdmission(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return AdmissionService.searchAdmission(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.admissions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.admissions.length === 0 ? "No Records Found" : "";
                  return vm.admissions;
              });
        }

        function pageChanged() {
            return retrieveAdmissions();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAdmissions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAdmission(id) {
            $window.location.href = "/Admission/Edit/" + id;
        }

        function canDeleteAdmission(id) {
            vm.loadingActions = true;
            vm.CanDeleteAdmission = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            AdmissionService.canDeleteAdmission(id).then(function (response) { vm.CanDeleteAdmission = response.data, vm.loadingActions = false });
        }

        //function deleteAAdmission(id) {
        //    return AdmissionService.deleteAdmission(id).then(function () { initialise(); });
        //};

    }

})();
