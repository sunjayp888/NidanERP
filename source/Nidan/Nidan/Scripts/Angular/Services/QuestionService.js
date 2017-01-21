(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('QuestionService', QuestionService);

    QuestionService.$inject = ['$http'];

    function QuestionService($http) {
        var service = {
            retrieveQuestions: retrieveQuestions,
            canDeleteAbsenceType: canDeleteAbsenceType,
            deleteQuestions: deleteQuestions
        };

        return service;

        function retrieveQuestions(Paging, OrderBy) {

            var url = "/Question/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteAbsenceType(id) {
            var url = "/AbsenceType/CanDeleteAbsenceType",
                data = {id: id};

            return $http.post(url, data);
        }

        function deleteQuestions(id) {
            var url = "/Question/Delete",
                data = {id: id};
            return $http.post(url, data);
        }
    }
})();