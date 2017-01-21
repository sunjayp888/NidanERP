(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('EventController', EventController);

    EventController.$inject = ['$window', 'EventService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function EventController($window, EventService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.events = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editEvent = editEvent;
        //vm.canDeleteAbsenceType = canDeleteAbsenceType;
        //vm.deleteAbsenceType = deleteAbsenceType;
        vm.retrieveEvents = retrieveEvents,
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveEvents() {
            return EventService.retrieveEvents(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.events = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.events;
                });
        }

        function pageChanged() {
            return retrieveEvents();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEvents();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editEvent(id) {
            $window.location.href = "/Event/Edit/" + id;
        }

        function canDeleteAbsenceType(id) {
            vm.loadingActions = true;
            vm.CanDeleteAbsenceType = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            EventService.canDeleteAbsenceType(id).then(function (response) { vm.CanDeleteAbsenceType = response.data, vm.loadingActions = false });
        }

        function deleteAbsenceType(id) {
            return EventService.deleteAbsenceType(id).then(function () { initialise(); });
        };

    }

})();
