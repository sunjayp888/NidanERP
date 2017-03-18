(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('QuestionService', QuestionService);

    QuestionService.$inject = ['$http'];

    function QuestionService($http) {
        var service = {
            retrieveQuestions: retrieveQuestions,
            canDeleteQuestion: canDeleteQuestion,
            deleteQuestion: deleteQuestion,
            searchQuestion: searchQuestion
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

        function searchQuestion(SearchKeyword, Paging, OrderBy) {
            var url = "/Question/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteQuestion(id) {
            var url = "/Question/CanDeleteQuestion",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteQuestion(id) {
            var url = "/Question/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();