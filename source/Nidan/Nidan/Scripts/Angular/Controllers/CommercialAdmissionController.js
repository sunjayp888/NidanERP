(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CommercialAdmissionController', CommercialAdmissionController);

    CommercialAdmissionController.$inject = ['$window', 'CommercialAdmissionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CommercialAdmissionController($window, CommercialAdmissionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.commercialAdmissions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCommercialAdmission = editCommercialAdmission;
        vm.canDeleteCommercialAdmission = canDeleteCommercialAdmission;
        //vm.deleteCommercialAdmission = deleteCommercialAdmission;
        vm.searchCommercialAdmission = searchCommercialAdmission;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("FirstName");
        }

        function retrieveCommercialAdmissions() {
            return CommercialAdmissionService.retrieveCommercialAdmissions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.commercialAdmissions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.commercialAdmissions;
                });
        }

        function searchCommercialAdmission(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return CommercialAdmissionService.searchCommercialAdmission(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.commercialAdmissions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.commercialAdmissions.length === 0 ? "No Records Found" : "";
                  return vm.commercialAdmissions;
              });
        }

        function pageChanged() {
            return retrieveCommercialAdmissions();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCommercialAdmissions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCommercialAdmission(id) {
            $window.location.href = "/CommercialAdmission/Edit/" + id;
        }

        function canDeleteCommercialAdmission(id) {
            vm.loadingActions = true;
            vm.CanDeleteCommercialAdmission = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            CommercialAdmissionService.canDeleteCommercialAdmission(id).then(function (response) { vm.CanDeleteCommercialAdmission = response.data, vm.loadingActions = false });
        }

        //function deleteACommercialAdmission(id) {
        //    return CommercialAdmissionService.deleteCommercialAdmission(id).then(function () { initialise(); });
        //    return CommercialAdmissionService.deleteCommercialAdmission(id).then(function () { initialise(); });
        //};

    }

})();