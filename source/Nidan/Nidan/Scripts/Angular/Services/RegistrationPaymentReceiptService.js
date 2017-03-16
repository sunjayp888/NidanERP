(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('RegistrationPaymentReceiptService', RegistrationPaymentReceiptService);

    RegistrationPaymentReceiptService.$inject = ['$http'];

    function RegistrationPaymentReceiptService($http) {
        var service = {
            retrieveRegistrationPaymentReceipts: retrieveRegistrationPaymentReceipts,
            canDeleteRegistrationPaymentReceipt: canDeleteRegistrationPaymentReceipt,
            deleteRegistrationPaymentReceipt: deleteRegistrationPaymentReceipt,
            searchRegistrationPaymentReceipt: searchRegistrationPaymentReceipt
        };

        return service;

        function retrieveRegistrationPaymentReceipts(Paging, OrderBy) {

            var url = "/RegistrationPaymentReceipt/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchRegistrationPaymentReceipt(SearchKeyword, Paging, OrderBy) {
            var url = "/RegistrationPaymentReceipt/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteRegistrationPaymentReceipt(id) {
            var url = "/RegistrationPaymentReceipt/CanDeleteRegistrationPaymentReceipt",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteRegistrationPaymentReceipt(id) {
            var url = "/RegistrationPaymentReceipt/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();