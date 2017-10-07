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
        vm.brainStormingQuestions = [];
        vm.DisscussionCompletedYesNo = [];
        vm.RefernceDetailDocument = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editBrainstorming = editBrainstorming;
        vm.createEventBrainstorming = createEventBrainstorming;
        vm.eventId;
        vm.saveEventBrainStorming = saveEventBrainStorming;
        vm.initialise = initialise;

        function initialise(eventId) {
            vm.eventId = eventId;
            order("BrainstormingId");
        }

        function retrieveBrainstormings() {
            return BrainstormingService.retrieveBrainstormings(vm.eventId)
                .then(function (response) {
                    vm.brainstormings = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.brainstormings;
                });
        }

        function retrieveBrainstormingQuestions() {
            return BrainstormingService.retrieveBrainstormingQuestions(vm.eventId)
                .then(function (response) {
                    vm.brainStormingQuestions = response.data.Items;
                    return vm.brainStormingQuestions;
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

        function saveEventBrainStorming() {
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
                var eventBrainStormingList = [];
                retrieveBrainstormingQuestions().then(function (response) {
                    $.each(vm.brainStormingQuestions, function (i) {
                        var brainStormingDisscussionId = 'brainStormingDisscussion' + vm.brainStormingQuestions[i].BrainstormingId;
                        var referenceDetailTextBoxId = 'referenceDetailTextBox' + vm.brainStormingQuestions[i].BrainstormingId;
                        eventBrainStormingList.push({
                            EventId: vm.eventId,
                            BrainstormingId: vm.brainStormingQuestions[i].BrainstormingId,
                            DisscussionCompletedYesNo: $(":radio[name=" + brainStormingDisscussionId + "]:checked").val(),
                            ReferenceDetailDocument: $('#' + referenceDetailTextBoxId).val()
                        });
                    });
                    BrainstormingService.createEventBrainstorming(vm.eventId, eventBrainStormingList).then(
                        function (response) {
                            if (response.data) {
                                alert(response.data);
                                var index = $('#tabs a[href="#tab_content_planning"]').parent().index();
                                $('#tabs').tabs('select', index);
                            };
                        });
                });
            } else {
                alert("Please reply for all question and save.");
            }
        };

        //function saveEventBrainStormingData() {
        //    var brainStormingDisscussion1 = $(":radio[name='brainStormingDisscussion1']:checked").val();
        //    var brainStormingDisscussion2 = $(":radio[name='brainStormingDisscussion2']:checked").val();
        //    var brainStormingDisscussion3 = $(":radio[name='brainStormingDisscussion3']:checked").val();
        //    var brainStormingDisscussion4 = $(":radio[name='brainStormingDisscussion4']:checked").val();
        //    var brainStormingDisscussion5 = $(":radio[name='brainStormingDisscussion5']:checked").val();
        //    var brainStormingDisscussion6 = $(":radio[name='brainStormingDisscussion6']:checked").val();
        //    var brainStormingDisscussion7 = $(":radio[name='brainStormingDisscussion7']:checked").val();
        //    var brainStormingDisscussion8 = $(":radio[name='brainStormingDisscussion8']:checked").val();
        //    var brainStormingDisscussion9 = $(":radio[name='brainStormingDisscussion9']:checked").val();
        //    var brainStormingDisscussion10 = $(":radio[name='brainStormingDisscussion10']:checked").val();
        //    var brainStormingDisscussion11 = $(":radio[name='brainStormingDisscussion11']:checked").val();
        //    var brainStormingDisscussion12 = $(":radio[name='brainStormingDisscussion12']:checked").val();
        //    var brainStormingDisscussion13 = $(":radio[name='brainStormingDisscussion13']:checked").val();
        //    var brainStormingDisscussion14 = $(":radio[name='brainStormingDisscussion14']:checked").val();
        //    var brainStormingDisscussion15 = $(":radio[name='brainStormingDisscussion15']:checked").val();
        //    var brainStormingDisscussion16 = $(":radio[name='brainStormingDisscussion16']:checked").val();
        //    var brainStormingDisscussion17 = $(":radio[name='brainStormingDisscussion17']:checked").val();
        //    var brainStormingDisscussion18 = $(":radio[name='brainStormingDisscussion18']:checked").val();
        //    var brainStormingDisscussion19 = $(":radio[name='brainStormingDisscussion19']:checked").val();
        //    var brainStormingReferenceDetail1 = $("referenceDetailTextBox1").val();
        //    var brainStormingReferenceDetail2 = $("referenceDetailTextBox2").val();
        //    var brainStormingReferenceDetail3 = $("referenceDetailTextBox3").val();
        //    var brainStormingReferenceDetail4 = $("referenceDetailTextBox4").val();
        //    var brainStormingReferenceDetail5 = $("referenceDetailTextBox5").val();
        //    var brainStormingReferenceDetail6 = $("referenceDetailTextBox6").val();
        //    var brainStormingReferenceDetail7 = $("referenceDetailTextBox7").val();
        //    var brainStormingReferenceDetail8 = $("referenceDetailTextBox8").val();
        //    var brainStormingReferenceDetail9 = $("referenceDetailTextBox9").val();
        //    var brainStormingReferenceDetail10 = $("referenceDetailTextBox10").val();
        //    var brainStormingReferenceDetail11 = $("referenceDetailTextBox11").val();
        //    var brainStormingReferenceDetail12 = $("referenceDetailTextBox12").val();
        //    var brainStormingReferenceDetail13 = $("referenceDetailTextBox13").val();
        //    var brainStormingReferenceDetail14 = $("referenceDetailTextBox14").val();
        //    var brainStormingReferenceDetail15 = $("referenceDetailTextBox15").val();
        //    var brainStormingReferenceDetail16 = $("referenceDetailTextBox16").val();
        //    var brainStormingReferenceDetail17 = $("referenceDetailTextBox17").val();
        //    var brainStormingReferenceDetail18 = $("referenceDetailTextBox18").val();
        //    var brainStormingReferenceDetail19 = $("referenceDetailTextBox19").val();
        //    //create object 

        //}
    }

})();
