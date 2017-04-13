(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('SubjectTrainerController', SubjectTrainerController);

    SubjectTrainerController.$inject = ['SubjectTrainerService', 'TeamService', '$filter'];

    function SubjectTrainerController(SubjectTrainerService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.subjectId;
        vm.trainers = [];
        vm.employmentTeams = [];
        vm.initialise = initialise;
        vm.createSubjectTrainer = createSubjectTrainer;
        vm.deleteSubjectTrainer = deleteSubjectTrainer;

        function initialise(subjectId) {
            vm.subjectId = subjectId,
            retrieveTeams();
        }

        function retrieveTrainers() {
            return TeamService.retrieveSubjectTrainers()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveSubjectTrainers();
                    return vm.teams;
                });
        }

        function retrieveSubjectTrainers() {
            return SubjectTrainerService.retrieveSubjectTrainers(vm.subjectId)
                .then(function (response) {
                    //vm.employmentTeams = response.data.Items;
                    angular.forEach(response.data, function (item) {
                        var trainer = $filter('filter')(vm.trainers, { TeamId: item.TeamId }, true)[0];
                        vm.employmentTeams.push({ TeamId: team.TeamId, Name: team.Name, Hex: team.Hex });
                    });
                    return vm.employmentTeams;
                });
        }

        function createEmploymentTeam($item) {
            return SubjectTrainerService.createEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }

        function deleteEmploymentTeam($item) {
            return SubjectTrainerService.deleteEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }


    }
})();
