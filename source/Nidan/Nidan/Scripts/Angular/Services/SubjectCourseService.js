(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('SubjectCourseService', SubjectCourseService);

    SubjectCourseService.$inject = ['$http'];

    function SubjectCourseService($http) {
        var service = {
            createSubjectCourse: createSubjectCourse,
            retrieveSubjectCourses: retrieveSubjectCourses,
            deleteSubjectCourse: deleteSubjectCourse
        };

        return service;

        function createSubjectCourse(subjectId, courseId) {

            var url = "/SubjectCourse/Create",
                data = {
                    SubjectId: subjectId,
                    CourseId: courseId
                };

            return $http.post(url, data);
        }

        function retrieveSubjectCourses(subjectId) {

            var url = "/SubjectCourse/List",
                data = {
                    subjectId: subjectId
                };

            return $http.post(url, data);
        }

        function deleteSubjectCourse(subjectId, courseId) {
            var url = "/SubjectCourse/Delete",
              data = {
                  subjectId: subjectId,
                  courseId: courseId
              };
            return $http.post(url, data);
        }

    }
})();