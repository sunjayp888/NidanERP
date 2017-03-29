(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('SessionController', SessionController);

    SessionController.$inject = ['$window', 'SessionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function SessionController($window, SessionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.sessions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
       // vm.editSession = editSession;
      //  vm.canDeleteSession = canDeleteSession;
      //  vm.deleteSession = deleteSession;
        vm.searchSession = searchSession;
      //  vm.viewSession = viewSession;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "SessionId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("SessionId");
        }

        function retrieveSessions() {
            return SessionService.retrieveSessions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sessions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.sessions;
                });
        }

        function searchSession(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return SessionService.searchSession(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.sessions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.sessions.length === 0 ? "No Records Found" : "";
                  return vm.sessions;
              });
        }

        function pageChanged() {
            return retrieveSessions();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveSessions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        //function editSession(id) {
        //    $window.location.href = "/Mobilization/Edit/" + id;
        //}

        //function canDeleteMobilization(id) {
        //    vm.loadingActions = true;
        //    vm.CanDeleteMobilization = false;
        //    $('.dropdown-menu').slideUp('fast');
        //    $('.' + id).toggle();
        //    MobilizationService.canDeleteMobilization(id).then(function (response) { vm.CanDeleteMobilization = response.data, vm.loadingActions = false });
        //}

        //function deleteMobilization(id) {
        //    return MobilizationService.deleteMobilization(id).then(function () { initialise(); });
        //};

        //function viewMobilization(mobilizationId) {
        //    $window.location.href = "/Mobilization/Edit/" + mobilizationId;
        //}

    }

})();
