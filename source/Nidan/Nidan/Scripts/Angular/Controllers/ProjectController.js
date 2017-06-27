(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ProjectController', ProjectController);

    ProjectController.$inject = ['$window', 'ProjectService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ProjectController($window, ProjectService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.projects = [];
        vm.selectedProjects = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.viewProject = viewProject;
        vm.deleteExpenseProject = deleteExpenseProject;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.type = "";

        initialise();

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveProjects() {
            return ProjectService.retrieveProjects(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.projects = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.projects;
                });
        }

        function pageChanged() {
            return retrieveProjects();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveProjects();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function viewProject(projectId) {
            $window.location.href = "/Project/Edit/" + projectId;
        }

        function deleteExpenseProject(expenseId, $item) {
            return ProjectService.deleteExpenseProject(expenseId, $item.ProjectId)
                .then(function () {
                });
        }
    }

})();
