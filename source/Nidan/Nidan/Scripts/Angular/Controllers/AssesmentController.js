(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AssesmentController', AssesmentController);

    AssesmentController.$inject = ['$window', 'AssesmentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AssesmentController($window, AssesmentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.assesments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAssesment = editAssesment;
        vm.searchAssesment = searchAssesment;
        vm.viewAssesment = viewAssesment;
        vm.retrieveAssesments = retrieveAssesments;
        vm.retrieveCandidateAssesmentByBatchId = retrieveCandidateAssesmentByBatchId;
        vm.openModuleExamSetModalPopUp = openModuleExamSetModalPopUp;
        vm.openModuleExamSetByAssesmentId = openModuleExamSetByAssesmentId;
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        vm.subjects = [];
        vm.retrieveSubjects = retrieveSubjects;
        vm.moduleExamSets = [];
        vm.retrieveModuleExamSets = retrieveModuleExamSets;
        vm.batches = [];
        vm.batchId;
        vm.retrieveBatches = retrieveBatches;
        vm.assignAssignModuleExamSet = assignAssignModuleExamSet;
        vm.updateModuleExamSet = updateModuleExamSet;
        vm.isAssignButtonEnable = true;
        vm.canWeAssign = canWeAssign;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.assesmentId;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "AssesmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("AssesmentId");
        }

        function retrieveAssesments() {
            vm.orderBy.property = "AssesmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return AssesmentService.retrieveAssesments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assesments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.assesments;
                });
        }

        function searchAssesment(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return AssesmentService.searchAssesment(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assesments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.assesments.length === 0 ? "No Records Found" : "";
                    return vm.assesments;
                });
        }

        function retrieveCandidateAssesmentByBatchId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveSubjects();
            retrieveModuleExamSets();
            return AssesmentService.retrieveCandidateAssesmentByBatchId(vm.batchId,vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assesments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.assesments;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchAssesment(vm.searchKeyword);
            }
            else {
                return retrieveAssesments();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAssesments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAssesment(id) {
            $window.location.href = "/Assesment/Edit/" + id;
        }

        function viewAssesment(assesmentId) {
            $window.location.href = "/Assesment/View/" + assesmentId;
        }

        function retrieveCourses(centreId) {
            return AssesmentService.retrieveCourses(centreId).then(function () {
                vm.courses = response.data;
            });
        };

        function retrieveBatches(courseId, centreId) {
            return AssesmentService.retrieveBatches(courseId, centreId).then(function () {
                vm.batches = response.data;
            });
        };

        function retrieveSubjects() {
            return AssesmentService.retrieveSubjects()
                .then(function (response) {
                    vm.subjects = response.data;
                    return vm.subjects;
                });
        }

        function retrieveModuleExamSets() {
            return AssesmentService.retrieveModuleExamSets()
                .then(function (response) {
                    vm.moduleExamSets = response.data;
                    return vm.moduleExamSets;
                });
        }

        function openModuleExamSetByAssesmentId(assesmentId) {
            vm.assesmentId = assesmentId;
            return AssesmentService.openModuleExamSetByAssesmentId(vm.assesmentId, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropStatus').val("Select Assign Type");
                    $('#dropStatus').val();
                    vm.fixAssetMappings = response.data;
                    return vm.fixAssetMappings;
                });
        }

        function openModuleExamSetModalPopUp(assesments) {
            vm.assesments = assesments;
            return AssesmentService.retrieveCandidateAssesmentList(vm.assesments, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropSubject').val("Select Module");
                    $('#dropSubject').val();
                    $('#dropModuleSet').val("Select Module Exam Set");
                    $('#dropModuleSet').val();
                    vm.assesments = response.data;
                    return vm.assesments;
                });
        }

        function assignAssignModuleExamSet() {
            for (var i = 0; i < vm.assesments.length; i++) {
                vm.assesments[i].SubjectId = $("#dropSubject").val();
                vm.assesments[i].ModuleExamSetId = $("#dropModuleSet").val();
            }
            return AssesmentService.assignAssignModuleExamSet(vm.assesments).then(function () {
                retrieveCandidateAssesmentByBatchId(vm.batchId);
            });
        }

        function updateModuleExamSet() {
            var assesment = {
                AssesmentId: vm.assesmentId,
                SubjectId: $('#dropSubject').val(),
                ModuleSetId: $('#dropModuleSet').val()
            }
            return AssesmentService.updateModuleExamSet(assesment)
                .then(function (response) {
                    retrieveCandidateAssesmentByBatchId(vm.batchId);
                });
        }

        function canWeAssign() {
            var count = 0;
            angular.forEach(vm.assesments,
                function (value, key) {
                    if (value.Ischecked) {
                        count++;
                    }
                });
            vm.isAssignButtonEnable = (count === 0);
        }

    }

})();
