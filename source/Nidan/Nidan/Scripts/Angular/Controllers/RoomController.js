(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('RoomController', RoomController);

    RoomController.$inject = ['$window', 'RoomService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function RoomController($window, RoomService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.rooms = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editRoom = editRoom;
        vm.canDeleteRoom = canDeleteRoom;
        vm.deleteRoom = deleteRoom;
        vm.searchRoom = searchRoom;
        vm.viewRoom = viewRoom;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "RoomId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("RoomId");
        }

        function retrieveRooms() {
            return RoomService.retrieveRooms(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.rooms = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.rooms;
                });
        }

        function searchRoom(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return RoomService.searchRoom(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.rooms = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.rooms.length === 0 ? "No Records Found" : "";
                  return vm.rooms;
              });
        }

        function pageChanged() {
            return retrieveRooms();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveRooms();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editRoom(id) {
            $window.location.href = "/Room/Edit/" + id;
        }

        function canDeleteRoom(id) {
            vm.loadingActions = true;
            vm.CanDeleteRoom = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            RoomService.canDeleteRoom(id).then(function (response) { vm.CanDeleteRoom = response.data, vm.loadingActions = false });
        }

        function deleteRoom(id) {
            return RoomService.deleteRoom(id).then(function () { initialise(); });
        };

        function viewRoom(roomId) {
            $window.location.href = "/Room/Edit/" + roomId;
        }

    }

})();
