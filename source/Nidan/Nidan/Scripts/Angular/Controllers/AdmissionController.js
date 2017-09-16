(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('AdmissionController', AdmissionController);

    AdmissionController.$inject = ['$window', 'AdmissionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function AdmissionController($window, AdmissionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.admissions = [];
        vm.documentsTypes = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editAdmission = editAdmission;
        vm.canDeleteAdmission = canDeleteAdmission;
        vm.deleteAdmission = deleteAdmission;
        vm.searchAdmission = searchAdmission;
        vm.viewAdmission = viewAdmission;
        vm.batches = [];
        vm.retrieveBatches = retrieveBatches;
        vm.retrieveDocumentsType = retrieveDocumentsType;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.searchAdmissionByDate = searchAdmissionByDate;
        vm.isDisabled = false;
        vm.disableButton = disableButton;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "AdmissionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("AdmissionId");
        }

        function retrieveAdmissions() {
            return AdmissionService.retrieveAdmissions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.admissions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.admissions;
                });
        }

        function searchAdmission(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return AdmissionService.searchAdmission(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.admissions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.admissions.length === 0 ? "No Records Found" : "";
                  return vm.admissions;
              });
        }

        function searchAdmissionByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return AdmissionService.searchAdmissionByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.admissions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.admissions.length === 0 ? "No Records Found" : "";
                  return vm.admissions;
              });
        }

        function retrieveDocumentsType(studentCode) {
            return AdmissionService.retrieveDocumentsType(studentCode)
                .then(function (response) {
                    vm.documentsTypes = response.data;
                    return vm.documentsTypes;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchAdmission(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchAdmissionByDate(vm.fromDate, vm.toDate);
            } else {
                return retrieveAdmissions();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAdmissions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAdmission(id) {
            $window.location.href = "/Admission/Edit/" + id;
        }

        function canDeleteAdmission(id) {
            vm.loadingActions = true;
            vm.CanDeleteAdmission = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            AdmissionService.canDeleteAdmission(id).then(function (response) { vm.CanDeleteAdmission = response.data, vm.loadingActions = false });
        }

        function deleteAdmission(id) {
            return AdmissionService.deleteAdmission(id).then(function () { initialise(); });
        };

        function viewAdmission(admissionId) {
            $window.location.href = "/Admission/View/" + admissionId;
        }

        function retrieveBatches(batchId) {
            return AdmissionService.retrieveBatches(batchId).then(function () {
                vm.courses = response.data;
            });
        };

        function disableButton() {
            vm.isDisabled = true;
        }
    }

})();
