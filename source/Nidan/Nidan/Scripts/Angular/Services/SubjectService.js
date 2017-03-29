(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('SubjectService', SubjectService);

    SubjectService.$inject = ['$http'];

    function SubjectService($http) {
        var service = {
            retrieveSubjects: retrieveSubjects,
            canDeleteSubject: canDeleteSubject,
            deleteSubject: deleteSubject,
            searchSubject: searchSubject,
            retrieveSessions: retrieveSessions
        };

        return service;

        function retrieveSubjects(Paging, OrderBy) {

            var url = "/Subject/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function retrieveSessions(Paging, OrderBy) {

            var url = "/Subject/SessionList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchSubject(SearchKeyword, Paging, OrderBy) {
            var url = "/Subject/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteSubject(id) {
            var url = "/Subject/CanDeleteSubject",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteSubject(id) {
            var url = "/Subject/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();