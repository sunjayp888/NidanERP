(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FixAssetService', FixAssetService);

    FixAssetService.$inject = ['$http'];

    function FixAssetService($http) {
        var service = {
            retrieveFixAssets: retrieveFixAssets,
            searchFixAsset: searchFixAsset,
            retrieveCentres: retrieveCentres,
            searchFixAssetByCentreId: searchFixAssetByCentreId
        };

        return service;

        function retrieveFixAssets(Paging, OrderBy) {

            var url = "/FixAsset/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
        //searchFixAssetByCentreId
        function searchFixAssetByCentreId(centreId,Paging, OrderBy) {

            var url = "/FixAsset/FixAssetByCentreId",
                data = {
                    centreId:centreId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCentres() {

            var url = "/FixAsset/Centre";
            return $http.post(url);
        }

        function searchFixAsset(SearchKeyword, Paging, OrderBy) {
            var url = "/FixAsset/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        }
})();