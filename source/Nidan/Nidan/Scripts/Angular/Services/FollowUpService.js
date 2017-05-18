(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('FollowUpService', FollowUpService);

    FollowUpService.$inject = ['$http'];

    function FollowUpService($http) {
        var service = {
            retrieveFollowUps: retrieveFollowUps,
            canDeleteFollowUp: canDeleteFollowUp,
            deleteFollowUp: deleteFollowUp,
            markAsReadFollowUp: markAsReadFollowUp,
            searchFollowUpByDate: searchFollowUpByDate,
            searchFollowUp: searchFollowUp
        };

        return service;

        function retrieveFollowUps(Paging, OrderBy) {

            var url = "/FollowUp/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchFollowUpByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/FollowUp/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchFollowUp(SearchKeyword, Paging, OrderBy) {
            var url = "/FollowUp/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteFollowUp(id) {
            var url = "/FollowUp/CanDeleteFollowUp",
                data = {id: id};

            return $http.post(url, data);
        }

        function deleteFollowUp(id) {
            var url = "/FollowUp/Delete",
                data = {id: id};

            return $http.post(url, data);
        }

        function markAsReadFollowUp(id) {
            var url = "/FollowUp/MarkAsRead",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();