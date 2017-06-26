(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ExpenseProjectService', ExpenseProjectService);

    ExpenseProjectService.$inject = ['$http'];

    function ExpenseProjectService($http) {
        var service = {
            createExpenseProject: createExpenseProject,
            retrieveExpenseProjects: retrieveExpenseProjects,
            deleteExpenseProject: deleteExpenseProject
        };

        return service;

        function createExpenseProject(expenseId, projectId) {

            var url = "/ExpenseProject/Create",
                data = {
                    ExpenseId: expenseId,
                    ProjectId: projectId
                };

            return $http.post(url, data);
        }

        function retrieveExpenseProjects(expenseId) {

            var url = "/ExpenseProject/List",
                data = {
                    expenseId: expenseId
                };

            return $http.post(url, data);
        }

        function deleteExpenseProject(expenseId, projectId) {
            var url = "/ExpenseProject/Delete",
                data = {
                    expenseId: expenseId,
                    projectId: projectId
                };
            return $http.post(url, data);
        }
    }
})();