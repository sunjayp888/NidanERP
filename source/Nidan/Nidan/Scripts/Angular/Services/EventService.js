(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('EventService', EventService);

    EventService.$inject = ['$http'];

    function EventService($http) {
        var service = {
            retrieveEvents: retrieveEvents,
            canDeleteEvent: canDeleteEvent,
            deleteEvent: deleteEvent,
            searchEvent: searchEvent,
            retrieveQuestions: retrieveQuestions
        };

        return service;

        function retrieveEvents(Paging, OrderBy) {

            var url = "/Event/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchEvent(SearchKeyword, Paging, OrderBy) {
            var url = "/Event/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteEvent(id) {
            var url = "/Event/CanDeleteEvent",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteEvent(id) {
            var url = "/Event/Delete",
                data = { id: id };

            return $http.post(url, data);
        }

        function retrieveQuestions(eventFunctionId, Paging, OrderBy) {
            var url = "/Event/QuestionList",
            data = {
                eventFunctionId: eventFunctionId,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();