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