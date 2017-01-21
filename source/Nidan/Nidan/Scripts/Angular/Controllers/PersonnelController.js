(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PersonnelController', PersonnelController);

    PersonnelController.$inject = ['$window', 'PersonnelService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function PersonnelController($window, PersonnelService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.personnel = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.viewPersonnelProfile = viewPersonnelProfile;
        vm.searchPersonnel = searchPersonnel;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("Forenames");
        }

        function retrievePersonnel() {
            return PersonnelService.retrievePersonnel(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.personnel = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.personnel.length === 0 ? "No Records Found" : "";
                    return vm.personnel;
                });
        }

        function searchPersonnel(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return PersonnelService.searchPersonnel(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.personnel = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.personnel.length === 0 ? "No Records Found" : "";
                  return vm.personnel;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchPersonnel(vm.searchKeyword)();
            }
            return retrievePersonnel();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchPersonnel(vm.searchKeyword)();
            }
            return retrievePersonnel();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function viewPersonnelProfile(personnelId) {
            $window.location.href = "/Personnel/Profile/" + personnelId;
        }

    }
})();
