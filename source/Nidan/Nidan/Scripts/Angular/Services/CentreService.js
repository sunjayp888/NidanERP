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
            unassignCentreCourseInstallment: unassignCentreCourseInstallment,
            retrieveUnassignedCentreSchemes: retrieveUnassignedCentreSchemes,
            retrieveCentreSchemes: retrieveCentreSchemes,
            assignCentreScheme: assignCentreScheme,
            unassignCentreScheme: unassignCentreScheme,
            retrieveUnassignedCentreSectors: retrieveUnassignedCentreSectors,
            retrieveCentreSectors: retrieveCentreSectors,
            assignCentreSector: assignCentreSector,
            unassignCentreSector: unassignCentreSector
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

        // CentreCourse

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

        // CentreCourseInstallment

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

        // CentreScheme

        function retrieveUnassignedCentreSchemes(centreId) {
            var url = "/Centre/UnassignedCentreSchemes",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function retrieveCentreSchemes(centreId) {
            var url = "/Centre/CentreSchemes",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function assignCentreScheme(centreId, schemeId) {
            var url = "/Centre/AssignCentreScheme",
                data = {
                    centreId: centreId,
                    schemeId: schemeId
                };

            return $http.post(url, data);
        }

        function unassignCentreScheme(centreId, schemeId) {
            var url = "/Centre/UnassignCentreScheme",
                data = {
                    centreId: centreId,
                    schemeId: schemeId
                };
            return $http.post(url, data);
        }

        // CentreSector

        function retrieveUnassignedCentreSectors(centreId) {
            var url = "/Centre/UnassignedCentreSectors",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function retrieveCentreSectors(centreId) {
            var url = "/Centre/CentreSectors",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function assignCentreSector(centreId, sectorId) {
            var url = "/Centre/AssignCentreSector",
                data = {
                    centreId: centreId,
                    sectorId: sectorId
                };

            return $http.post(url, data);
        }

        function unassignCentreSector(centreId, sectorId) {
            var url = "/Centre/UnassignCentreSector",
                data = {
                    centreId: centreId,
                    sectorId: sectorId
                };
            return $http.post(url, data);
        }
    }
})();