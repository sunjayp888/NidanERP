(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('MobilizationController', MobilizationController);

    MobilizationController.$inject = ['$window', 'MobilizationService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function MobilizationController($window, MobilizationService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobilizations = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editMobilization = editMobilization;
        vm.canDeleteMobilization = canDeleteMobilization;
        vm.deleteMobilization = deleteMobilization;
        vm.searchMobilization = searchMobilization;
        vm.viewMobilization = viewMobilization;
        vm.searchMobilizationByDate = searchMobilizationByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveMobilizations() {
            return MobilizationService.retrieveMobilizations(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobilizations = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.mobilizations;
                });
        }

        function searchMobilization(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return MobilizationService.searchMobilization(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.mobilizations = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.mobilizations.length === 0 ? "No Records Found" : "";
                  return vm.mobilizations;
              });
        }

        function searchMobilizationByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return MobilizationService.searchMobilizationByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.mobilizations = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.mobilizations.length === 0 ? "No Records Found" : "";
                  return vm.mobilizations;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchMobilization(vm.searchKeyword);
            } else {
                return retrieveMobilizations();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveMobilizations();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editMobilization(id) {
            $window.location.href = "/Mobilization/Edit/" + id;
        }

        function canDeleteMobilization(id) {
            vm.loadingActions = true;
            vm.CanDeleteMobilization = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            MobilizationService.canDeleteMobilization(id).then(function (response) { vm.CanDeleteMobilization = response.data, vm.loadingActions = false });
        }

        function deleteMobilization(id) {
            return MobilizationService.deleteMobilization(id).then(function () { initialise(); });
        };

        function viewMobilization(mobilizationId) {
            $window.location.href = "/Mobilization/View/" + mobilizationId;
        }

    }

})();
