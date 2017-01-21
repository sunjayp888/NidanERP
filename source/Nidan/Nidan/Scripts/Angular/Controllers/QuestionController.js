(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('QuestionController', QuestionController);

    QuestionController.$inject = ['$window', 'QuestionService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function QuestionController($window, QuestionService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.questions = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editQuestion = editQuestion;
        vm.canDeleteAbsenceType = canDeleteAbsenceType;
        vm.deleteQuestion = deleteQuestion;
        initialise();

        function initialise() {
            order("Description");
        }

        function retrieveQuestions() {
            return QuestionService.retrieveQuestions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.absenceTypes = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.absenceTypes;
                });
        }

        function pageChanged() {
            return retrieveAbsenceTypes();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveAbsenceTypes();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editAbsenceType(id) {
            $window.location.href = "/AbsenceType/Edit/" + id;
        }

        function canDeleteAbsenceType(id) {
            vm.loadingActions = true;
            vm.CanDeleteAbsenceType = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            QuestionService.canDeleteAbsenceType(id).then(function (response) { vm.CanDeleteAbsenceType = response.data, vm.loadingActions = false });
        }
       
        function deleteAbsenceType(id) {
            return QuestionService.deleteAbsenceType(id).then(function () { initialise(); });
        };

    }

})();
