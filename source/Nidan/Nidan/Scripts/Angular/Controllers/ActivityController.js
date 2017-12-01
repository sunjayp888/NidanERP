(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ActivityController', ActivityController);

    ActivityController.$inject = ['$window', 'ActivityService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ActivityController($window, ActivityService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.activities = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editActivity = editActivity;
        vm.searchActivity = searchActivity;
        vm.viewActivity = viewActivity;
        vm.searchActivityByDate = searchActivityByDate;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("CreatedDate");
        }

        function retrieveActivities() {
            return ActivityService.retrieveActivities(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.activities;
                });
        }

        function searchActivity(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return ActivityService.searchActivity(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.activities.length === 0 ? "No Records Found" : "";
                    return vm.activities;
                });
        }

        function searchActivityByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return ActivityService.searchActivityByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activities = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.activities.length === 0 ? "No Records Found" : "";
                    return vm.activities;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchActivity(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchActivityByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveActivities();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveActivities();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editActivity(id) {
            $window.location.href = "/Activity/Edit/" + id;
        }

        function viewActivity(activityId) {
            $window.location.href = "/Activity/View/" + activityId;
        }
    }
})();
