(function () {
    'use strict';

    angular
        .module('moment-module', [])
        .factory('moment', moment);

    moment.$inject = ['$window'];

    function moment($window) {
        return $window.moment;
    }
})();