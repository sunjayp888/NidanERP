(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchTrainerService', BatchTrainerService);

    BatchTrainerService.$inject = ['$http'];

    function BatchTrainerService($http) {
        var service = {
            createBatchTrainer: createBatchTrainer,
            retrieveBatchTrainers: retrieveBatchTrainers,
            deleteBatchTrainer: deleteBatchTrainer
        };

        return service;

        function createBatchTrainer(batchId, trainerId) {

            var url = "/BatchTrainer/Create",
                data = {
                    BatchId: batchId,
                    TrainerId: trainerId
                };

            return $http.post(url, data);
        }

        function retrieveBatchTrainers(batchId) {

            var url = "/BatchTrainer/List",
                data = {
                    batchId: batchId
                };

            return $http.post(url, data);
        }

        function deleteBatchTrainer(batchId, trainerId) {
            var url = "/BatchTrainer/Delete",
              data = {
                  batchId: batchId,
                  trainerId: trainerId
              };
            return $http.post(url, data);
        }

    }
})();