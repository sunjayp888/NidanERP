(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BrainstormingService', BrainstormingService);

    BrainstormingService.$inject = ['$http'];

    function BrainstormingService($http) {
        var service = {
            retrieveBrainstormings: retrieveBrainstormings,
            updateEventBrainstorming: updateEventBrainstorming,
            retrieveBrainstormingQuestions: retrieveBrainstormingQuestions
        };

        return service;

        function retrieveBrainstormingQuestions() {
            var url = "/Event/BrainStormingQuestion";
            return $http.post(url);
        }

        function retrieveBrainstormings(eventId) {
            var url = "/Event/EventBrainstormingList",
                data = {
                    eventId: eventId
                };
            return $http.post(url, data);
        }

        function updateEventBrainstorming(eventId, eventBrainstormings) {
            var url = "/EventManagement/UpdateEventBrainStorming";
            var data = { eventBrainstormings: eventBrainstormings, eventId: eventId }
            return $http.post(url, data);
        }
    }
})();