(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('EnquiryController', EnquiryController);

    EnquiryController.$inject = ['$window', 'EnquiryService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function EnquiryController($window, EnquiryService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.enquiries = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editEnquiry = editEnquiry;
        vm.canDeleteEnquiry = canDeleteEnquiry;
        //vm.deleteEnquiry = deleteEnquiry;
        vm.searchEnquiry = searchEnquiry;
        vm.searchEnquiryByDate = searchEnquiryByDate;
        vm.viewEnquiry = viewEnquiry;
        vm.searchKeyword = "";
        vm.fromDate = "";
        vm.toDate = "";
        vm.searchMessage = "";
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        vm.talukas = [];
        vm.retrieveTalukas = retrieveTalukas;
        vm.districts = [];
        vm.retrieveDistricts = retrieveDistricts;
        vm.states = [];
        //vm.retrieveStates = retrieveStates;
        vm.retrieveSectors = retrieveSectors;
        initialise();

        function initialise() {
            vm.orderBy.property = "ConversionProspect";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("ConversionProspect");
        }

        function retrieveEnquiries() {
            vm.orderBy.property = "ConversionProspect";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            return EnquiryService.retrieveEnquiries(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.enquiries = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.enquiries;
                });
        }

        function searchEnquiry(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return EnquiryService.searchEnquiry(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.enquiries = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.enquiries.length === 0 ? "No Records Found" : "";
                  return vm.enquiries;
              });
        }

        function searchEnquiryByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return EnquiryService.searchEnquiryByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.enquiries = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.enquiries.length === 0 ? "No Records Found" : "";
                  return vm.enquiries;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchEnquiry(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchEnquiryByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveEnquiries();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEnquiries();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editEnquiry(id) {
            $window.location.href = "/Enquiry/Edit/" + id;
        }

        function canDeleteEnquiry(id) {
            vm.loadingActions = true;
            vm.CanDeleteEnquiry = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            EnquiryService.canDeleteEnquiry(id).then(function (response) { vm.CanDeleteEnquiry = response.data, vm.loadingActions = false });
        }

        function viewEnquiry(enquiryId) {
            $window.location.href = "/Enquiry/View/" + enquiryId;
        }

        //function deleteAEnquiry(id) {
        //    return EnquiryService.deleteEnquiry(id).then(function () { initialise(); });
        //};

        function retrieveCourses(sectorId) {
            return EnquiryService.retrieveCourses(sectorId).then(function() {
                vm.courses = response.data;
            });
        };

        function retrieveSectors(schemeId) {
            return EnquiryService.retrieveSectors(schemeId).then(function () {
                vm.sectors = response.data;
            });
        };

        function retrieveTalukas(districtId) {
            return EnquiryService.retrieveTalukas(districtId).then(function () {
                vm.talukas = response.data;
            });
        };

        function retrieveDistricts(stateId) {
            return EnquiryService.retrieveDistricts(stateId).then(function () {
                vm.districts = response.data;
            });
        };
    }

})();
