(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OtherFeeService', OtherFeeService);

    OtherFeeService.$inject = ['$http'];

    function OtherFeeService($http) {
        var service = {
            retrieveOtherFeeByEnquiryId: retrieveOtherFeeByEnquiryId
        };

        return service;

        function retrieveOtherFeeByEnquiryId(enquiryId, Paging, OrderBy) {

            var url = "/OtherFee/CandidateOtherFeeByEnquiryId",
                data = {
                    enquiryId:enquiryId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();