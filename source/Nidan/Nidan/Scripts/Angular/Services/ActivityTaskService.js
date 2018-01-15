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
            retrieveActivityTasksByActivityId: retrieveActivityTasksByActivityId,
            deleteActivityTask: deleteActivityTask,
            openAddTaskStatus: openAddTaskStatus,
            retrieveTaskStates: retrieveTaskStates,
            createTaskStatus: createTaskStatus,
            retrieveActivityTaskStatesByActivityTaskId: retrieveActivityTaskStatesByActivityTaskId
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

        function retrieveActivityTaskStatesByActivityTaskId(activityTaskId, Paging, OrderBy) {

            var url = "/ActivityTask/ActivityTaskStatesByActivityTaskId",
                data = {
                    activityTaskId: activityTaskId,
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

        function deleteActivityTask(activityTaskId) {

            var url = "/ActivityTask/Delete",
                data = {
                    activityTaskId: activityTaskId
                };

            return $http.post(url, data);
        }

        function openAddTaskStatus(activityTaskId) {

            var url = "/ActivityTask/AddTaskStatus",
                data = {
                    activityTaskId: activityTaskId
                };

            return $http.post(url, data);
        }

        function retrieveTaskStates() {

            var url = "/ActivityTask/GetTaskStates";
            return $http.post(url);
        }

        function createTaskStatus(activityTaskState) {
            var url = "/ActivityTask/CreateActivityTaskStatus",
                data = {
                    activityTaskState: activityTaskState
                };
            return $http.post(url, data);
        }
    }
})();