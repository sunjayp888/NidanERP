(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('PlanningService', PlanningService);

    PlanningService.$inject = ['$http'];

    function PlanningService($http) {
        var service = {
            retrievePlannings: retrievePlannings,
            updateEventPlanning: updateEventPlanning,
            retrievePlanningQuestions: retrievePlanningQuestions
        };

        return service;

        function retrievePlanningQuestions() {
            var url = "/Event/PlanningQuestion";
            return $http.post(url);
        }

        function retrievePlannings(eventId) {
            var url = "/Event/EventPlanningList",
                data = {
                    eventId: eventId
                };
            return $http.post(url, data);
        }

        function updateEventPlanning(eventId, eventPlannings) {
            var url = "/EventManagement/UpdateEventPlanning";
            var data = { eventPlannings: eventPlannings, eventId: eventId }
            return $http.post(url, data);
        }
    }
})();