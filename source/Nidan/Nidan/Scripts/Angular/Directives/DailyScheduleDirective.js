(function () {
    'use strict';

    angular
        .module('Nidan')
        .directive('dailySchedule', dailySchedule);
    
    function dailySchedule() {
        // Usage:
        //     <daily-schedule></daily-schedule>
        // Creates:

        var directive = {            
            templateUrl: '/Scripts/Angular/Directives/Views/_schedule.html',
            restrict: 'E',
            scope: {},
            controller: 'dailyScheduleController',
            controllerAs: 'vm',            
            bindToController: {
                beginDate: '=',
                items: '=',                
                dateChanged: '=',
                itemUrl: '@',
                id: '='               
            },
            transclude: true,
            link: setTransscope
        };

        return directive;


        function setTransscope($scope, $element) {
            $element.data('transscope', $scope.$parent.$new());
        };

    }
    
})();