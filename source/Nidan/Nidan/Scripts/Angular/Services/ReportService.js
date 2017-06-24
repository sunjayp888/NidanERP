(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('ReportService', ReportService);

    ReportService.$inject = ['$http'];

    function ReportService($http) {
        var service = {
            //retrieveEnquiryReports: retrieveEnquiryReports,
            searchEnquiryByDate: searchEnquiryByDate,
            //retrieveMobilizationReports: retrieveMobilizationReports,
            searchMobilizationByDate: searchMobilizationByDate,
            //retrieveFollowUpReports: retrieveFollowUpReports,
            searchFollowUpByDate: searchFollowUpByDate,
            downloadEnquiryCSVByDate: downloadEnquiryCSVByDate,
            searchAdmissionByDate: searchAdmissionByDate
        };

        return service;

        //function retrieveEnquiryReports(Paging, OrderBy) {

        //    var url = "/Report/EnquiryList",
        //        data = {
        //            paging: Paging,
        //            orderBy: new Array(OrderBy)
        //        };

        //    return $http.post(url, data);
        //}

        function searchEnquiryByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Report/SearchEnquiryByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        //function retrieveMobilizationReports(Paging, OrderBy) {

        //    var url = "/Report/MobilizationList",
        //        data = {
        //            paging: Paging,
        //            orderBy: new Array(OrderBy)
        //        };

        //    return $http.post(url, data);
        //}

        function searchMobilizationByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Report/SearchMobilizationByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        //function retrieveFollowUpReports(Paging, OrderBy) {

        //    var url = "/Report/FollowUpList",
        //        data = {
        //            paging: Paging,
        //            orderBy: new Array(OrderBy)
        //        };

        //    return $http.post(url, data);
        //}

        function searchFollowUpByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Report/SearchFollowUpByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function searchAdmissionByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/Report/SearchAdmissionByDate",
                data = {
                    fromDate: FromDate,
                    toDate: ToDate,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function downloadEnquiryCSVByDate(fromDate, toDate) {
            var url = "/Report/DownloadEnquiryCSVByDate",
                data = {
                    fromDate: fromDate,
                    toDate: toDate
                };
            return $http.post(url, data);
        }



    }
})();