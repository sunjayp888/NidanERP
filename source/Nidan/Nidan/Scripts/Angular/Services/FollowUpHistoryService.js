(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FollowUpHistoryService', FollowUpHistoryService);

    FollowUpHistoryService.$inject = ['$http'];

    function FollowUpHistoryService($http) {
        var service = {
            retrieveFollowUpHistories: retrieveFollowUpHistories
            //canDeleteSession: canDeleteSession,
            //deleteSession: deleteSession,
            //searchSession: searchSession
        };

        return service;

        function retrieveFollowUpHistories(Paging, OrderBy) {

            var url = "/FollowUp/FollowUpHistoryList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();