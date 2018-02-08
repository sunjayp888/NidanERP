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
        vm.centreId;
        vm.assetClassId;
        vm.registrationSummaryReports = [];
        vm.downPaymentSummaryReports = [];
        vm.installmentSummaryReports = [];
        vm.centrePettyCashReports = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.centreId;
        vm.fromYear;
        vm.date;
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
        vm.availablePettyCashReport = availablePettyCashReport;
        vm.searchStockByDate = searchStockByDate;
        vm.searchMobilizationCountReportBydate = searchMobilizationCountReportBydate;
        vm.searchBankDepositeReportBydate = searchBankDepositeReportBydate;
        vm.searchBankDepositeCountReportBydate = searchBankDepositeCountReportBydate;
        vm.searchMobilizationCountReportByMonth = searchMobilizationCountReportByMonth;
        vm.searchMobilizationCountReportByMonthAndYear = searchMobilizationCountReportByMonthAndYear;
        vm.searchFixAssetByCentreIdAssetClassId = searchFixAssetByCentreIdAssetClassId;
        vm.searchBankDepositeReportByMonthAndYear = searchBankDepositeReportByMonthAndYear;
        vm.searchBankDepositeReportByMonth = searchBankDepositeReportByMonth;
        vm.downloadEnquiryCSVByDate = downloadEnquiryCSVByDate;
        vm.downloadMobilizationCSVByDate = downloadMobilizationCSVByDate;
        vm.downloadFollowUpCSVByDate = downloadFollowUpCSVByDate;
        vm.downloadAdmissionCSVByDate = downloadAdmissionCSVByDate;
        vm.downloadRegistrationCSVByDate = downloadRegistrationCSVByDate;
        vm.downloadCounsellingCSVByDate = downloadCounsellingCSVByDate;
        vm.downloadExpenseCSVByDate = downloadExpenseCSVByDate;
        vm.downloadFixAssetByCentreIdAssetClassId = downloadFixAssetByCentreIdAssetClassId;
        vm.downloadStockCSVByDate = downloadStockCSVByDate;
        vm.totalSumOfCountByMonth = totalSumOfCountByMonth;
        vm.totalSumOfCountByDate = totalSumOfCountByDate;
        vm.downloadMobilizationCountReportCSVByMonthAndYear = downloadMobilizationCountReportCSVByMonthAndYear;
        vm.downloadMobilizationCountReportCSVByDate = downloadMobilizationCountReportCSVByDate;
        vm.viewMobilizationReportByDate = viewMobilizationReportByDate;
        vm.searchBankDepositeReportBydate = searchBankDepositeReportBydate;
        vm.searchBankDepositeCountReportBydate = searchBankDepositeCountReportBydate;
        vm.searchBankDepositeReportByMonthAndYear = searchBankDepositeReportByMonthAndYear;
        vm.searchBankDepositeReportByMonth = searchBankDepositeReportByMonth;
        vm.viewBankDepositeReportByDate = viewBankDepositeReportByDate;
        vm.viewBankDepositeReportByMonthWise = viewBankDepositeReportByMonthWise;
        vm.viewBankDepositeDetailByDate = viewBankDepositeDetailByDate;
        vm.retrieveBankDepositeByDate = retrieveBankDepositeByDate;
        vm.availablePettyCashReport = availablePettyCashReport;
        vm.viewCentrePettyCashByCentreId = viewCentrePettyCashByCentreId;
        vm.viewBankDepositeReportByDate = viewBankDepositeReportByDate;
        vm.viewMobilizationReportByMonthWise = viewMobilizationReportByMonthWise;
        vm.viewBankDepositeReportByMonthWise = viewBankDepositeReportByMonthWise;
        vm.viewCandidateFeeByDate = viewCandidateFeeByDate;
        vm.viewBankDepositeDetailByDate = viewBankDepositeDetailByDate;
        vm.retrieveCandidateFeeByDate = retrieveCandidateFeeByDate;
        vm.retrieveBankDepositeByDate = retrieveBankDepositeByDate;
        vm.retrieveRegistrationSummaryByDate = retrieveRegistrationSummaryByDate;
        vm.retrieveDownPaymentSummaryByDate = retrieveDownPaymentSummaryByDate;
        vm.retrieveInstallmentSummaryByDate = retrieveInstallmentSummaryByDate;
        vm.retrieveCentrePettyCashByCentreId = retrieveCentrePettyCashByCentreId;
        vm.viewCentrePettyCashByCentreId = viewCentrePettyCashByCentreId;
        vm.searchExpenseDetailReportByDate = searchExpenseDetailReportByDate;

        function initialise() {
            vm.orderBy.property = "ReportId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("ReportId");
        }

        function searchFixAssetByCentreIdAssetClassId(assetClassId, centreId) {
            vm.assetClassId = assetClassId;
            vm.centreId = centreId;
            vm.orderBy.property = "fixAssetMappingId";
            vm.orderBy.class = "asc";
            order("fixAssetMappingId");
            return ReportService.searchFixAssetByCentreIdAssetClassId(vm.assetClassId, vm.centreId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }
        
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

        function searchStockByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "StockPurchaseDate";
            vm.orderBy.class = "asc";
            order("StockPurchaseDate");
            return ReportService.searchStockByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

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

        function searchMobilizationCountReportBydate(centreId, fromMonth, fromYear) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.fromMonth = fromMonth == undefined ? getUrlParameter("month") : fromMonth;
            vm.fromYear = fromYear == undefined ? getUrlParameter("year") : fromYear;

            return ReportService.searchMobilizationCountReportBydate(vm.centreId, vm.fromMonth, vm.fromYear)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    // totalSumOfCountByDate(centreId, fromMonth, fromYear);
                    $('#CentreId').val(vm.centreId);
                    $('#FromMonth').val(vm.fromMonth);
                    $('#FromYear').val(vm.fromYear);
                    return vm.reports;
                });
        }

        function searchMobilizationCountReportByMonth() {
            vm.orderBy.property = "Month";
            vm.orderBy.class = "asc";
            order("Date");
            return ReportService.searchMobilizationCountReportByMonth().then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function searchMobilizationCountReportByMonthAndYear(centreId,fromYear) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.fromYear = fromYear == undefined ? getUrlParameter("year") : fromYear;
            vm.orderBy.property = "Month";
            vm.orderBy.class = "asc";
            order("Month");
            return ReportService.searchMobilizationCountReportByMonthAndYear(vm.centreId, vm.fromYear, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    totalSumOfCountByMonth(centreId, fromMonth, toMonth, fromYear, toYear);
                    return vm.reports;
                });
        }

        function availablePettyCashReport() {
            vm.orderBy.property = "CentreId";
            vm.orderBy.class = "asc";
            vm.orderBy.direction = "Ascending";
            return ReportService.availablePettyCashReport(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }

        function pageChanged() {
            vm.centreId = $("#CentreId").val();
            vm.assetClassId = $("#AssetClassId").val();
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
            if (path[2] == "BankDepositeReportByDate") {
                searchBankDepositeReportBydate(vm.centreId, vm.fromMonth, vm.fromYear);
            }
            if (path[2] == "MobilizationProcessReportByMonth") {
                searchMobilizationCountReportByMonthAndYear(vm.centreId, vm.fromMonth, vm.toMonth, vm.fromYear, vm.toYear);
            }
            if (path[2] == "BankDepositeProcessReportByMonth") {
                searchBankDepositeReportByMonthAndYear(vm.centreId, vm.fromMonth, vm.toMonth, vm.fromYear, vm.toYear);
            }
            if (path[2] == "BankDepositeReportByDate") {
                searchBankDepositeReportBydate(vm.centreId, vm.fromMonth, vm.fromYear);
            }
            if (path[2] == "BankDepositeProcessReportByMonth") {
                searchBankDepositeReportByMonthAndYear(vm.centreId, vm.fromMonth, vm.toMonth, vm.fromYear, vm.toYear);
            }
        }

        function downloadFixAssetByCentreIdAssetClassId(centreId, assetClassId) {
            return ReportService.downloadFixAssetByCentreIdAssetClassId(centreId, assetClassId);
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

        function downloadMobilizationCountReportCSVByMonthAndYear(centreId,fromYear) {
            return ReportService.downloadMobilizationCountReportCSVByMonthAndYear(centreId,fromYear);
        }

        function downloadMobilizationCountReportCSVByDate(centreId, month, year) {
            return ReportService.downloadMobilizationCountReportCSVByDate(centreId, month, year);
        }

        function downloadEnquiryCSVByDate(fromDate, toDate) {
            return ReportService.downloadEnquiryCSVByDate(fromDate, toDate);
        }

        function downloadStockCSVByDate(fromDate, toDate) {
            return ReportService.downloadStockCSVByDate(fromDate, toDate);
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

        function retrieveCandidateFeeByDate(centreId,date) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.date = date == undefined ? getUrlParameter("date") : date;
            return ReportService.retrieveCandidateFeeByDate(vm.centreId, vm.date).then(function (response) {
                vm.reports = response.data;
                return vm.reports;
            });
        }

        function retrieveRegistrationSummaryByDate(centreId, date) {
            vm.centreId = centreId;
            vm.date = date;
            return ReportService.retrieveRegistrationSummaryByDate(vm.centreId, vm.date).then(function (response) {
                vm.registrationSummaryReports = response.data.Items;
                vm.downPaymentSummaryReports = null;
                vm.installmentSummaryReports = null;
                return vm.registrationSummaryReports;
            });
        } 

        function retrieveDownPaymentSummaryByDate(centreId, date) {
            vm.centreId = centreId;
            vm.date = date;
            return ReportService.retrieveDownPaymentSummaryByDate(vm.centreId, vm.date).then(function (response) {
                vm.downPaymentSummaryReports = response.data.Items;
                vm.registrationSummaryReports = null;
                vm.installmentSummaryReports = null;
                return vm.downPaymentSummaryReports;
            });
        }

        function retrieveInstallmentSummaryByDate(centreId, date) {
            vm.centreId = centreId;
            vm.date = date;
            return ReportService.retrieveInstallmentSummaryByDate(vm.centreId, vm.date).then(function (response) {
                vm.installmentSummaryReports = response.data.Items;
                vm.registrationSummaryReports = null;
                vm.downPaymentSummaryReports = null;
                return vm.installmentSummaryReports;
            });
        }

        function retrieveCentrePettyCashByCentreId(centreId) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            return ReportService.retrieveCentrePettyCashByCentreId(vm.centreId, vm.paging, vm.orderBy).then(function (response) {
                vm.centrePettyCashReports = response.data.Items;
                return vm.centrePettyCashReports;
            });
        }

        function totalSumOfCountByDate(centreId, fromMonth, fromYear) {
            return ReportService.totalSumOfCountByDate(centreId, fromMonth, fromYear).then(function (response) {
                vm.totalSumOfCountReportsByDate = response.data;
                return vm.totalSumOfCountReportsByDate;
            });
        }

        function viewMobilizationReportByDate(centreId, fromMonth, fromYear) {
            window.location.href = "/Report/MobilizationProcessReportByDate?centreId=" +centreId + "&month=" + fromMonth + "&year=" + fromYear;
        }

        function viewCandidateFeeByDate(centreId,date) {
            window.location.href = "/Report/FeeSummaryByDate?centreId=" + centreId + "&date=" + date;
        }
        
        function viewMobilizationReportByMonthWise(centreId, fromYear) {
            vm.centreId = centreId;
            vm.fromYear = fromYear;
            window.location.href = "/Report/MobilizationProcessReportByMonth?centreId=" + centreId + "&year=" + fromYear;
        }

        function viewCentrePettyCashByCentreId(centreId) {
            window.location.href = "/Report/CentrePettyCashByCentre?centreId=" + centreId;
        }

       function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
       }

        function searchBankDepositeReportBydate(centreId, fromMonth, fromYear) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.fromMonth = fromMonth == undefined ? getUrlParameter("month") : fromMonth;
            vm.fromYear = fromYear == undefined ? getUrlParameter("year") : fromYear;

            return ReportService.searchBankDepositeReportBydate(vm.centreId, vm.fromMonth, vm.fromYear)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    // totalSumOfCountByDate(centreId, fromMonth, fromYear);
                    $('#CentreId').val(vm.centreId);
                    $('#FromMonth').val(vm.fromMonth);
                    $('#FromYear').val(vm.fromYear);
                    return vm.reports;
                });
        }

        function searchBankDepositeCountReportBydate(centreId, fromMonth, fromYear) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.fromMonth = fromMonth == undefined ? getUrlParameter("month") : fromMonth;
            vm.fromYear = fromYear == undefined ? getUrlParameter("year") : fromYear;

            return ReportService.searchBankDepositeCountReportBydate(vm.centreId, vm.fromMonth, vm.fromYear)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    // totalSumOfCountByDate(centreId, fromMonth, fromYear);
                    $('#CentreId').val(vm.centreId);
                    $('#FromMonth').val(vm.fromMonth);
                    $('#FromYear').val(vm.fromYear);
                    return vm.reports;
                });
        }

        function searchBankDepositeReportByMonthAndYear(centreId, fromYear) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.fromYear = fromYear == undefined ? getUrlParameter("year") : fromYear;
            vm.orderBy.property = "Month";
            vm.orderBy.class = "asc";
            order("Month");
            return ReportService.searchBankDepositeReportByMonthAndYear(vm.centreId, vm.fromYear, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    totalSumOfCountByMonth(centreId, fromMonth, toMonth, fromYear, toYear);
                    return vm.reports;
                });
        }

        function searchBankDepositeReportByMonth() {
            vm.orderBy.property = "Month";
            vm.orderBy.class = "asc";
            order("Date");
            return ReportService.searchBankDepositeReportByMonth().then(function (response) {
                vm.reports = response.data;
                vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                return vm.reports;
            });
        }

        function retrieveBankDepositeByDate(centreId, date) {
            vm.centreId = centreId == undefined ? getUrlParameter("centreId") : centreId;
            vm.date = date == undefined ? getUrlParameter("date") : date;
            return ReportService.retrieveBankDepositeByDate(vm.centreId, vm.date).then(function (response) {
                vm.reports = response.data.Items;
                return vm.reports;
            });
        }

        function viewBankDepositeReportByDate(centreId, fromMonth, fromYear) {
            window.location.href = "/Report/BankDepositeReportByDate?centreId=" + centreId + "&month=" + fromMonth + "&year=" + fromYear;
        }

        function viewBankDepositeDetailByDate(centreId, date) {
            window.location.href = "/Report/BankDepositeDetailByDate?centreId=" + centreId + "&date=" + date;
        }

        function viewBankDepositeReportByMonthWise(centreId, fromYear) {
            vm.centreId = centreId;
            vm.fromYear = fromYear;
            window.location.href = "/Report/BankDepositeProcessReportByMonth?centreId=" + centreId + "&year=" + fromYear;
        }

        function searchExpenseDetailReportByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "ExpenseGeneratedDate";
            vm.orderBy.class = "asc";
            order("ExpenseGeneratedDate");
            return ReportService.searchExpenseDetailReportByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.reports = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.reports.length === 0 ? "No Records Found" : "";
                    return vm.reports;
                });
        }
    }

})();
