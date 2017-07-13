(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('EnquiryCourseController', EnquiryCourseController);

    EnquiryCourseController.$inject = ['EnquiryCourseService', 'TeamService', '$filter'];

    function EnquiryCourseController(EnquiryCourseService, TeamService, $filter) {
        /* jshint validthis:true */
        var vm = this;
        vm.enquiryId;
        vm.courses = [];
        vm.initialise = initialise;
        vm.createEnquiryCourse = createEnquiryCourse;
        vm.deleteEnquiryCourse = deleteEnquiryCourse;

        function initialise(enquiryId) {
            vm.enquiryId = enquiryId;
            retrieveCourses();
            retrieveEnquiryCourses();
        }

        function retrieveCourses() {
            return TeamService.retrieveEnquiryCourses()
                .then(function (response) {
                    vm.teams = response.data;
                    retrieveEnquiryCourses();
                    return vm.teams;
                });
        }

        function retrieveEnquiryCourses() {
            return EnquiryCourseService.retrieveEnquiryCourses(vm.enquiryId)
                .then(function (response) {
                    //vm.employmentTeams = response.data.Items;
                    angular.forEach(response.data, function (item) {
                        var course = $filter('filter')(vm.courses, { TeamId: item.TeamId }, true)[0];
                        vm.employmentTeams.push({ TeamId: team.TeamId, Name: team.Name, Hex: team.Hex });
                    });
                    return vm.employmentTeams;
                });
        }
    }
})();
