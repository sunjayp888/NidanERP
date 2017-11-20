(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('TrainerDocumentController', TrainerDocumentController);

    TrainerDocumentController.$inject = ['$window', 'TrainerDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function TrainerDocumentController($window, TrainerDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.trainerDocuments = [];
        vm.trainerDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveTrainerDocuments = retrieveTrainerDocuments;
        vm.createTrainerDocument = createTrainerDocument;
        vm.downloadTrainerDocument = downloadTrainerDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveTrainerDocuments(vm.StudentCode);
        }

        function retrieveTrainerDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return TrainerDocumentService.retrieveTrainerDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.trainerDocuments = response.data;
                    return vm.trainerDocuments;
                });
        }

        function createTrainerDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return TrainerDocumentService.createTrainerDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#admissionDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveTrainerDocuments(vm.studentCode);
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

        function downloadTrainerDocument(guid) {
            return TrainerDocumentService.downloadTrainerDocument(guid);
        }
    }
})();
