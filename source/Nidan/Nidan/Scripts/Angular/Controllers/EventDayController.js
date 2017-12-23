(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('EventDayController', EventDayController);

    EventDayController.$inject = ['$window', 'EventDayService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function EventDayController($window, EventDayService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.eventDays = [];
        vm.eventDayQuestions = [];
        vm.DisscussionCompletedYesNo = [];
        vm.RefernceDetailDocument = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editEventDay = editEventDay;
        vm.createEventDay = createEventDay;
        vm.eventId;
        vm.saveEventDay = saveEventDay;
        vm.initialise = initialise;

        function initialise(eventId) {
            vm.eventId = eventId;
            order("EventDayId");
        }

        function retrieveEventDays() {
            return EventDayService.retrieveEventDays(vm.eventId)
                .then(function (response) {
                    vm.eventDays = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.eventDays;
                });
        }

        function retrieveEventDayQuestions() {
            return EventDayService.retrieveEventDayQuestions(vm.eventId)
                .then(function (response) {
                    vm.eventDayQuestions = response.data.Items;
                    return vm.eventDayQuestions;
                });
        }

        function pageChanged() {
            return retrieveEventDays();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEventDays();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editEventDay(id) {
            $window.location.href = "/EventDay/Edit/" + id;
        }

        function createEventDay() {
            var eventId = $("#Event_EventId").val();
            return EventDayService.createEventEventDay(eventId, vm.eventDays).then(function (response) {
                vm.eventDays = response.data.Items;
                return vm.eventDays;
            });
        }

        function saveEventDay() {
            var blank = false;
            $("input:radio").each(function () {
                var val = $('input:radio[name=' + this.name + ']:checked').val();
                if (val === undefined) {
                    blank = true;
                    return false;
                }
            });
            //alert(blank ? "At least one group is blank" : "All groups are checked");
            if (!blank) {
                var eventDayList = [];
                $.each(vm.eventDays, function (i) {
                    var eventDayDisscussionId = 'eventDayDisscussion' + vm.eventDays[i].EventManagementId;
                    var referenceDetailTextBoxId = 'referenceDetailTextBox' + vm.eventDays[i].EventManagementId;
                    eventDayList.push({
                        EventId: vm.eventId,
                        EventManagementId: vm.eventDays[i].EventManagementId,
                        EventQuestionAnswerCompleted: $(":radio[name=" + eventDayDisscussionId + "]:checked").val(),
                        Description: $('#' + referenceDetailTextBoxId).val(),
                        CentreId: vm.eventDays[i].CentreId
                    });
                });
                EventDayService.updateEventDay(vm.eventId, eventDayList).then(
                    function (response) {
                        if (response.data) {
                            //var index = $('#tabs a[href="#tab_content_planning"]').parent().index();
                            //$('#tabs').tabs('select', index);
                            retrieveEventDays();
                        };
                    });
            } else {
                alert("Please reply for all question and save.");
            }
        };
    }
})();
