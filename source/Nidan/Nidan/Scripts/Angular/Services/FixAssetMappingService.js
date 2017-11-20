(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FixAssetMappingService', FixAssetMappingService);

    FixAssetMappingService.$inject = ['$http'];

    function FixAssetMappingService($http) {
        var service = {
            retrieveFixAssetMappingbyAssetClassId: retrieveFixAssetMappingbyAssetClassId,
            searchFixAssetByAssetOutStatusId: searchFixAssetByAssetOutStatusId,
            searchFixAssetMapping: searchFixAssetMapping,
            retrieveAssignTypes: retrieveAssignTypes,
            retrieveFixAssetMappingList: retrieveFixAssetMappingList,
            openfixAssetMappingId: openfixAssetMappingId,
            updateFixAssetMapping: updateFixAssetMapping,
            assignFixAsset:assignFixAsset,
            retrieveAssignOutStatus: retrieveAssignOutStatus,
            retrieveFixAssetMappingByCentreId: retrieveFixAssetMappingByCentreId,
            retrieveRooms: retrieveRooms
        };

        return service;

        function retrieveFixAssetMappingByCentreId(Paging, OrderBy) {

            var url = "/FixAssetMapping/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }


        function retrieveFixAssetMappingbyAssetClassId(assetClassId, Paging, OrderBy) {

            var url = "/FixAssetMapping/FixAssetMappingListByAssetClassId",
                data = {
                    assetClassId: assetClassId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchFixAssetByAssetOutStatusId(assetOutStatusId, Paging, OrderBy) {

            var url = "/FixAssetMapping/FixAssetByAssetOutStatusId",
                data = {
                    assetOutStatusId: assetOutStatusId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchFixAssetMapping(SearchKeyword, Paging, OrderBy) {
            var url = "/FixAssetMapping/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveAssignOutStatus() {

            var url = "/FixAssetMapping/AssignOutStatus";
            return $http.post(url);
        }

        function retrieveAssignTypes() {

            var url = "/FixAssetMapping/AssignType";
            return $http.post(url);
        }
        //retrieveRooms
        function retrieveRooms() {

            var url = "/FixAssetMapping/Room";
            return $http.post(url);
        }

        function retrieveFixAssetMappingList(fixAssetMappings, Paging, OrderBy) {

            var url = "/FixAssetMapping/FixAssetMappingCheckedList",
                data = {
                    fixAssetMappings: fixAssetMappings,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function openfixAssetMappingId(fixAssetMappingId, Paging, OrderBy) {

            var url = "/FixAssetMapping/FixAssetMappingByFixAssetMappingId",
                data = {
                    fixAssetMappingId: fixAssetMappingId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function updateFixAssetMapping(fixAssetMapping) {
            var url = "/FixAssetMapping/UpdateFixAssetMapping",
                data = {
                    fixAssetMapping: fixAssetMapping
                };
            return $http.post(url, data);
        }

        function assignFixAsset(fixAssetMappings) {
            var url = "/FixAssetMapping/AssignFixAssetMapping",
                data = {
                    fixAssetMappings: fixAssetMappings
                };
            return $http.post(url, data);
        }

    }
})();