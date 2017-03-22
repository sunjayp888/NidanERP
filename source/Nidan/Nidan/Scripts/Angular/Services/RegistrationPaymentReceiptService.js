(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('RegistrationPaymentReceiptService', RegistrationPaymentReceiptService);

    RegistrationPaymentReceiptService.$inject = ['$http'];

    function RegistrationPaymentReceiptService($http) {
        var service = {
            retrieveRegistrationPaymentReceipts: retrieveRegistrationPaymentReceipts,
            retrieveEnquiries: retrieveEnquiries,
            canDeleteRegistrationPaymentReceipt: canDeleteRegistrationPaymentReceipt,
            deleteRegistrationPaymentReceipt: deleteRegistrationPaymentReceipt,
            searchEnquiry: searchEnquiry,
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

        function retrieveEnquiries(Paging, OrderBy) {

            var url = "/RegistrationPaymentReceipt/EnquiryList",
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

        function searchEnquiry(SearchKeyword, Paging, OrderBy) {
            var url = "/RegistrationPaymentReceipt/EnquirySearch",
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