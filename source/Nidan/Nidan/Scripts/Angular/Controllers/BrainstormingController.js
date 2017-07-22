(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BrainstormingController', BrainstormingController);

    BrainstormingController.$inject = ['$window', 'BrainstormingService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BrainstormingController($window, BrainstormingService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.brainstormings = [];
        vm.DisscussionCompletedYesNo = [];
        vm.RefernceDetailDocument = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBrainstorming = editBrainstorming;
        vm.createEventBrainstorming = createEventBrainstorming;
        initialise();

        function initialise() {
            order("BrainstormingId");
        }

        function retrieveBrainstormings() {
            return BrainstormingService.retrieveBrainstormings(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.brainstormings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.brainstormings;
                });
        }

        function pageChanged() {
            return retrieveBrainstormings();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBrainstormings();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editBrainstorming(id) {
            $window.location.href = "/Brainstorming/Edit/" + id;
        }

        function createEventBrainstorming() {
            var eventId = $("#Event_EventId").val();
            return BrainstormingService.createEventBrainstorming(eventId, vm.eventBrainstormings).then(function (response) {
                vm.eventBrainstormings = response.data.Items;
                return vm.eventBrainstormings;
            });
        }
    }

})();
