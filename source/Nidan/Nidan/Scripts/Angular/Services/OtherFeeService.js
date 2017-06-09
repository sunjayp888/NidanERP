(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OtherFeeService', OtherFeeService);

    OtherFeeService.$inject = ['$http'];

    function OtherFeeService($http) {
        var service = {
            retrieveOtherFees: retrieveOtherFees,
            canDeleteOtherFee: canDeleteOtherFee,
            deleteOtherFee: deleteOtherFee,
            searchOtherFee: searchOtherFee,
            //addExpense: addExpense
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

        function canDeleteOtherFee(id) {
            var url = "/OtherFee/CanDeleteOtherFee",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteOtherFee(id) {
            var url = "/OtherFee/Delete",
                data = { id: id };

            return $http.post(url, data);
        }

        //function addExpense() {
        //    var url = "/OtherFee/Create",
        //        data = { };

        //    return $http.post(url, data);
        //}
    }
})();