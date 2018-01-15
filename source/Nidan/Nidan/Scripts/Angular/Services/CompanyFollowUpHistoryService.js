(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CompanyFollowUpHistoryService', CompanyFollowUpHistoryService);

    CompanyFollowUpHistoryService.$inject = ['$http'];

    function CompanyFollowUpHistoryService($http) {
        var service = {
            retrieveCompanyFollowUpHistories: retrieveCompanyFollowUpHistories
        };

        return service;

        function retrieveCompanyFollowUpHistories(Paging, OrderBy) {

            var url = "/CompanyFollowUp/CompanyFollowUpHistoryList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();