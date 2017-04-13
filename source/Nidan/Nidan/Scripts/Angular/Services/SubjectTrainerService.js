(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('SubjectTrainerService', SubjectTrainerService);

    SubjectTrainerService.$inject = ['$http'];

    function SubjectTrainerService($http) {
        var service = {
            createSubjectTrainer: createSubjectTrainer,
            retrieveSubjectTrainers: retrieveSubjectTrainers,
            deleteSubjectTrainer: deleteSubjectTrainer
        };

        return service;

        function createSubjectTrainer(subjectId, trainerId) {

            var url = "/SubjectTrainer/Create",
                data = {
                    SubjectId: subjectId,
                    TrainerId: trainerId
                };

            return $http.post(url, data);
        }

        function retrieveSubjectTrainers(subjectId) {

            var url = "/SubjectTrainer/List",
                data = {
                    subjectId: subjectId
                };

            return $http.post(url, data);
        }

        function deleteSubjectTrainer(subjectId, trainerId) {
            var url = "/SubjectTrainer/Delete",
              data = {
                  subjectId: subjectId,
                  trainerId: trainerId
              };
            return $http.post(url, data);
        }

    }
})();