(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('EnquiryCourseService', EnquiryCourseService);

    EnquiryCourseService.$inject = ['$http'];

    function EnquiryCourseService($http) {
        var service = {
            createEnquiryCourse: createEnquiryCourse,
            retrieveEnquiryCourses: retrieveEnquiryCourses,
            deleteEnquiryCourse: deleteEnquiryCourse
        };

        return service;

        function createEnquiryCourse(enquiryId, courseId) {

            var url = "/EnquiryCourse/Create",
                data = {
                    EnquiryId: enquiryId,
                    CourseId: courseId
                };

            return $http.post(url, data);
        }

        function retrieveEnquiryCourses(enquiryId) {

            var url = "/EnquiryCourse/List",
                data = {
                    enquiryId: enquiryId
                };

            return $http.post(url, data);
        }

        function deleteEnquiryCourse(enquiryId, courseId) {
            var url = "/EnquiryCourse/Delete",
              data = {
                  enquiryId: enquiryId,
                  courseId: courseId
              };
            return $http.post(url, data);
        }

    }
})();