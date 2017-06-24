(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ReportController', ReportController);

    ReportController.$inject = ['$window', 'ReportService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ReportController($window, ReportService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.reports = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        //vm.retrieveEnquiryReports = retrieveEnquiryReports;
        vm.searchEnquiryByDate = searchEnquiryByDate;
        //vm.retrieveMobilizationReports = retrieveMobilizationReports;
        vm.searchMobilizationByDate = searchMobilizationByDate;
        //vm.retrieveFollowUpReports = retrieveFollowUpReports;
        vm.searchFollowUpByDate = searchFollowUpByDate;
        vm.downloadEnquiryCSVByDate = downloadEnquiryCSVByDate;
        vm.searchAdmissionByDate = searchAdmissionByDate;

        function initialise() {
            vm.orderBy.property = "ReportId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("ReportId");
        }

        //function retrieveEnquiryReports() {
        //    return ReportService.retrieveEnquiryReports(vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.reports = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            return vm.reports;
        //        });
        //}

        function searchEnquiryByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "EnquiryDate";
            vm.orderBy.class = "asc";
            order("EnquiryDate");
            return ReportService.searchEnquiryByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        //function retrieveMobilizationReports() {
        //    return ReportService.retrieveMobilizationReports(vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.reports = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            return vm.reports;
        //        });
        //}

        function searchMobilizationByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.class = "asc";
            order("CreatedDate");
            return ReportService.searchMobilizationByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        //function retrieveFollowUpReports() {
        //    return ReportService.retrieveFollowUpReports(vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.reports = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            return vm.reports;
        //        });
        //}

        function searchFollowUpByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "FollowUpId";
            vm.orderBy.class = "asc";
            order("FollowUpId");
            return ReportService.searchFollowUpByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function searchAdmissionByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "AdmissionDate";
            vm.orderBy.class = "asc";
            order("AdmissionDate");
            return ReportService.searchAdmissionByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        //function searchReport(searchKeyword) {
        //    vm.searchKeyword = searchKeyword;
        //    return ReportService.searchReport(vm.searchKeyword, vm.paging, vm.orderBy)
        //      .then(function (response) {
        //          vm.reports = response.data.Items;
        //          vm.paging.totalPages = response.data.TotalPages;
        //          vm.paging.totalResults = response.data.TotalResults;
        //          vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
        //          return vm.reports;
        //      });
        //}

        //function retrieveReport() {
        //    return ReportService.retrieveReport(vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.reports = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            return vm.reports;
        //        });
        //}

        function pageChanged() {
            vm.fromDate = $("#txt_FromDate").val();
            vm.toDate = $("#txt_ToDate").val();
            var path = window.location.pathname.split('/');
            if (path[2] == "Enquiry") {
                searchEnquiryByDate(vm.fromDate, vm.toDate);
            }
            if (path[2] == "Mobilization") {
                searchMobilizationByDate(vm.fromDate, vm.toDate);
            }
            if (path[2] == "FollowUp") {
                searchFollowUpByDate(vm.fromDate, vm.toDate);
            }
            if (path[2] == "Admission") {
                searchAdmissionByDate(vm.fromDate, vm.toDate);
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            //retrieveReport();
            //return retrieveReports();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function downloadEnquiryCSVByDate(fromDate, toDate) {
            return ReportService.downloadEnquiryCSVByDate(fromDate, toDate);
        }
    }

})();
