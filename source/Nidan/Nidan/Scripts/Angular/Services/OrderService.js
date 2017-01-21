(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OrderService', OrderService);
        
    OrderService.$inject = ['Order'];

    function OrderService(Order) {

        var service = {            
            order: order,
            orderClass: orderClass            
        };

        return service;

        function order(orderBy, property) {
            var direction = orderBy.property === property && orderBy.direction === Order.Direction.Ascending.name
                ? Order.Direction.Descending
                : Order.Direction.Ascending;

            orderBy.direction = direction.name;
            orderBy.class = direction.class;
            orderBy.property = property;

            return orderBy;
        }

        function orderClass(orderBy, property) {
            if (orderBy.property !== property)
                return 'sorting';
            else
                return 'sorting_' + orderBy.class;
        }
    }
})();