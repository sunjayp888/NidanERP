(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('AssesmentService', AssesmentService);

    AssesmentService.$inject = ['$http'];

    function AssesmentService($http) {
        var service = {
            retrieveAssesments: retrieveAssesments,
            searchAssesment: searchAssesment,
            retrieveCandidateAssesmentByBatchId: retrieveCandidateAssesmentByBatchId,
            retrieveCandidateAssesmentList: retrieveCandidateAssesmentList,
            assignAssignModuleExamSet: assignAssignModuleExamSet,
            retrieveSubjects: retrieveSubjects,
            retrieveModuleExamSets: retrieveModuleExamSets,
            updateModuleExamSet: updateModuleExamSet
        };

        return service;

        function retrieveAssesments(Paging, OrderBy) {

            var url = "/Assesment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAssesment(SearchKeyword, Paging, OrderBy) {
            var url = "/Assesment/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssesmentByBatchId(batchId, Paging, OrderBy) {
            var url = "/Assesment/CandidateAssesmentByBatchId",
                data = {
                    batchId: batchId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveCandidateAssesmentList(assesments, Paging, OrderBy) {

            var url = "/Assesment/CandidateAssesmentCheckedList",
                data = {
                    assesments: assesments,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function assignAssignModuleExamSet(assesments) {
            var url = "/Assesment/AssignModuleExamSet",
                data = {
                    assesments: assesments
                };
            return $http.post(url, data);
        }

        function retrieveSubjects() {

            var url = "/Assesment/Subject";
            return $http.post(url);
        }

        function retrieveModuleExamSets() {

            var url = "/Assesment/ModuleExamSet";
            return $http.post(url);
        }

        function updateModuleExamSet(assesment) {
            var url = "/Assesment/UpdateCandidateAssesment",
                data = {
                    assesment: assesment
                };
            return $http.post(url, data);
        }
    }
})();