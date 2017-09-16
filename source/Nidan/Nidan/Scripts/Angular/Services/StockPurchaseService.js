(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('StockPurchaseService', StockPurchaseService);

    StockPurchaseService.$inject = ['$http'];

    function StockPurchaseService($http) {
        var service = {
            retrieveStockPurchases: retrieveStockPurchases,
            searchStockPurchaseByDate: searchStockPurchaseByDate,
            searchStockPurchase: searchStockPurchase,
            retrieveStockIssuesByStockPurchaseId: retrieveStockIssuesByStockPurchaseId,
            retrieveStockPurchaseByStationary: retrieveStockPurchaseByStationary,
            retrieveStockPurchaseBySector: retrieveStockPurchaseBySector
        };

        return service;

        function retrieveStockPurchases(Paging, OrderBy) {

            var url = "/StockPurchase/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchStockPurchaseByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/StockPurchase/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };
            return $http.post(url, data);
        }

        function searchStockPurchase(SearchKeyword, Paging, OrderBy) {
            var url = "/StockPurchase/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };
            return $http.post(url, data);
        }

        function retrieveStockIssuesByStockPurchaseId(stockPurchaseId, Paging, OrderBy) {

            var url = "/StockPurchase/StockIssueList",
                data = {
                    stockPurchaseId:stockPurchaseId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveStockPurchaseBySector(Paging, OrderBy) {

            var url = "/StockPurchase/StockPurchaseBySector" +
                    "",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveStockPurchaseByStationary(Paging, OrderBy) {

            var url = "/StockPurchase/StockPurchaseByStationary",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();