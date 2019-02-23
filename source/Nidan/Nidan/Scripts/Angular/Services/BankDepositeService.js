(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BankDepositeService', BankDepositeService);

    BankDepositeService.$inject = ['$http'];

    function BankDepositeService($http) {
        var service = {
            retrieveBankDeposites: retrieveBankDeposites,
            searchBankDeposite: searchBankDeposite,
            retrievePaymentModes: retrievePaymentModes,
            retrieveCentres: retrieveCentres,
            searchBankDepositeByDate: searchBankDepositeByDate,
            searchBankDepositeByDateByCentreId: searchBankDepositeByDateByCentreId,
            updateIsCleared: updateIsCleared,
            updateIsBounced: updateIsBounced
        };

        return service;

        function retrieveBankDeposites(Paging, OrderBy) {

            var url = "/BankDeposite/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBankDeposite(SearchKeyword, Paging, OrderBy) {
            var url = "/BankDeposite/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBankDepositeByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/BankDeposite/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchBankDepositeByDateByCentreId(centreId, FromDate, ToDate, Paging, OrderBy) {
            var url = "/BankDeposite/SearchBankDepositeByDateByCentreId",
                data = {
                    centreId: centreId,
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrievePaymentModes() {

            var url = "/BankDeposite/PaymentMode";
            return $http.post(url);
        }

        function retrieveCentres() {

            var url = "/BankDeposite/GetCentres";
            return $http.post(url);
        }

        function updateIsCleared(bankDepositeId) {
            var url = "/BankDeposite/UpdateIsCleared",
                data = {
                    bankDepositeId: bankDepositeId
                };

            return $http.post(url, data);
        }

        function updateIsBounced(bankDepositeId) {
            var url = "/BankDeposite/UpdateIsBounced",
                data = {
                    bankDepositeId: bankDepositeId
                };

            return $http.post(url, data);
        }
    }
})();