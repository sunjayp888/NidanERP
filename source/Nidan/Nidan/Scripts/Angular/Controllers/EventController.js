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
        vm.canDeleteEvent = canDeleteEvent;
        vm.deleteEvent = deleteEvent;
        vm.searchEvent = searchEvent;
        vm.viewEvent = viewEvent;
        vm.retrieveQuestions = retrieveQuestions;
        vm.searchKeyword = "";
        vm.searchMessage = "";
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

        function searchEvent(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return EventService.searchEvent(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.events = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.events.length === 0 ? "No Records Found" : "";
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

        function canDeleteEvent(id) {
            vm.loadingActions = true;
            vm.CanDeleteEvent = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            EventService.canDeleteEvent(id).then(function (response) { vm.CanDeleteEvent = response.data, vm.loadingActions = false });
        }

        function deleteEvent(id) {
            return EventService.deleteEvent(id).then(function () { initialise(); });
        };

        function viewEvent(eventId) {
            $window.location.href = "/Event/Edit/" + eventId;
        }

        //function retrieveQuestions(eventFunctionId) {
        //    return EventService.retrieveQuestions(eventFunctionId).then(function () {
        //        vm.questions = response.data;
        //    });
        //};

        function retrieveQuestions(eventFunctionId) {
            vm.eventFunctionId = eventFunctionId;
            return EventService.retrieveQuestions(vm.eventFunctionId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.questions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.questions;
                });
        }

    }

})();
