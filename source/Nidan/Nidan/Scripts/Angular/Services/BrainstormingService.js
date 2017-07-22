(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BrainstormingService', BrainstormingService);

    BrainstormingService.$inject = ['$http'];

    function BrainstormingService($http) {
        var service = {
            retrieveBrainstormings: retrieveBrainstormings,
            createEventBrainstorming: createEventBrainstorming
        };

        return service;

        function retrieveBrainstormings(Paging, OrderBy) {

            var url = "/Event/BrainstormingList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function createEventBrainstorming(eventId, eventBrainstormings) {

            var url = "/EventBrainstorming/Create";
            var data = { eventBrainstormings: eventBrainstormings, eventId: eventId}
            return $http.post(url, data);
        }
    }
})();