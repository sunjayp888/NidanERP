(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CompanyService', CompanyService);

    CompanyService.$inject = ['$http'];

    function CompanyService($http) {
        var service = {
            retrieveCompanies: retrieveCompanies,
            searchCompany: searchCompany,
            searchCompanyByDate: searchCompanyByDate,
        };

        return service;

        function retrieveCompanies(Paging, OrderBy) {

            var url = "/Company/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }


        function searchCompany(SearchKeyword, Paging, OrderBy) {
            var url = "/Company/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCompanyByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Company/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();