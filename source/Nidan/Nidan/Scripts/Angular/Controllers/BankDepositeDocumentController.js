(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('BankDepositeDocumentController', BankDepositeDocumentController);

    BankDepositeDocumentController.$inject = ['$window', 'BankDepositeDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function BankDepositeDocumentController($window, BankDepositeDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.bankDepositeDocuments = [];
        vm.bankDepositeDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveBankDepositeDocuments = retrieveBankDepositeDocuments;
        vm.createBankDepositeDocument = createBankDepositeDocument;
        vm.downloadBankDepositeDocument = downloadBankDepositeDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveBankDepositeDocuments(vm.StudentCode);
        }

        function retrieveBankDepositeDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return BankDepositeDocumentService.retrieveBankDepositeDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.bankDepositeDocuments = response.data;
                    return vm.bankDepositeDocuments;
                });
        }

        function createBankDepositeDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return BankDepositeDocumentService.createBankDepositeDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#bankDepositeDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveBankDepositeDocuments(vm.studentCode);
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

        function downloadBankDepositeDocument(guid) {
            return BankDepositeDocumentService.downloadBankDepositeDocument(guid);
        }
    }
})();
