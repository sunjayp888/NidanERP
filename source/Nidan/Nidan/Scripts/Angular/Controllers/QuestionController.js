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
        vm.canDeleteQuestion = canDeleteQuestion;
        vm.deleteQuestion = deleteQuestion;
        vm.searchQuestion = searchQuestion;
        vm.viewQuestion = viewQuestion;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("Description");
        }

        function retrieveQuestions() {
            return QuestionService.retrieveQuestions(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.questions = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.questions;
                });
        }

        function searchQuestion(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return QuestionService.searchQuestion(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.questions = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.questions.length === 0 ? "No Records Found" : "";
                  return vm.questions;
              });
        }

        function pageChanged() {
            return retrieveQuestions();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveQuestions();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editQuestion(id) {
            $window.location.href = "/Question/Edit/" + id;
        }

        function canDeleteQuestion(id) {
            vm.loadingActions = true;
            vm.CanDeleteQuestion = false;
            $('.dropdown-menu').slideUp('fast');
            $('.' + id).toggle();
            QuestionService.canDeleteQuestion(id).then(function (response) { vm.CanDeleteQuestion = response.data, vm.loadingActions = false });
        }

        function deleteQuestion(id) {
            return QuestionService.deleteQuestion(id).then(function () { initialise(); });
        };

        function viewQuestion(questionId) {
            $window.location.href = "/Question/Edit/" + questionId;
        }

    }

})();
