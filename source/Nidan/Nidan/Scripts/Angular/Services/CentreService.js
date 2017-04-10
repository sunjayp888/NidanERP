(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CentreService', CentreService);

    CentreService.$inject = ['$http'];

    function CentreService($http) {
        var service = {
            retrieveCentres: retrieveCentres,
            retrieveUnassignedCentreCourses: retrieveUnassignedCentreCourses,
            retrieveCentreCourses: retrieveCentreCourses,
            assignCentreCourse: assignCentreCourse,
            unassignCentreCourse: unassignCentreCourse
        };

        return service;

        function retrieveCentres(Paging, OrderBy) {

            var url = "/Centre/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveUnassignedCentreCourses(centreId) {
            var url = "/Centre/UnassignedCentreCourses",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }
        function retrieveCentreCourses(centreId) {
            var url = "/Centre/CentreCourses",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function assignCentreCourse(centreId, courseId) {
            var url = "/Centre/AssignCentreCourse",
                data = {
                    centreId: centreId,
                    courseId: courseId
                };

            return $http.post(url, data);
        }

        function unassignCentreCourse(centreId, courseId) {
            var url = "/Centre/UnassignCentreCourse",
                data = {
                    centreId: centreId,
                    courseId: courseId
                };
            return $http.post(url, data);
        }
    }
})();