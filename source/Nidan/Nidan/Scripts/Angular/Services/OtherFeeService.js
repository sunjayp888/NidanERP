(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OtherFeeService', OtherFeeService);

    OtherFeeService.$inject = ['$http'];

    function OtherFeeService($http) {
        var service = {
            retrieveOtherFees: retrieveOtherFees,
            //canDeleteOtherFee: canDeleteOtherFee,
            deleteOtherFee: deleteOtherFee,
            searchOtherFee: searchOtherFee,
            retrieveOtherFeesByCashMemo: retrieveOtherFeesByCashMemo
        };

        return service;

        function retrieveOtherFees(Paging, OrderBy) {

            var url = "/OtherFee/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchOtherFee(SearchKeyword, Paging, OrderBy) {
            var url = "/OtherFee/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        //function canDeleteOtherFee(id) {
        //    var url = "/OtherFee/CanDeleteOtherFee",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}

        function deleteOtherFee(centreId, otherfeeId, cashMemo) {
            var url = "/OtherFee/Delete",
                data = {
                    centreId: centreId,
                    otherfeeId: otherfeeId,
                    cashMemo: cashMemo
                };

            return $http.post(url, data);
        }

        function retrieveOtherFeesByCashMemo(cashMemo, Paging, OrderBy) {
            var url = "/OtherFee/ListByCashMemo",
                data = {
                    cashMemo: cashMemo,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

    }
})();