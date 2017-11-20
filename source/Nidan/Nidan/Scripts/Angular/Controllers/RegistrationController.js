(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('RegistrationController', RegistrationController);

    RegistrationController.$inject = ['$window', 'RegistrationService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function RegistrationController($window, RegistrationService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.Registrations = [];
        vm.enquiries = [];
        vm.candidateFees = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editRegistration = editRegistration;
        vm.canDeleteRegistration = canDeleteRegistration;
        vm.deleteRegistration = deleteRegistration;
        vm.searchRegistration = searchRegistration;
        vm.viewRegistration = viewRegistration;
        vm.createRegistration = createRegistration;
        vm.searchRegistrationByDate = searchRegistrationByDate;
        vm.retrieveEnquiries = retrieveEnquiries;
        vm.searchEnquiry = searchEnquiry;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("EnquiryId");
        }

        function retrieveRegistrations() {
            return RegistrationService.retrieveRegistrations(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.Registrations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.Registrations;
                });
        }

        function retrieveEnquiries() {
            return RegistrationService.retrieveRegistrations(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.Registrations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.Registrations;
                });
        }

        function searchRegistrationByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.orderBy.property = "RegistrationDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return RegistrationService.searchRegistrationByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.Registrations = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.Registrations.length === 0 ? "No Records Found" : "";
                  return vm.Registrations;
              });
        }

        function searchRegistration(searchKeyword) {
            vm.orderBy.property = "RegistrationDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.searchKeyword = searchKeyword;
            return RegistrationService.searchRegistration(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.Registrations = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.Registrations.length === 0 ? "No Records Found" : "";
                  return vm.Registrations;
              });
        }

        function searchEnquiry(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return RegistrationService.searchEnquiry(vm.searchKeyword, vm.paging, vm.orderBy)
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
                searchRegistration(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchRegistrationByDate(vm.fromDate, vm.toDate);
            } else {
                return retrieveRegistrations();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            //retrieveCandidateFeeList();
            return retrieveRegistrations();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editRegistration(id) {
            $window.location.href = "/Registration/Edit/" + id;
        }

        function canDeleteRegistration(id) {
            vm.loadingActions = true;
            vm.CanDeleteRegistration = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            RegistrationService.canDeleteRegistration(id).then(function (response) { vm.CanDeleteRegistration = response.data, vm.loadingActions = false });
        }

        function deleteRegistration(id) {
            return RegistrationService.deleteRegistration(id).then(function () { initialise(); });
        };

        function viewRegistration(registrationId) {
            $window.location.href = "/Registration/View/" + registrationId;
        }

        function createRegistration(enquiryId) {
            $window.location.href = "/Registration/Create/" + enquiryId;
        }

        //function viewCandidateFee(candidateInstallmentId) {
        //    $window.location.href = "/CandidateFee/Detail/" + candidateInstallmentId;
        //}

    }

})();
