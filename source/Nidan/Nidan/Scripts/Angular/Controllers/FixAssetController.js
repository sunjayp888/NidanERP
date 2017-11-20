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
        vm.fixAssetId;
        vm.centreId;
        vm.centres = [];
        vm.rooms = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFixAsset = editFixAsset;
        vm.searchFixAsset = searchFixAsset;
        vm.viewFixAsset = viewFixAsset;
        vm.retrieveFixAssets = retrieveFixAssets;
        vm.retrieveCentres = retrieveCentres;
        vm.searchFixAssetByCentreId = searchFixAssetByCentreId;
        vm.items = [];
        vm.retrieveItems = retrieveItems;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.allItemsSelected = false;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "FixAssetId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("FixAssetId");
        }

        function retrieveFixAssets() {
            vm.orderBy.property = "FixAssetId";
            vm.orderBy.direction = "Ascending";
            retrieveCentres();
            return FixAssetService.retrieveFixAssets(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssets = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.fixAssets;
                });
        }

        function searchFixAssetByCentreId() {//dropCentre
            vm.centreId = $('#dropCentre').val();
            vm.orderBy.property = "FixAssetId";
            vm.orderBy.class = "asc";
            order("FixAssetId");
            return FixAssetService.searchFixAssetByCentreId(vm.centreId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssets = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.fixAssets.length === 0 ? "No Records Found" : "";
                    return vm.fixAssets;
                });
        }

        function retrieveCentres() {
            return FixAssetService.retrieveCentres()
                .then(function (response) {
                    vm.centres = response.data;
                    return vm.centres;
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

        function retrieveItems(assetClassId) {
            return FixAssetService.retrieveItems(assetClassId).then(function () {
                vm.items = response.data;
            });
        };

    }

})();
