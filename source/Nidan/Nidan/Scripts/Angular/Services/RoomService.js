(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('RoomService', RoomService);

    RoomService.$inject = ['$http'];

    function RoomService($http) {
        var service = {
            retrieveRooms: retrieveRooms,
            canDeleteRoom: canDeleteRoom,
            deleteRoom: deleteRoom,
            searchRoom: searchRoom
        };

        return service;

        function retrieveRooms(Paging, OrderBy) {

            var url = "/Room/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchRoom(SearchKeyword, Paging, OrderBy) {
            var url = "/Room/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteRoom(id) {
            var url = "/Room/CanDeleteRoom",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteRoom(id) {
            var url = "/Room/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();