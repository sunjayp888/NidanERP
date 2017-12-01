(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ActivityTaskService', ActivityTaskService);

    ActivityTaskService.$inject = ['$http'];

    function ActivityTaskService($http) {
        var service = {
            retrieveActivityTasks: retrieveActivityTasks,
            searchActivityTask: searchActivityTask,
            searchActivityTaskByDate: searchActivityTaskByDate,
            retrieveActivityTasksByActivityId: retrieveActivityTasksByActivityId
        };

        return service;

        function retrieveActivityTasks(Paging, OrderBy) {

            var url = "/ActivityTask/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveActivityTasksByActivityId(activityId, Paging, OrderBy) {

            var url = "/ActivityTask/ActivityTaskByActivityId",
                data = {
                    activityId: activityId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchActivityTask(SearchKeyword, Paging, OrderBy) {
            var url = "/ActivityTask/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchActivityTaskByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/ActivityTask/SearchByDate",
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