(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ExpenseDocumentController', ExpenseDocumentController);

    ExpenseDocumentController.$inject = ['$window', 'ExpenseDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ExpenseDocumentController($window, ExpenseDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.expenseDocuments = [];
        vm.expenseDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveExpenseDocuments = retrieveExpenseDocuments;
        vm.createExpenseDocument = createExpenseDocument;
        vm.downloadExpenseDocument = downloadExpenseDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveExpenseDocuments(vm.StudentCode);
        }

        function retrieveExpenseDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return ExpenseDocumentService.retrieveExpenseDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.expenseDocuments = response.data;
                    return vm.expenseDocuments;
                });
        }

        function createExpenseDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return ExpenseDocumentService.createExpenseDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#admissionDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveExpenseDocuments(vm.studentCode);
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

        function removeError(documentTypeId) {
            vm.Errors.length = 0;
            vm.documentFile = null;
            vm.documentTypeId = documentTypeId;
        }

        function downloadExpenseDocument(guid) {
            return ExpenseDocumentService.downloadExpenseDocument(guid);
        }
    }
})();
