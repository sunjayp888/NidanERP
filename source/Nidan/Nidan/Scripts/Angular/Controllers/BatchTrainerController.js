(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BatchTrainerController', BatchTrainerController);

    BatchTrainerController.$inject = ['BatchTrainerService', 'TeamService', '$filter'];

    function BatchTrainerController(BatchTrainerService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.batchId;
        vm.trainers = [];
        vm.employmentTeams = [];
        vm.initialise = initialise;
        vm.createBatchTrainer = createBatchTrainer;
        vm.deleteBatchTrainer = deleteBatchTrainer;

        function initialise(batchId) {
            vm.batchId = batchId,
            retrieveTeams();
        }

        function retrieveTrainers() {
            return TeamService.retrieveBatchTrainers()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveBatchTrainers();
                    return vm.teams;
                });
        }

        function retrieveBatchTrainers() {
            return BatchTrainerService.retrieveBatchTrainers(vm.batchId)
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
            return BatchTrainerService.createEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }

        function deleteEmploymentTeam($item) {
            return BatchTrainerService.deleteEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }


    }
})();
