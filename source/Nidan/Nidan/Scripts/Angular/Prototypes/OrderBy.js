(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OrderBy', OrderBy)
        .constant('Order', {
            Direction: {
                Ascending: { name: 'Ascending', class: 'asc' },
                Descending: { name: 'Descending', class: 'desc' }
            }
        });

    function OrderBy() {

        function OrderBy() {
            this.property = '';
            this.direction = '';
            this.class = '';
        }

        return OrderBy;
    }

})();