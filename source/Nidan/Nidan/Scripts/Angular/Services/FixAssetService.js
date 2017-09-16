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
            searchFixAssetByDate: searchFixAssetByDate,
            retrieveCentreFixAssetsByFixAssetId: retrieveCentreFixAssetsByFixAssetId
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

        function searchFixAsset(SearchKeyword, Paging, OrderBy) {
            var url = "/FixAsset/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchFixAssetByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/FixAsset/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function retrieveCentreFixAssetsByFixAssetId(fixAssetId, Paging, OrderBy) {

            var url = "/FixAsset/CentreFixAssetList",
                data = {
                    fixAssetId: fixAssetId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

    }
})();