(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ExpenseProjectController', ExpenseProjectController);

    ExpenseProjectController.$inject = ['ExpenseProjectService', 'TeamService', '$filter'];

    function ExpenseProjectController(ExpenseProjectService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.expenseId;
        vm.projects = [];
        vm.employmentTeams = [];
        vm.initialise = initialise;
        vm.createExpenseProject = createExpenseProject;
        vm.deleteExpenseProject = deleteExpenseProject;

        function initialise(expenseId) {
            vm.expenseId = expenseId;
            retrieveTeams();
        }

        function retrieveProjects() {
            return TeamService.retrieveExpenseProjects()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveExpenseProjects();
                    return vm.teams;
                });
        }

        function retrieveExpenseProjects() {
            return ExpenseProjectService.retrieveExpenseProjects(vm.expenseId)
                .then(function (response) {
                    //vm.employmentTeams = response.data.Items;
                    angular.forEach(response.data, function (item) {
                        var project = $filter('filter')(vm.projects, { TeamId: item.TeamId }, true)[0];
                        vm.employmentTeams.push({ TeamId: team.TeamId, Name: team.Name, Hex: team.Hex });
                    });
                    return vm.employmentTeams;
                });
        }
    }
})();
