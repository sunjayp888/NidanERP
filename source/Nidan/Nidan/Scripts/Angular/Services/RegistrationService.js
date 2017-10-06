(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('RegistrationService', RegistrationService);

    RegistrationService.$inject = ['$http'];

    function RegistrationService($http) {
        var service = {
            retrieveRegistrations: retrieveRegistrations,
            searchRegistrationByDate: searchRegistrationByDate,
            //viewCandidateFee: viewCandidateFee,
            retrieveEnquiries: retrieveEnquiries,
            canDeleteRegistration: canDeleteRegistration,
            deleteRegistration: deleteRegistration,
            searchEnquiry: searchEnquiry,
            searchRegistration: searchRegistration
        };

        return service;

        function retrieveRegistrations(Paging, OrderBy) {

            var url = "/Registration/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchRegistrationByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Registration/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
        
        function retrieveEnquiries(Paging, OrderBy) {

            var url = "/Registration/EnquiryList",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchRegistration(SearchKeyword, Paging, OrderBy) {
            var url = "/Registration/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchEnquiry(SearchKeyword, Paging, OrderBy) {
            var url = "/Registration/EnquirySearch",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteRegistration(id) {
            var url = "/Registration/CanDeleteRegistration",
                data = { id: id };

            return $http.post(url, data);
        }

        function deleteRegistration(id) {
            var url = "/Registration/Delete",
                data = { id: id };

            return $http.post(url, data);
        }
    }
})();