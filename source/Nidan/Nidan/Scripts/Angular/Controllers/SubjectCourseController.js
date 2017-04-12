(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('SubjectCourseController', SubjectCourseController);

    SubjectCourseController.$inject = ['SubjectCourseService', 'TeamService', '$filter'];

    function SubjectCourseController(SubjectCourseService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.subjectId;
        vm.courses = [];
        vm.employmentTeams = [];
        vm.initialise = initialise;
        vm.createSubjectCourse = createSubjectCourse;
        vm.deleteSubjectCourse = deleteSubjectCourse;

        function initialise(subjectId) {
            vm.subjectId = subjectId
            retrieveTeams();
        }

        function retrieveCourses() {
            return TeamService.retrieveSubjectCourses()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveSubjectCourses();
                    return vm.teams;
                });
        }

        function retrieveSubjectCourses() {
            return SubjectCourseService.retrieveSubjectCourses(vm.subjectId)
                .then(function (response) {
                    //vm.employmentTeams = response.data.Items;
                    angular.forEach(response.data, function (item) {
                        var course = $filter('filter')(vm.courses, { TeamId: item.TeamId }, true)[0];
                        vm.employmentTeams.push({ TeamId: team.TeamId, Name: team.Name, Hex: team.Hex });
                    });
                    return vm.employmentTeams;
                });
        }

        function createEmploymentTeam($item) {
            return SubjectCourseService.createEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }

        function deleteEmploymentTeam($item) {
            return SubjectCourseService.deleteEmploymentTeam(vm.employmentId, $item.TeamId)
              .then(function () {
              });
        }


    }
})();
