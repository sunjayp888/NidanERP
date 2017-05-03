(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CandidateFeeController', CandidateFeeController);

    CandidateFeeController.$inject = ['$window', 'CandidateFeeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CandidateFeeController($window, CandidateFeeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.candidateFees = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCandidateFee = editCandidateFee;
        vm.viewCandidateFee = viewCandidateFee;
        //vm.searchKeyword = "";
        //vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "InstallmentDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("InstallmentDate");
        }

        function retrieveCandidateFees() {
            return CandidateFeeService.retrieveCandidateFees(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.candidateFees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.candidateFees;
                });
        }

        //function searchMobilization(searchKeyword) {
        //    vm.searchKeyword = searchKeyword;
        //    return MobilizationService.searchMobilization(vm.searchKeyword, vm.paging, vm.orderBy)
        //      .then(function (response) {
        //          vm.mobilizations = response.data.Items;
        //          vm.paging.totalPages = response.data.TotalPages;
        //          vm.paging.totalResults = response.data.TotalResults;
        //          vm.searchMessage = vm.mobilizations.length === 0 ? "No Records Found" : "";
        //          return vm.mobilizations;
        //      });
        //}

        function pageChanged() {
            return retrieveCandidateFees();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCandidateFees();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCandidateFee(id) {
            $window.location.href = "/CandidateFee/Edit/" + id;
        }


        

        function viewCandidateFee(candidateFeeId) {
            $window.location.href = "/CandidateFee/Edit/" + candidateFeeId;
        }

    }

})();
