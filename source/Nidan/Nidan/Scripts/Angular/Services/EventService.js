(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('EventService', EventService);

    EventService.$inject = ['$http'];

    function EventService($http) {
        var service = {
            retrieveEvents: retrieveEvents,
            //canDeleteAbsenceType: canDeleteAbsenceType,
            //deleteAbsenceType: deleteAbsenceType
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

        //function canDeleteAbsenceType(id) {
        //    var url = "/AbsenceType/CanDeleteAbsenceType",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}

        //function deleteAbsenceType(id) {
        //    var url = "/AbsenceType/Delete",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}
    }
})();