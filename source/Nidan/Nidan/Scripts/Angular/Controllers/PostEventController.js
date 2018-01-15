(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PostEventController', PostEventController);

    PostEventController.$inject = ['$window', 'PostEventService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function PostEventController($window, PostEventService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.postEvents = [];
        vm.postEventQuestions = [];
        vm.DisscussionCompletedYesNo = [];
        vm.RefernceDetailDocument = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editPostEvent = editPostEvent;
        vm.createPostEvent = createPostEvent;
        vm.eventId;
        vm.savePostEvent = savePostEvent;
        vm.initialise = initialise;

        function initialise(eventId) {
            vm.eventId = eventId;
            order("PostEventId");
        }

        function retrievePostEvents() {
            return PostEventService.retrievePostEvents(vm.eventId)
                .then(function (response) {
                    vm.postEvents = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.postEvents;
                });
        }

        function retrievePostEventQuestions() {
            return PostEventService.retrievePostEventQuestions(vm.eventId)
                .then(function (response) {
                    vm.postEventQuestions = response.data.Items;
                    return vm.postEventQuestions;
                });
        }

        function pageChanged() {
            return retrievePostEvents();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrievePostEvents();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editPostEvent(id) {
            $window.location.href = "/PostEvent/Edit/" + id;
        }

        function createPostEvent() {
            var eventId = $("#Event_EventId").val();
            return PostEventService.createPostEvent(eventId, vm.postEvents).then(function (response) {
                vm.postEvents = response.data.Items;
                return vm.postEvents;
            });
        }

        function savePostEvent() {
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
                var postEventList = [];
                $.each(vm.postEvents, function (i) {
                    var postEventDisscussionId = 'postEventDisscussion' + vm.postEvents[i].EventManagementId;
                    var referenceDetailTextBoxId = 'referenceDetailTextBox' + vm.postEvents[i].EventManagementId;
                    postEventList.push({
                        EventId: vm.eventId,
                        EventManagementId: vm.postEvents[i].EventManagementId,
                        EventQuestionAnswerCompleted: $(":radio[name=" + postEventDisscussionId + "]:checked").val(),
                        Description: $('#' + referenceDetailTextBoxId).val(),
                        CentreId: vm.postEvents[i].CentreId
                    });
                });
                PostEventService.updatePostEvent(vm.eventId, postEventList).then(
                    function (response) {
                        if (response.data) {
                            //var index = $('#tabs a[href="#tab_content_planning"]').parent().index();
                            //$('#tabs').tabs('select', index);
                            retrievePostEvents();
                        };
                    });
            } else {
                alert("Please reply for all question and save.");
            }
        };
    }
})();
