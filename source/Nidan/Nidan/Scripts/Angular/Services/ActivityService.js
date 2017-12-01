(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ActivityService', ActivityService);

    ActivityService.$inject = ['$http'];

    function ActivityService($http) {
        var service = {
            retrieveActivities: retrieveActivities,
            searchActivity: searchActivity,
            searchActivityByDate: searchActivityByDate
        };

        return service;

        function retrieveActivities(Paging, OrderBy) {

            var url = "/Activity/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchActivity(SearchKeyword, Paging, OrderBy) {
            var url = "/Activity/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchActivityByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Activity/SearchByDate",
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