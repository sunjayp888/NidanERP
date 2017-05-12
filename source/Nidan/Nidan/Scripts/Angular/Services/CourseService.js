(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CourseService', CourseService);

    CourseService.$inject = ['$http'];

    function CourseService($http) {
        var service = {
            retrieveCourses: retrieveCourses,
            canDeleteCourse: canDeleteCourse,
            deleteCourse: deleteCourse,
            searchCourse: searchCourse,
            deleteEnquiryCourse: deleteEnquiryCourse,
            deleteSubjectCourse: deleteSubjectCourse,
            retrieveCourseBySectorId: retrieveCourseBySectorId
        };

        return service;

        function retrieveCourses(Paging, OrderBy) {

            var url = "/Course/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCourseBySectorId(sectorId) {
            var url = "/Enquiry/GetCourse",
                data = {
                    sectorId: sectorId
                };
            return $http.post(url, data);
        }

        function searchCourse(SearchKeyword, Paging, OrderBy) {
            var url = "/Course/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function canDeleteCourse(id) {
            var url = "/Course/CanDeleteCourse",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteCourse(id) {
            var url = "/Course/Delete",
                data = { id: id };

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