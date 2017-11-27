(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ModuleExamSetController', ModuleExamSetController);

    ModuleExamSetController.$inject = ['$window', 'ModuleExamSetService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ModuleExamSetController($window, ModuleExamSetService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.moduleExamSets = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editModuleExamSet = editModuleExamSet;
        vm.searchModuleExamSet = searchModuleExamSet;
        vm.viewModuleExamSet = viewModuleExamSet;
        vm.retrieveModuleExamSets = retrieveModuleExamSets;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "ModuleExamSetId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("ModuleExamSetId");
        }

        function retrieveModuleExamSets() {
            return ModuleExamSetService.retrieveModuleExamSets(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.moduleExamSets = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.moduleExamSets;
                });
        }

        function searchModuleExamSet(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ModuleExamSetService.searchModuleExamSet(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.moduleExamSets = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.moduleExamSets.length === 0 ? "No Records Found" : "";
                    return vm.moduleExamSets;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchModuleExamSet(vm.searchKeyword);
            } else {
                return retrieveModuleExamSets();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveModuleExamSets();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editModuleExamSet(id) {
            $window.location.href = "/ModuleExamSet/Edit/" + id;
        }

        function viewModuleExamSet(moduleExamSetId) {
            $window.location.href = "/ModuleExamSet/View/" + moduleExamSetId;
        }

    }

})();
