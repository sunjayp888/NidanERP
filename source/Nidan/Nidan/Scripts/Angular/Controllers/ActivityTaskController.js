(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ActivityTaskController', ActivityTaskController);

    ActivityTaskController.$inject = ['$window', 'ActivityTaskService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ActivityTaskController($window, ActivityTaskService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.activityTasks = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editActivityTask = editActivityTask;
        vm.searchActivityTask = searchActivityTask;
        vm.viewActivityTask = viewActivityTask;
        vm.searchActivityTaskByDate = searchActivityTaskByDate;
        vm.retrieveActivityTasksByActivityId = retrieveActivityTasksByActivityId;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("CreatedDate");
        }

        function retrieveActivityTasks() {
            return ActivityTaskService.retrieveActivityTasks(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activityTasks = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.activityTasks;
                });
        }

        function retrieveActivityTasksByActivityId(activityId) {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return ActivityTaskService.retrieveActivityTasksByActivityId(activityId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activityTasks = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.activityTasks.length === 0 ? "No Records Found" : "";
                    return vm.activityTasks;
                });
        }

        function searchActivityTask(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.fromDate = null;
            vm.toDate = null;
            return ActivityTaskService.searchActivityTask(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activityTasks = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.activityTasks.length === 0 ? "No Records Found" : "";
                    return vm.activityTasks;
                });
        }

        function searchActivityTaskByDate(fromDate, toDate) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.searchKeyword = null;
            return ActivityTaskService.searchActivityTaskByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.activityTasks = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.activityTasks.length === 0 ? "No Records Found" : "";
                    return vm.activityTasks;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchActivityTask(vm.searchKeyword);
            } else if (vm.fromDate && vm.toDate) {
                searchActivityTaskByDate(vm.fromDate, vm.toDate);
            }
            else {
                return retrieveActivityTasks();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveActivityTasks();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editActivityTask(id) {
            $window.location.href = "/ActivityTask/Edit/" + id;
        }

        function viewActivityTask(activityTaskId) {
            $window.location.href = "/ActivityTask/View/" + activityTaskId;
        }
    }
})();
