(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ProjectService', ProjectService);

    ProjectService.$inject = ['$http'];

    function ProjectService($http) {
        var service = {
            retrieveProjects: retrieveProjects,
            deleteExpenseProject: deleteExpenseProject
        };

        return service;

        function retrieveProjects(Paging, OrderBy) {

            var url = "/Project/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
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