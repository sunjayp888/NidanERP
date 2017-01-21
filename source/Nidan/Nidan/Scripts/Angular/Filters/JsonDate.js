(function () {
    'use strict';

    angular
        .module('Nidan')
        .filter('JsonDate', JsonDate);
    
    function JsonDate() {
        return JsonDateFilter;

        function JsonDateFilter(input) {
            return new Date(parseInt(input.substr(6)));
        }
    }
})();