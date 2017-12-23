(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PlanningController', PlanningController);

    PlanningController.$inject = ['$window', 'PlanningService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function PlanningController($window, PlanningService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.plannings = [];
        vm.planningQuestions = [];
        vm.DisscussionCompletedYesNo = [];
        vm.RefernceDetailDocument = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editPlanning = editPlanning;
        vm.createEventPlanning = createEventPlanning;
        vm.eventId;
        vm.saveEventPlanning = saveEventPlanning;
        vm.initialise = initialise;

        function initialise(eventId) {
            vm.eventId = eventId;
            order("PlanningId");
        }

        function retrievePlannings() {
            return PlanningService.retrievePlannings(vm.eventId)
                .then(function (response) {
                    vm.plannings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.plannings;
                });
        }

        function retrievePlanningQuestions() {
            return PlanningService.retrievePlanningQuestions(vm.eventId)
                .then(function (response) {
                    vm.planningQuestions = response.data.Items;
                    return vm.planningQuestions;
                });
        }

        function pageChanged() {
            return retrievePlannings();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrievePlannings();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editPlanning(id) {
            $window.location.href = "/Planning/Edit/" + id;
        }

        function createEventPlanning() {
            var eventId = $("#Event_EventId").val();
            return PlanningService.createEventPlanning(eventId, vm.eventPlannings).then(function (response) {
                vm.eventPlannings = response.data.Items;
                return vm.eventPlannings;
            });
        }

        function saveEventPlanning() {
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
                var eventPlanningList = [];
                $.each(vm.plannings, function (i) {
                    var planningDisscussionId = 'planningDisscussion' + vm.plannings[i].EventManagementId;
                    var referenceDetailTextBoxId = 'referenceDetailTextBox' + vm.plannings[i].EventManagementId;
                    eventPlanningList.push({
                        EventId: vm.eventId,
                        EventManagementId: vm.plannings[i].EventManagementId,
                        EventQuestionAnswerCompleted: $(":radio[name=" + planningDisscussionId + "]:checked").val(),
                        Description: $('#' + referenceDetailTextBoxId).val(),
                        CentreId: vm.plannings[i].CentreId
                    });
                });
                PlanningService.updateEventPlanning(vm.eventId, eventPlanningList).then(
                    function (response) {
                        if (response.data) {
                            //var index = $('#tabs a[href="#tab_content_planning"]').parent().index();
                            //$('#tabs').tabs('select', index);
                            retrievePlannings();
                        };
                    });
            } else {
                alert("Please reply for all question and save.");
            }
        };
    }
})();
