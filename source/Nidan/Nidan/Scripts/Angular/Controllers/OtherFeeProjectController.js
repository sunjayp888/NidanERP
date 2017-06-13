(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('OtherFeeProjectController', OtherFeeProjectController);

    OtherFeeProjectController.$inject = ['OtherFeeProjectService', 'TeamService', '$filter'];

    function OtherFeeProjectController(OtherFeeProjectService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.otherFeeId;
        vm.projects = [];
        vm.employmentTeams = [];
        vm.initialise = initialise;
        vm.createOtherFeeProject = createOtherFeeProject;
        

        function initialise(otherFeeId) {
            vm.otherFeeId = otherFeeId;
            retrieveTeams();
        }

        function retrieveProjects() {
            return TeamService.retrieveOtherFeeProjects()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveOtherFeeProjects();
                    return vm.teams;
                });
        }

        function retrieveOtherFeeProjects() {
            return OtherFeeProjectService.retrieveOtherFeeProjects(vm.otherFeeId)
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
