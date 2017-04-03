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
        vm.documentsType = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.orderClass = orderClass;
        vm.retrieveStudentDocuments = retrieveStudentDocuments;
        vm.retrieveDocumentsType = retrieveDocumentsType;
        vm.createDocument = createDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveDocumentsType();
            retrieveStudentDocuments(vm.StudentCode);
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

        function retrieveDocumentsType() {
            return DocumentService.retrieveDocumentsType(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.documentsType = response.data;
                    return vm.documentsType;
                });
        }

        function retrieveStudentDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return DocumentService.retrieveStudentDocuments(vm.StudentCode, vm.paging, vm.orderBy)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.documents = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.documents;
                });
        }

        function createDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return DocumentService.createDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#counsellingDocumentModal").modal('hide');
                        $("#trainerDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveStudentDocuments(vm.studentCode);
                    }
                    else {
                        vm.Errors = response.data;
                    }
                });
            }
            else {
                if (vm.documentFile === undefined) {
                    vm.Errors.push('No file available, select file');
                }
            }
        }

        function pageChanged() {
            return retrieveStudentDocuments(vm.studentCode);
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveStudentDocuments(vm.studentCode);
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function removeError() {
            vm.Errors.length = 0;
            vm.documentFile = null;
        }

    }

})();
