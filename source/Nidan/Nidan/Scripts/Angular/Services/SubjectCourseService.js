(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('SubjectCourseService', SubjectCourseService);

    SubjectCourseService.$inject = ['$http'];

    function SubjectCourseService($http) {
        var service = {
            retrieveCourses: retrieveCourses,
            deleteSubjectCourse: deleteSubjectCourse,
        };

        return service;

        function retrieveCourses(Paging, OrderBy) {

            var url = "/Course/SubjectCourseList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
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