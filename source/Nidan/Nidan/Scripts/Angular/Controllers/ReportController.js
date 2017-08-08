(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ReportController', ReportController);

    ReportController.$inject = ['$window', 'ReportService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ReportController($window, ReportService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.totalSumOfCountReportsByMonth = [];
        vm.totalSumOfCountReportsByDate = [];
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
        vm.searchRegistrationByDate = searchRegistrationByDate;
        vm.searchCounsellingByDate = searchCounsellingByDate;
        vm.searchExpenseByDate = searchExpenseByDate;
        vm.searchMobilizationCountReportBydate = searchMobilizationCountReportBydate;
        vm.searchMobilizationCountReportByMonthAndYear = searchMobilizationCountReportByMonthAndYear;
        vm.downloadEnquiryCSVByDate = downloadEnquiryCSVByDate;
        vm.downloadMobilizationCSVByDate = downloadMobilizationCSVByDate;
        vm.downloadFollowUpCSVByDate = downloadFollowUpCSVByDate;
        vm.downloadAdmissionCSVByDate = downloadAdmissionCSVByDate;
        vm.downloadRegistrationCSVByDate = downloadRegistrationCSVByDate;
        vm.downloadCounsellingCSVByDate = downloadCounsellingCSVByDate;
        vm.downloadExpenseCSVByDate = downloadExpenseCSVByDate;
        vm.totalSumOfCountByMonth = totalSumOfCountByMonth;
        vm.totalSumOfCountByDate = totalSumOfCountByDate;

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

        function searchRegistrationByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "RegistrationDate";
            vm.orderBy.class = "asc";
            order("RegistrationDate");
            return ReportService.searchRegistrationByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function searchCounsellingByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.class = "asc";
            order("CreatedDate");
            return ReportService.searchCounsellingByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function searchExpenseByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "ExpenseCreatedDate";
            vm.orderBy.class = "asc";
            order("ExpenseCreatedDate");
            return ReportService.searchExpenseByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function searchMobilizationCountReportBydate(centreId, fromMonth, fromYear) {
            vm.centreId = centreId;
            vm.fromMonth = fromMonth;
            vm.fromYear = fromYear;
            vm.orderBy.property = "Date";
            vm.orderBy.class = "asc";
            //order("Date");
            return ReportService.searchMobilizationCountReportBydate(vm.centreId, vm.fromMonth, vm.fromYear, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    totalSumOfCountByDate(centreId, fromMonth, fromYear);
                    return vm.reports;
                });
        }

        function searchMobilizationCountReportByMonthAndYear(centreId, fromMonth, toMonth, fromYear, toYear) {
            vm.centreId = centreId;
            vm.fromMonth = fromMonth;
            vm.toMonth = toMonth;
            vm.fromYear = fromYear;
            vm.toYear = toYear;
            vm.orderBy.property = "Month";
            vm.orderBy.class = "asc";
            order("Month");
            return ReportService.searchMobilizationCountReportByMonthAndYear(centreId, fromMonth, toMonth, fromYear, toYear, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    totalSumOfCountByMonth(centreId, fromMonth, toMonth, fromYear, toYear);
                    return vm.reports;
                });
        }

        function pageChanged() {
            vm.centreId = $("#CentreId").val();
            vm.fromDate = $("#fromDate").val();
            vm.toDate = $("#toDate").val();
            vm.fromMonth = $("#FromMonth").val();
            vm.toMonth = $("#ToMonth").val();
            vm.fromYear = $("#FromYear").val();
            vm.toYear = $("#ToYear").val();
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
            if (path[2] == "Registration") {
                searchRegistrationByDate(vm.fromDate, vm.toDate);
            }
            if (path[2] == "Counselling") {
                searchCounsellingByDate(vm.fromDate, vm.toDate);
            }
            if (path[2] == "MobilizationProcessReportByDate") {
                searchMobilizationCountReportBydate(vm.centreId, vm.fromMonth, vm.fromYear);
            }
            if (path[2] == "MobilizationProcessReportByMonth") {
                searchMobilizationCountReportByMonthAndYear(vm.centreId, vm.fromMonth, vm.toMonth, vm.fromYear, vm.toYear);
            }
        }

        function downloadEnquiryCSVByDate(fromDate, toDate) {
            return ReportService.downloadEnquiryCSVByDate(fromDate, toDate);
        }

        function downloadMobilizationCSVByDate(fromDate, toDate) {
            return ReportService.downloadMobilizationCSVByDate(fromDate, toDate);
        }

        function downloadFollowUpCSVByDate(fromDate, toDate) {
            return ReportService.downloadFollowUpCSVByDate(fromDate, toDate);
        }

        function downloadAdmissionCSVByDate(fromDate, toDate) {
            return ReportService.downloadAdmissionCSVByDate(fromDate, toDate);
        }

        function downloadRegistrationCSVByDate(fromDate, toDate) {
            return ReportService.downloadRegistrationCSVByDate(fromDate, toDate);
        }

        function downloadCounsellingCSVByDate(fromDate, toDate) {
            return ReportService.downloadCounsellingCSVByDate(fromDate, toDate);
        }

        function downloadExpenseCSVByDate(fromDate, toDate) {
            return ReportService.downloadExpenseCSVByDate(fromDate, toDate);
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function totalSumOfCountByMonth(centreId, fromMonth, toMonth, fromYear, toYear) {
            return ReportService.totalSumOfCountByMonth(centreId, fromMonth, toMonth, fromYear, toYear).then(function (response) {
                vm.totalSumOfCountReportsByMonth = response.data;
                return vm.totalSumOfCountReportsByMonth;
            });
        }

        function totalSumOfCountByDate(centreId, fromMonth, fromYear) {
            return ReportService.totalSumOfCountByDate(centreId, fromMonth, fromYear).then(function (response) {
                vm.totalSumOfCountReportsByDate = response.data;
                return vm.totalSumOfCountReportsByDate;
            });
        }
    }

})();
