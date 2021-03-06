﻿(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AssessmentService', AssessmentService);

    AssessmentService.$inject = ['$http'];

    function AssessmentService($http) {
        var service = {
            retrieveAssessments: retrieveAssessments,
            searchAssessment: searchAssessment,
            retrieveCandidateAssessmentByBatchId: retrieveCandidateAssessmentByBatchId,
            retrieveCandidateAssessmentList: retrieveCandidateAssessmentList,
            assignAssignModuleExamSet: assignAssignModuleExamSet,
            retrieveSubjects: retrieveSubjects,
            retrieveModuleExamSets: retrieveModuleExamSets,
            updateModuleExamSet: updateModuleExamSet,
            openModuleExamSetByAssessmentId: openModuleExamSetByAssessmentId,
            retrieveCandidateAssessment: retrieveCandidateAssessment,
            createCandidateAssessmentQuestionAnswer: createCandidateAssessmentQuestionAnswer,
            retrieveCandidateAssessmentDetailByBatchIdAssessmentId: retrieveCandidateAssessmentDetailByBatchIdAssessmentId,
            retrieveCandidateAssessmentQuestionAnswer: retrieveCandidateAssessmentQuestionAnswer,
            retrieveCandidateAttemptedAssessment: retrieveCandidateAttemptedAssessment,
            updateCandidateAssessmentQuestionAnswer: updateCandidateAssessmentQuestionAnswer,
            updateCandidateAssessmentTotalMarkObtained: updateCandidateAssessmentTotalMarkObtained
        };

        return service;

        function retrieveAssessments(Paging, OrderBy) {

            var url = "/Assessment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAssessment(SearchKeyword, Paging, OrderBy) {
            var url = "/Assessment/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssessmentByBatchId(batchId, Paging, OrderBy) {
            var url = "/Assessment/CandidateAssessmentByBatchId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssessmentList(assessments, Paging, OrderBy) {

            var url = "/Assessment/CandidateAssessmentCheckedList",
                data = {
                    assessments: assessments,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function assignAssignModuleExamSet(assessments) {
            var url = "/Assessment/AssignModuleExamSet",
                data = {
                    assessments: assessments
                };
            return $http.post(url, data);
        }

        function retrieveSubjects() {

            var url = "/Assessment/Subject";
            return $http.post(url);
        }

        function retrieveModuleExamSets() {

            var url = "/Assessment/ModuleExamSet";
            return $http.post(url);
        }

        function updateModuleExamSet(candidateAssessment) {
            var url = "/Assessment/UpdateCandidateAssessment",
                data = {
                    candidateAssessment: candidateAssessment
                };
            return $http.post(url, data);
        }

        function openModuleExamSetByAssessmentId(candidateassessmentId, Paging, OrderBy) {

            var url = "/Assessment/ModuleExamSetByAssessmentId",
                data = {
                    candidateassessmentId: candidateassessmentId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssessment(candidateAssessmentId, Paging, OrderBy) {

            var url = "/CandidateAssessmentQuestionAnswer/CandidateAssessmentQuestionAnswerList",
                data = {
                    candidateAssessmentId:candidateAssessmentId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function createCandidateAssessmentQuestionAnswer(candidateAssessment) {

            var url = "/CandidateAssessmentQuestionAnswer/CandidateAssessmentQuestionAnswer",
                data = {
                    candidateAssessment: candidateAssessment
                    //paging: Paging,
                    //orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function updateCandidateAssessmentQuestionAnswer(candidateAssessment) {

            var url = "/CandidateAssessmentQuestionAnswer/UpdateCandidateAssessmentQuestionAnswer",
                data = {
                    candidateAssessment: candidateAssessment
                    //paging: Paging,
                    //orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssessmentDetailByBatchIdAssessmentId(batchId, Paging, OrderBy) {

            var url = "/CandidateAssessmentQuestionAnswer/CandidateAssessmentDetailByBatchIdAssessmentId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAttemptedAssessment(candidateAssessmentId, Paging, OrderBy) {

            var url = "/CandidateAssessmentQuestionAnswer/CandidateAttemptedAssessmentList",
                data = {
                    candidateAssessmentId: candidateAssessmentId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function updateCandidateAssessmentTotalMarkObtained(candidateAssessmentId) {

            var url = "/CandidateAssessmentQuestionAnswer/UpdateCandidateAssessmentTotalMarkObtained",
                data = {
                    candidateAssessmentId: candidateAssessmentId
                    //paging: Paging,
                    //orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssessmentQuestionAnswer(candidateAssessmentId, moduleExamQuestionSetId, Paging, OrderBy) {
            var url = "/CandidateAssessmentQuestionAnswer/CandidateAssessmentQuestionAnswerbyId",
                data = {
                    candidateAssessmentId: candidateAssessmentId,
                    moduleExamQuestionSetId:moduleExamQuestionSetId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();