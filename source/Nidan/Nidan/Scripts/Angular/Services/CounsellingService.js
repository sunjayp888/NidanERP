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
            searchCounselling: searchCounselling
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