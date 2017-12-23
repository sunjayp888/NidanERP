(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('EventDayService', EventDayService);

    EventDayService.$inject = ['$http'];

    function EventDayService($http) {
        var service = {
            retrieveEventDays: retrieveEventDays,
            updateEventDay: updateEventDay,
            retrieveEventDayQuestions: retrieveEventDayQuestions
        };

        return service;

        function retrieveEventDayQuestions() {
            var url = "/Event/EventDayQuestion";
            return $http.post(url);
        }

        function retrieveEventDays(eventId) {
            var url = "/Event/EventDayList",
                data = {
                    eventId: eventId
                };
            return $http.post(url, data);
        }

        function updateEventDay(eventId, eventDays) {
            var url = "/EventManagement/UpdateEventDay";
            var data = { eventDays: eventDays, eventId: eventId }
            return $http.post(url, data);
        }
    }
})();