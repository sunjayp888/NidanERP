(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('DocumentController', DocumentController);

    DocumentController.$inject = ['$window', 'DocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function DocumentController($window, DocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.documents = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.retrieveStudentDocuments = retrieveStudentDocuments;
        initialise();

        function initialise() {
            order("CreatedDateTime");
        }

        function retrieveDocuments() {
            return DocumentService.retrieveDocuments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.documents = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.documents;
                });
        }

        function retrieveStudentDocuments(studentCode, category) {
            vm.StudentCode = studentCode;
            vm.Category = category;

            return DocumentService.retrieveStudentDocuments(vm.StudentCode, vm.Category, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.documents = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.documents;
                });
        }

        function pageChanged() {
            return retrieveDocuments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveDocuments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }

})();
