(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FollowUpController', FollowUpController);

    FollowUpController.$inject = ['$window', 'FollowUpService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FollowUpController($window, FollowUpService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.followUps = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editFollowUp = editFollowUp;
        vm.canDeleteFollowUp = canDeleteFollowUp;
        vm.searchFollowUp = searchFollowUp;
        vm.searchFollowUpByDate = searchFollowUpByDate;
        vm.deleteFollowUp = deleteFollowUp;
        vm.viewFollowUp = viewFollowUp;
        vm.pendingFollowUp = pendingFollowUp;
        vm.todaysFollowUp = todaysFollowUp;
        vm.tomorrowsFollowUp = tomorrowsFollowUp;
        vm.upcomingFollowUp = upcomingFollowUp;
        vm.markAsReadFollowUp = markAsReadFollowUp;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "FollowUpDateTime";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("FollowUpDateTime");
        }

        function retrieveFollowUps() {
            return FollowUpService.retrieveFollowUps(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.followUps;
                });
        }

        function pageChanged() {
            var path = window.location.pathname.split('/');
            if (path[2] == "Pending") {
                pendingFollowUp();
            }
            else if (path[2] == "Todays") {
                todaysFollowUp();
            }
            else if (path[2] == "Tomorrows") {
                tomorrowsFollowUp();
            }
            else if (path[2] == "Upcoming") {
                upcomingFollowUp();
            }
            else {
                if (vm.searchKeyword) {
                    searchFollowUp(vm.searchKeyword);
                } else if (vm.fromDate && vm.toDate) {
                    searchFollowUpByDate(vm.fromDate, vm.toDate);
                } else {
                    retrieveFollowUps();
                }
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveFollowUps();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editFollowUp(id) {
            $window.location.href = "/FollowUp/Edit/" + id;
        }

        function searchFollowUpByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            return FollowUpService.searchFollowUpByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.followUps = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                  return vm.followUps;
              });
        }

        function canDeleteFollowUp(id) {
            vm.loadingActions = true;
            vm.CanDeleteFollowUp = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            FollowUpService.canDeleteFollowUp(id).then(function (response) { vm.CanDeleteFollowUp = response.data, vm.loadingActions = false });
        }

        function searchFollowUp(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return FollowUpService.searchFollowUp(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.followUps = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                  return vm.followUps;
              });
        }

        function deleteFollowUp(id) {
            return FollowUpService.deleteFollowUp(id).then(function () { initialise(); });
        };

        function markAsReadFollowUp(id) {
            return FollowUpService.markAsReadFollowUp(id).then(function () { initialise(); });
        };

        function viewFollowUp(followUpId) {
            $window.location.href = "/FollowUp/Edit/" + followUpId;
        }

        function pendingFollowUp() {
            vm.orderBy.property = "FollowUpDateTime";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return FollowUpService.pendingFollowUp(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                    return vm.followUps;
                });
        }

        function todaysFollowUp() {
            vm.orderBy.property = "FollowUpDateTime";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return FollowUpService.todaysFollowUp(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                    return vm.followUps;
                });
        }

        function tomorrowsFollowUp() {
            vm.orderBy.property = "FollowUpDateTime";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return FollowUpService.tomorrowsFollowUp(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                    return vm.followUps;
                });
        }

        function upcomingFollowUp() {
            vm.orderBy.property = "FollowUpDateTime";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return FollowUpService.upcomingFollowUp(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.followUps = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.followUps.length === 0 ? "No Records Found" : "";
                    return vm.followUps;
                });
        }
    }

})();
