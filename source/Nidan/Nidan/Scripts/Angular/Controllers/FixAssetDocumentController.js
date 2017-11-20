(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('FixAssetDocumentController', FixAssetDocumentController);

    FixAssetDocumentController.$inject = ['$window', 'FixAssetDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function FixAssetDocumentController($window, FixAssetDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.fixAssetDocuments = [];
        vm.fixAssetDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveFixAssetDocuments = retrieveFixAssetDocuments;
        vm.createFixAssetDocument = createFixAssetDocument;
        vm.downloadFixAssetDocument = downloadFixAssetDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveFixAssetDocuments(vm.StudentCode);
        }

        function retrieveFixAssetDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return FixAssetDocumentService.retrieveFixAssetDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.fixAssetDocuments = response.data;
                    return vm.fixAssetDocuments;
                });
        }

        function createFixAssetDocument(studentCode) {
            vm.Errors = [];
            vm.StudentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return FixAssetDocumentService.createFixAssetDocument(vm.StudentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#admissionDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveFixAssetDocuments(vm.StudentCode);
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

        function downloadFixAssetDocument(guid) {
            return FixAssetDocumentService.downloadFixAssetDocument(guid);
        }
    }
})();
