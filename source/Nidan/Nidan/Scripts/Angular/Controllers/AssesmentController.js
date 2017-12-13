(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AssessmentController', AssessmentController);

    AssessmentController.$inject = ['$window', 'AssessmentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AssessmentController($window, AssessmentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.assessments = [];
        vm.candidateAssessments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAssessment = editAssessment;
        vm.searchAssessment = searchAssessment;
        vm.viewAssessment = viewAssessment;
        vm.retrieveAssessments = retrieveAssessments;
        vm.retrieveCandidateAssessmentByBatchId = retrieveCandidateAssessmentByBatchId;
        vm.openModuleExamSetModalPopUp = openModuleExamSetModalPopUp;
        vm.openModuleExamSetByAssessmentId = openModuleExamSetByAssessmentId;
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        vm.subjects = [];
        vm.retrieveSubjects = retrieveSubjects;
        vm.moduleExamSets = [];
        vm.retrieveModuleExamSets = retrieveModuleExamSets;
        vm.batches = [];
        vm.batchId;
        vm.candidateAssessmentId;
        vm.retrieveBatches = retrieveBatches;
        vm.assignAssignModuleExamSet = assignAssignModuleExamSet;
        vm.updateModuleExamSet = updateModuleExamSet;
        vm.isAssignButtonEnable = true;
        vm.canWeAssign = canWeAssign;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.assessmentId;
        vm.initialise = initialise;
        vm.retrieveCandidateAssessment = retrieveCandidateAssessment;
        vm.createCandidateAssessmentQuestionAnswer = createCandidateAssessmentQuestionAnswer;
        vm.IsOptionA;
        vm.IsOptionB;
        vm.IsOptionC;
        vm.IsOptionD;
        vm.SubjectiveAnswer;
        vm.retrieveCandidateAssessmentDetailByBatchIdAssessmentId = retrieveCandidateAssessmentDetailByBatchIdAssessmentId;
        vm.batchId;
        vm.assessmentId;
        vm.retrieveCandidateAssessmentQuestionAnswer = retrieveCandidateAssessmentQuestionAnswer;
        vm.candidateAssessmentQuestionAnswerId;
        vm.retrieveCandidateAttemptedAssessment = retrieveCandidateAttemptedAssessment;
        vm.updateCandidateAssessmentQuestionAnswer = updateCandidateAssessmentQuestionAnswer;
        vm.candidateAssessmentQuestionAnswerId;
        vm.updateCandidateAssessmentTotalMarkObtained = updateCandidateAssessmentTotalMarkObtained;

        function initialise() {
            vm.orderBy.property = "AssessmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("AssessmentId");
        }

        function retrieveAssessments() {
            vm.orderBy.property = "AssessmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return AssessmentService.retrieveAssessments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.assessments;
                });
        }

        function searchAssessment(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return AssessmentService.searchAssessment(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.assessments.length === 0 ? "No Records Found" : "";
                    return vm.assessments;
                });
        }

        function retrieveCandidateAssessmentByBatchId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            retrieveSubjects();
            retrieveModuleExamSets();
            return AssessmentService.retrieveCandidateAssessmentByBatchId(vm.batchId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.assessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.assessments;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchAssessment(vm.searchKeyword);
            }
            var path = window.location.pathname.split('/');
            if (path[2] === "CandidateAssessmentQuestionAnswerSet") {
                retrieveCandidateAssessment(vm.candidateAssessmentId);
            }
            if (path[2] === "CandidateAttemptedQuestionAnswer") {
                retrieveCandidateAttemptedAssessment(vm.candidateAssessmentId);
            }
            else {
                return retrieveAssessments();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAssessments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAssessment(id) {
            $window.location.href = "/Assessment/Edit/" + id;
        }

        function viewAssessment(assessmentId) {
            $window.location.href = "/Assessment/View/" + assessmentId;
        }

        function retrieveCourses(centreId) {
            return AssessmentService.retrieveCourses(centreId).then(function () {
                vm.courses = response.data;
            });
        };

        function retrieveBatches(courseId, centreId) {
            return AssessmentService.retrieveBatches(courseId, centreId).then(function () {
                vm.batches = response.data;
            });
        };

        function retrieveSubjects() {
            return AssessmentService.retrieveSubjects()
                .then(function (response) {
                    vm.subjects = response.data;
                    return vm.subjects;
                });
        }

        function retrieveModuleExamSets() {
            return AssessmentService.retrieveModuleExamSets()
                .then(function (response) {
                    vm.moduleExamSets = response.data;
                    return vm.moduleExamSets;
                });
        }

        function openModuleExamSetByAssessmentId(candidateassessmentId) {
            vm.candidateassessmentId = candidateassessmentId;
            return AssessmentService.openModuleExamSetByAssessmentId(vm.candidateassessmentId, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropStatus').val("Select Assign Type");
                    $('#dropStatus').val();
                    $('#dropModuleSet').val("Select Module Exam Set");
                    $('#dropModuleSet').val();
                    vm.assessments = response.data;
                    return vm.assessments;
                });
        }

        function openModuleExamSetModalPopUp(assessments) {
            vm.assessments = assessments;
            return AssessmentService.retrieveCandidateAssessmentList(vm.assessments, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#dropSubject').val("Select Module");
                    $('#dropSubject').val();
                    $('#dropModuleSet').val("Select Module Exam Set");
                    $('#dropModuleSet').val();
                    vm.assessments = response.data;
                    return vm.assessments;
                });
        }

        function assignAssignModuleExamSet() {
            for (var i = 0; i < vm.assessments.length; i++) {
                vm.assessments[i].SubjectId = $("#dropSubject").val();
                vm.assessments[i].ModuleExamSetId = $("#dropModuleSet").val();
            }
            return AssessmentService.assignAssignModuleExamSet(vm.assessments).then(function () {
                retrieveCandidateAssessmentByBatchId(vm.batchId);
            });
        }

        function updateModuleExamSet() {
            var candidateAssessment = {
                CandidateassessmentId: vm.candidateassessmentId,
                SubjectId: $('#dropSubject').val(),
                ModuleSetId: $('#dropModuleSet').val()
            }
            return AssessmentService.updateModuleExamSet(candidateAssessment)
                .then(function (response) {
                    retrieveCandidateAssessmentByBatchId(vm.batchId);
                });
        }

        function canWeAssign() {
            var count = 0;
            angular.forEach(vm.assessments,
                function (value, key) {
                    if (value.Ischecked) {
                        count++;
                    }
                });
            vm.isAssignButtonEnable = (count === 0);
        }

        function retrieveCandidateAssessment(candidateAssessmentId) {
            vm.candidateAssessmentId = candidateAssessmentId;
            vm.orderBy.property = "CandidateAssessmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.paging.pageSize = 1;
            return AssessmentService.retrieveCandidateAssessment(vm.candidateAssessmentId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateAssessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateAssessments;
                });
        }

        //createCandidateAssessmentQuestionAnswer
        function createCandidateAssessmentQuestionAnswer(optionA, optionB, optionC, optionD, subjectAnswer, candidateAssessmentId, assessmentId, moduleExamSetId, moduleExamQuestionSetId, questionTypeId, answerType, markPerQuestion) {
            var optionAanswer = $('#' + optionA).is(":checked");
            var optionBanswer = $('#' + optionB).is(":checked");
            var optionCanswer = $('#' + optionC).is(":checked");
            var optionDanswer = $('#' + optionD).is(":checked");
            var subjectivequestionanswer = $('#' + subjectAnswer).val();
            var candidateAssessment = {
                CandidateAssessmentId: candidateAssessmentId,
                ModuleExamSetId: moduleExamSetId,
                ModuleExamQuestionSetId: moduleExamQuestionSetId,
                AssessmentId: assessmentId,
                QuestionTypeId: questionTypeId,
                AnswerType: answerType,
                MarkPerQuestion: markPerQuestion,
                IsOptionA: optionAanswer,
                IsOptionB: optionBanswer,
                IsOptionC: optionCanswer,
                IsOptionD: optionDanswer,
                SubjectiveAnswer: subjectivequestionanswer
            }
            return AssessmentService.createCandidateAssessmentQuestionAnswer(candidateAssessment)
                .then(function (response) {
                    retrieveCandidateAssessmentQuestionAnswer(vm.candidateAssessmentId, vm.ModuleExamQuestionSetId);
                });
        }

        function updateCandidateAssessmentQuestionAnswer(markObtained, candidateAssessmentQuestionAnswerId) {
            var candidateMarkObtained = $('#' + markObtained).val();
            vm.candidateAssessmentQuestionAnswerId = candidateAssessmentQuestionAnswerId;
            var candidateAssessment = {
                CandidateAssessmentQuestionAnswerId:vm.candidateAssessmentQuestionAnswerId,
                MarkObtained: candidateMarkObtained
            }
            return AssessmentService.updateCandidateAssessmentQuestionAnswer(candidateAssessment)
                .then(function (response) {
                    retrieveCandidateAssessmentQuestionAnswer(vm.candidateAssessmentId, vm.ModuleExamQuestionSetId);
                });
        }

        function retrieveCandidateAssessmentDetailByBatchIdAssessmentId(batchId) {
            vm.batchId = batchId;
            vm.orderBy.property = "CandidateAssessmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return AssessmentService.retrieveCandidateAssessmentDetailByBatchIdAssessmentId(vm.batchId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateAssessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateAssessments;
                });
        }

        //retrieveCandidateAttemptedAssessment
        function retrieveCandidateAttemptedAssessment(candidateAssessmentId) {
            vm.candidateAssessmentId = candidateAssessmentId;
            vm.orderBy.property = "CandidateAssessmentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.paging.pageSize = 1;
            return AssessmentService.retrieveCandidateAttemptedAssessment(vm.candidateAssessmentId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateAssessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateAssessments;
                });
        }

        //updateCandidateAssessmentTotalMarkObtained
        function updateCandidateAssessmentTotalMarkObtained(candidateAssessmentId) {
            vm.candidateAssessmentId = candidateAssessmentId;
            return AssessmentService.updateCandidateAssessmentTotalMarkObtained(vm.candidateAssessmentId)
                .then(function (response) {
                    retrieveCandidateAssessmentDetailByBatchIdAssessmentId(vm.batchId);
                });
        }

        function retrieveCandidateAssessmentQuestionAnswer(candidateAssessmentId,moduleExamQuestionSetId) {
            vm.candidateAssessmentId = candidateAssessmentId;
            vm.moduleExamQuestionSetId = moduleExamQuestionSetId;
            vm.orderBy.property = "candidateAssessmentQuestionAnswerId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return AssessmentService.retrieveCandidateAssessmentQuestionAnswer(vm.candidateAssessmentId, vm.moduleExamQuestionSetId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateAssessments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateAssessments;
                });
        }
    }

})();
