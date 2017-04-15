(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('TrainerService', TrainerService);

    TrainerService.$inject = ['$http'];

    function TrainerService($http) {
        var service = {
            retrieveTrainers: retrieveTrainers,
            searchTrainer: searchTrainer,
            deleteBatchTrainer: deleteBatchTrainer
        };

        return service;

        function retrieveTrainers(Paging, OrderBy) {

            var url = "/Trainer/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchTrainer(SearchKeyword, Paging, OrderBy) {
            var url = "/Trainer/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
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