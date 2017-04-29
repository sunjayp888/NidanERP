(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('EnquiryService', EnquiryService);

    EnquiryService.$inject = ['$http'];

    function EnquiryService($http) {
        var service = {
            retrieveEnquiries: retrieveEnquiries,
            canDeleteEnquiry: canDeleteEnquiry,
            // deleteEnquiry: deleteEnquiry
            searchEnquiry: searchEnquiry,
            searchEnquiryByDate: searchEnquiryByDate
            
        };

        return service;

        function retrieveEnquiries(Paging, OrderBy) {

            var url = "/Enquiry/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchEnquiry(SearchKeyword, Paging, OrderBy) {
            var url = "/Enquiry/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchEnquiryByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Enquiry/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteEnquiry(id) {
            var url = "/Enquiry/CanDeleteEnquiry",
                data = { id: id };

            return $http.post(url, data);
        }

        //function deleteEnquiry(id) {
        //    var url = "/Enquiry/Delete",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}
    }
})();