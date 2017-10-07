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
        vm.rooms = [];
        vm.centreFixAssets = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFixAsset = editFixAsset;
        vm.searchFixAsset = searchFixAsset;
        vm.viewFixAsset = viewFixAsset;
        vm.searchFixAssetByDate = searchFixAssetByDate;
        vm.retrieveCentreFixAssetsByFixAssetId = retrieveCentreFixAssetsByFixAssetId;
        vm.openCentreFixAssetModalPopUp = openCentreFixAssetModalPopUp;
        vm.markAsset = markAsset;
        vm.selectEntity = selectEntity;
        vm.selectAll = selectAll;
        //vm.retrieveRoom = retrieveRoom;
        vm.rooms=[];
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.allItemsSelected = false;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "DateofPurchase";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("DateofPurchase");
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

        function retrieveCentreFixAssetsByFixAssetId(FixAssetId) {
            vm.FixAssetId;
            vm.orderBy.property = "CentreFixAssetId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return FixAssetService.retrieveCentreFixAssetsByFixAssetId(FixAssetId, vm.paging, vm.orderBy)
            .then(function (response) {
                vm.centreFixAssets = response.data.Items;
                vm.paging.totalPages = response.data.TotalPages;
                vm.paging.totalResults = response.data.TotalResults;
                return vm.centreFixAssets;
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

        function retrieveRooms() {
            return FixAssetService.retrieveRooms()
                .then(function (response) {
                    vm.rooms = response.data;
                    return vm.rooms;
                });
        }

        function openCentreFixAssetModalPopUp(fixAssetId) {
            vm.fixAssetId = fixAssetId;
            retrieveRooms();
            return FixAssetService.retrieveCentreFixAssetsByFixAssetId(vm.fixAssetId, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropDownRoom').val("Select Room");
                    $("#txtDateofUse").val('');
                });
        }

        function markAsset() {
            vm.roomId = $('#dropDownRoom').val();
            vm.dateofPutToUse = $('#txtDateofUse').val();
            for (var i = 0; i <= vm.centreFixAssets.length; i++) {
                if (vm.centreFixAssets[i].Ischecked) {
                    vm.centreFixAssets[i].RoomId = vm.roomId;
                    vm.centreFixAssets[i].DateofPutToUse = vm.dateofPutToUse;
                    //vm.centreFixAssets[i].AssetCode = vm.centreFixAssets.rooms;
                    return FixAssetService.markAsset(vm.roomId, vm.dateofPutToUse, vm.centreFixAssets).then(function (response) {
                        vm.centreFixAssets = response.data.items;
                        return vm.centreFixAssets;
                    });
                }
            }
            return vm.centreFixAssets;
        }

        function selectEntity() {
            // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
            for (var i = 0; i < vm.centreFixAssets.length; i++) {
                if (!vm.centreFixAssets[i].Ischecked) {
                    vm.allItemsSelected = false;
                    return;
                }
            }
            //If not the check the "allItemsSelected" checkbox
            vm.allItemsSelected = true;
        };

        function selectAll() {
            // Loop through all the entities and set their isChecked property
            for (var i = 0; i < vm.centreFixAssets.length; i++) {
                vm.centreFixAssets[i].isChecked = vm.allItemsSelected;
            }
        };

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
