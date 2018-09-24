(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('GovernmentMobilizationController', GovernmentMobilizationController);

    GovernmentMobilizationController.$inject = ['$window', 'GovernmentMobilizationService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function GovernmentMobilizationController($window, GovernmentMobilizationService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.governmentMobilizations = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editGovernmentMobilization = editGovernmentMobilization;
        vm.searchGovernmentMobilization = searchGovernmentMobilization;
        vm.viewGovernmentMobilization = viewGovernmentMobilization;
        vm.searchGovernmentMobilizationByDate = searchGovernmentMobilizationByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        //vm.districts = [];
        //vm.districtBlocks = [];
        //vm.retrieveDistrictBlocks = retrieveDistrictBlocks;
        //vm.districtBlockPanchayats = [];
        //vm.retrieveBlockPanchayats = retrieveBlockPanchayats;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveGovernmentMobilizations() {
            return GovernmentMobilizationService.retrieveGovernmentMobilizations(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.governmentMobilizations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.governmentMobilizations;
                });
        }

        function searchGovernmentMobilization(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return GovernmentMobilizationService.searchGovernmentMobilization(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.governmentMobilizations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.governmentMobilizations.length === 0 ? "No Records Found" : "";
                    return vm.governmentMobilizations;
                });
        }

        function searchGovernmentMobilizationByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return GovernmentMobilizationService.searchGovernmentMobilizationByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.governmentMobilizations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.governmentMobilizations.length === 0 ? "No Records Found" : "";
                    return vm.governmentMobilizations;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchGovernmentMobilization(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchGovernmentMobilizationByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveGovernmentMobilizations();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveGovernmentMobilizations();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editGovernmentMobilization(id) {
            $window.location.href = "/GovernmentMobilization/Edit/" + id;
        }

        function viewGovernmentMobilization(governmentMobilizationId) {
            $window.location.href = "/GovernmentMobilization/View/" + governmentMobilizationId;
        }

        //function retrieveDistrictBlocks(districtId) {
        //    return GovernmentMobilizationService.retrieveDistrictBlocks(districtId).then(function () {
        //        vm.districtBlocks = response.data;
        //    });
        //};

        //function retrieveBlockPanchayats(districtBlockId) {
        //    return GovernmentMobilizationService.retrieveBlockPanchayats(districtBlockId).then(function () {
        //        vm.blockPanchayats = response.data;
        //    });
        //};
    }
})();
