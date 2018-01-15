(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CompanyFollowUpService', CompanyFollowUpService);

    CompanyFollowUpService.$inject = ['$http'];

    function CompanyFollowUpService($http) {
        var service = {
            retrieveCompanyFollowUps: retrieveCompanyFollowUps,
            searchCompanyFollowUpByDate: searchCompanyFollowUpByDate,
            searchCompanyFollowUp: searchCompanyFollowUp
        };

        return service;

        function retrieveCompanyFollowUps(Paging, OrderBy) {

            var url = "/CompanyFollowUp/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCompanyFollowUpByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/CompanyFollowUp/SearchByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCompanyFollowUp(SearchKeyword, Paging, OrderBy) {
            var url = "/CompanyFollowUp/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();