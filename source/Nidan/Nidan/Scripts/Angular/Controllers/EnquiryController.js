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
        initialise();

        function initialise() {
            order("CandidateName");
        }

        function retrieveEnquiries() {
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
            return retrieveEnquiries();
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
            $window.location.href = "/Enquiry/Edit/" + enquiryId;
        }

        //function deleteAEnquiry(id) {
        //    return EnquiryService.deleteEnquiry(id).then(function () { initialise(); });
        //};

    }

})();
