(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('OtherFeeController', OtherFeeController);

    OtherFeeController.$inject = ['$window', 'OtherFeeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function OtherFeeController($window, OtherFeeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.otherFees = [];
        vm.enquiryId;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.retrieveOtherFeeByEnquiryId = retrieveOtherFeeByEnquiryId;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise=initialise;

        function initialise() {
            vm.orderBy.property = "OtherFeeId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("OtherFeeId");
        }

        function retrieveOtherFeeByEnquiryId(enquiryId) {
            vm.orderBy.property = "OtherFeeId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.enquiryId = enquiryId;
            return OtherFeeService.retrieveOtherFeeByEnquiryId(vm.enquiryId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.otherFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.otherFees;
                });
        }

        function pageChanged() {
            return retrieveOtherFeeByEnquiryId();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveOtherFeeByEnquiryId();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }

})();
