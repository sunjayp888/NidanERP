(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FixAssetMappingController', FixAssetMappingController);

    FixAssetMappingController.$inject = ['$window', 'FixAssetMappingService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FixAssetMappingController($window, FixAssetMappingService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.fixAssetMappings = [];
        vm.assetClassId;
        vm.assignTypeId;
        vm.roomId;
        vm.assignTypes = [];
        vm.rooms = [];
        vm.test = [];
        vm.fixAssetMappingId;
        vm.rooms = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFixAssetMapping = editFixAssetMapping;
        vm.searchFixAssetMapping = searchFixAssetMapping;
        vm.viewFixAssetMapping = viewFixAssetMapping;
        vm.viewFixAssetMappingByAssetClassId = viewFixAssetMappingByAssetClassId;
        vm.retrieveFixAssetMappingbyAssetClassId = retrieveFixAssetMappingbyAssetClassId;
        //vm.searchFixAssetByCentreIdAssetClassId = searchFixAssetByCentreIdAssetClassId;
        vm.searchFixAssetByAssetOutStatusId = searchFixAssetByAssetOutStatusId;
        vm.openFixAssetMappingModalPopUp = openFixAssetMappingModalPopUp;
        vm.openfixAssetMappingId = openfixAssetMappingId;
        vm.retrieveAssignTypes = retrieveAssignTypes;
        vm.retrieveRooms = retrieveRooms;
        vm.assetOutStatusId;
        vm.assignOutStates = [];
        vm.updateFixAssetMapping = updateFixAssetMapping;
        vm.assignFixAsset = assignFixAsset;
        vm.retrieveAssignOutStatus = retrieveAssignOutStatus;
        vm.retrieveFixAssetMappingByCentreId = retrieveFixAssetMappingByCentreId;
        //vm.selectEntity = selectEntity;
        vm.isAssignButtonEnable = true;
        vm.canWeAssign = canWeAssign;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "FixAssetMappingId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("FixAssetMappingId");
        }

        function retrieveFixAssetMappingByCentreId() {
            vm.orderBy.property = "CentreId";
            vm.orderBy.direction = "Ascending";
            return FixAssetMappingService.retrieveFixAssetMappingByCentreId(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssetMappings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.fixAssetMappings;
                });
        }

        function retrieveFixAssetMappingbyAssetClassId(assetClassId) {
            vm.assetClassId = assetClassId;
            //== undefined ? getUrlParameter("assetClassId") : assetClassId;
            vm.orderBy.property = "FixAssetMappingId";
            vm.orderBy.direction = "Ascending";
            retrieveAssignOutStatus();
            retrieveAssignTypes();
            retrieveRooms();
            return FixAssetMappingService.retrieveFixAssetMappingbyAssetClassId(vm.assetClassId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssetMappings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.fixAssetMappings;
                });
        }

        function searchFixAssetByAssetOutStatusId() {
            vm.assetOutStatusId = $("#dropStatus").val();
            vm.orderBy.property = "FixAssetMappingId";
            vm.orderBy.direction = "Ascending";
            //retrieveAssignOutStatus();
            retrieveAssignTypes();
            retrieveRooms();
            return FixAssetMappingService.searchFixAssetByAssetOutStatusId(vm.assetOutStatusId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssetMappings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.fixAssetMappings;
                });
        }

        function searchFixAssetMapping(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return FixAssetMappingService.searchFixAssetMapping(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.fixAssetMappings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.fixAssetMappings.length === 0 ? "No Records Found" : "";
                    return vm.fixAssetMappings;
                });
        }

        //retrieveRooms
        function retrieveRooms() {
            return FixAssetMappingService.retrieveRooms()
                .then(function (response) {
                    vm.rooms = response.data;
                    return vm.rooms;
                });
        }

        function retrieveAssignTypes() {
            return FixAssetMappingService.retrieveAssignTypes()
                .then(function (response) {
                    vm.assignTypes = response.data;
                    return vm.assignTypes;
                });
        }

        function openFixAssetMappingModalPopUp(fixAssetMappings) {
            vm.fixAssetMappings = fixAssetMappings;
            return FixAssetMappingService.retrieveFixAssetMappingList(vm.fixAssetMappings, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropAssignType').val("Select Assign Type");
                    $('#dropAssignType').val();
                    $('#dropRoom').val("Select Room");
                    $('#dropRoom').val();
                    vm.fixAssetMappings = response.data;
                    return vm.fixAssetMappings;
                });
        }

        function retrieveAssignOutStatus() {
            return FixAssetMappingService.retrieveAssignOutStatus()
                .then(function (response) {
                    vm.assignOutStates = response.data;
                    return vm.assignOutStates;
                });
        }

        function openfixAssetMappingId(fixAssetMappingId) {
            vm.fixAssetMappingId = fixAssetMappingId;
            return FixAssetMappingService.openfixAssetMappingId(vm.fixAssetMappingId, vm.paging, vm.orderBy)
                .then(function (response) {
                    //$('#dropStatus').val("Select Assign Type");
                    //$('#dropStatus').val();
                    retrieveFixAssetMappingbyAssetClassId(vm.assetClassId);
                });
        }

        function updateFixAssetMapping() {
            vm.assetOutStatusId = $("#assetOutStatus").val();
            var fixAssetMapping = {
                FixAssetMappingId: vm.fixAssetMappingId,
                AssetOutStatusId: $("#assetOutStatus").val(),
                StatusDate: $("#txtStatusDate").val(),
                Cost: $("#txtCost").val()
                // Remark: $("#txtRemark").val(),
            }
            return FixAssetMappingService.updateFixAssetMapping(fixAssetMapping)
                .then(function (response) {
                    retrieveFixAssetMappingbyAssetClassId(vm.assetClassId);
                });
        }

        function assignFixAsset() {
            var assignOutOwner = $("#dropAssignType").val() == 1 ? $("#dropRoom option:selected").text() : $("#txtAssignOutOwner").val();
            for (var i = 0; i < vm.fixAssetMappings.length; i++) {
                vm.fixAssetMappings[i].AssignTypeId = $("#dropAssignType").val();
                vm.fixAssetMappings[i].RoomId = $("#dropRoom").val() == "" ? "null" : $("#dropRoom").val();
                vm.fixAssetMappings[i].AssetOutOwner = assignOutOwner;
                vm.fixAssetMappings[i].AssetOutDate = $("#txtAssignOutDate").val();
                vm.fixAssetMappings[i].StatusDate = $("#txtAssignOutDate").val();
                vm.fixAssetMappings[i].Remark = $("#txtRemark").val();

            }
            return FixAssetMappingService.assignFixAsset(vm.fixAssetMappings).then(function () {
                retrieveFixAssetMappingbyAssetClassId(vm.assetClassId);
            });
        }

        function canWeAssign() {
            var count = 0;
            angular.forEach(vm.fixAssetMappings,
                function (value, key) {
                    if (value.Ischecked) {
                        count++;
                    }
                });
            vm.isAssignButtonEnable = (count === 0);
        }

        function pageChanged() {
            return retrieveFixAssetMappingbyAssetClassId();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveFixAssetMappingbyAssetClassId();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editFixAssetMapping(id) {
            $window.location.href = "/FixAssetMapping/Edit/" + id;
        }

        function viewFixAssetMapping(fixAssetMappingId) {
            $window.location.href = "/FixAssetMapping/View/" + fixAssetMappingId;
        }

        function viewFixAssetMappingByAssetClassId(assetClassId) {
            window.location.href = "/FixAssetMapping/FixAssetMappingbyAssetClassId?assetClassId=" + assetClassId;
        }

    }

})();
