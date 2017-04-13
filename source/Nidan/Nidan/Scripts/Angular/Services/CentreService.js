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
            unassignCentreCourse: unassignCentreCourse,
            retrieveUnassignedCentreCourseInstallments: retrieveUnassignedCentreCourseInstallments,
            retrieveCentreCourseInstallments: retrieveCentreCourseInstallments,
            assignCentreCourseInstallment: assignCentreCourseInstallment,
            unassignCentreCourseInstallment: unassignCentreCourseInstallment
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

        function retrieveUnassignedCentreCourseInstallments(centreId) {
            var url = "/Centre/UnassignedCentreCourseInstallments",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function retrieveCentreCourseInstallments(centreId) {
            var url = "/Centre/CentreCourseInstallments",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function assignCentreCourseInstallment(centreId, courseInstallmentId) {
            var url = "/Centre/AssignCentreCourseInstallment",
                data = {
                    centreId: centreId,
                    courseInstallmentId: courseInstallmentId
                };

            return $http.post(url, data);
        }

        function unassignCentreCourseInstallment(centreId, courseInstallmentId) {
            var url = "/Centre/UnassignCentreCourseInstallment",
                data = {
                    centreId: centreId,
                    courseInstallmentId: courseInstallmentId
                };
            return $http.post(url, data);
        }
    }
})();