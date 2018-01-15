(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('PostEventService', PostEventService);

    PostEventService.$inject = ['$http'];

    function PostEventService($http) {
        var service = {
            retrievePostEvents: retrievePostEvents,
            updatePostEvent: updatePostEvent,
            retrievePostEventQuestions: retrievePostEventQuestions
        };

        return service;

        function retrievePostEventQuestions() {
            var url = "/Event/PostEventQuestion";
            return $http.post(url);
        }

        function retrievePostEvents(eventId) {
            var url = "/Event/PostEventList",
                data = {
                    eventId: eventId
                };
            return $http.post(url, data);
        }

        function updatePostEvent(eventId, postEvents) {
            var url = "/EventManagement/UpdatePostEvent";
            var data = { postEvents: postEvents, eventId: eventId }
            return $http.post(url, data);
        }
    }
})();