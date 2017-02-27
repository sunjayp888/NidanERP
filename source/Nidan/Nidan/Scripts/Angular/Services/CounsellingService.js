(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('CounsellingService', CounsellingService);

    CounsellingService.$inject = ['$http'];

    function CounsellingService($http) {
        var service = {
            retrieveCounsellings: retrieveCounsellings,
            canDeleteCounselling: canDeleteCounselling,
            // deleteEnquiry: deleteEnquiry
            searchCounselling: searchCounselling,
            searchCounsellingByDate: searchCounsellingByDate

        };

        return service;

        function retrieveCounsellings(Paging, OrderBy) {

            var url = "/Counselling/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchCounselling(SearchKeyword, Paging, OrderBy) {
            var url = "/Counselling/Search",
            data = {
                searchKeyword: SearchKeyword,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function searchCounsellingByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Counselling/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }

        function canDeleteCounselling(id) {
            var url = "/Counselling/CanDeleteCounselling",
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