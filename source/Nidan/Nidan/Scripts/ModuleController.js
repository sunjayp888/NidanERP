(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ModuleController', ModuleController);

    ModuleController.$inject = ['$window', 'ModuleService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ModuleController($window, ModuleService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.modules = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editModule = editModule;
        vm.canDeleteModule = canDeleteModule;
        vm.deleteModule = deleteModule;
        vm.searchModule = searchModule;
        vm.viewModule = viewModule;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "ModuleId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("ModuleId");
        }

        function retrieveModules() {
            return ModuleService.retrieveModules(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.modules = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.modules;
                });
        }

        function searchModule(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ModuleService.searchModule(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.modules = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.modules.length === 0 ? "No Records Found" : "";
                    return vm.modules;
                });
        }

        function pageChanged() {
            return retrieveModules();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveModules();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editModule(id) {
            $window.location.href = "/Module/Edit/" + id;
        }

        function canDeleteModule(id) {
            vm.loadingActions = true;
            vm.CanDeleteModule = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            ModuleService.canDeleteModule(id).then(function (response) { vm.CanDeleteModule = response.data, vm.loadingActions = false });
        }

        function deleteModule(id) {
            return ModuleService.deleteModule(id).then(function () { initialise(); });
        };

        function viewModule(moduleId) {
            $window.location.href = "/Module/Edit/" + moduleId;
        }

    }

})();
