(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FixAssetController', FixAssetController);

    FixAssetController.$inject = ['$window', 'FixAssetService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FixAssetController($window, FixAssetService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.fixAssets = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFixAsset = editFixAsset;
        vm.searchFixAsset = searchFixAsset;
        vm.viewFixAsset = viewFixAsset;
        vm.searchFixAssetByDate = searchFixAssetByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "RoomId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("RoomId");
        }

        function retrieveFixAssets() {
            return FixAssetService.retrieveFixAssets(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssets = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.fixAssets;
                });
        }

        function searchFixAsset(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return FixAssetService.searchFixAsset(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.fixAssets = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.fixAssets.length === 0 ? "No Records Found" : "";
                  return vm.fixAssets;
              });
        }

        function searchFixAssetByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return FixAssetService.searchFixAssetByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.fixAssets = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.fixAssets.length === 0 ? "No Records Found" : "";
                  return vm.fixAssets;
              });
        }

        function pageChanged() {
            return retrieveFixAssets();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveFixAssets();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editFixAsset(id) {
            $window.location.href = "/FixAsset/Edit/" + id;
        }
        
        function viewFixAsset(fixAssetId) {
            $window.location.href = "/FixAsset/View/" + fixAssetId;
        }

    }

})();
