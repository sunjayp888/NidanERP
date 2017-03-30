(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('SubjectController', SubjectController);

    SubjectController.$inject = ['$window', 'SubjectService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function SubjectController($window, SubjectService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.subjects = [];
        vm.sessions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editSubject = editSubject;
        vm.canDeleteSubject = canDeleteSubject;
        vm.deleteSubject = deleteSubject;
        vm.searchSubject = searchSubject;
        vm.viewSubject = viewSubject;
        vm.retrieveSessions = retrieveSessions;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("Name");
        }

        function retrieveSubjects() {
            return SubjectService.retrieveSubjects(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.subjects = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.subjects;
                });
        }

        function retrieveSessions() {
            return SubjectService.retrieveSessions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sessions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.sessions;
                });
        }

        function searchSubject(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return SubjectService.searchSubject(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.subjects = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.subjects.length === 0 ? "No Records Found" : "";
                  return vm.subjects;
              });
        }

        function pageChanged() {
            return retrieveSubjects();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveSubjects();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editSubject(id) {
            $window.location.href = "/Subject/Edit/" + id;
        }

        function canDeleteSubject(id) {
            vm.loadingActions = true;
            vm.CanDeleteSubject = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            SubjectService.canDeleteSubject(id).then(function (response) { vm.CanDeleteSubject = response.data, vm.loadingActions = false });
        }

        function deleteSubject(id) {
            return SubjectService.deleteSubject(id).then(function () { initialise(); });
        };

        function viewSubject(subjectId) {
            $window.location.href = "/Subject/Edit/" + subjectId;
        }

    }

})();
