(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('StudentAdmissionDocumentController', StudentAdmissionDocumentController);

    StudentAdmissionDocumentController.$inject = ['$window', 'StudentAdmissionDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function StudentAdmissionDocumentController($window, StudentAdmissionDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.studentAdmissionDocuments = [];
        vm.studentAdmissionDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveStudentAdmissionDocuments = retrieveStudentAdmissionDocuments;
        vm.createStudentAdmissionDocument = createStudentAdmissionDocument;
        vm.downloadStudentAdmissionDocument = downloadStudentAdmissionDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveStudentAdmissionDocuments(vm.StudentCode);
        }

        function retrieveStudentAdmissionDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return StudentAdmissionDocumentService.retrieveStudentAdmissionDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.studentAdmissionDocuments = response.data;
                    return vm.studentAdmissionDocuments;
                });
        }

        function createStudentAdmissionDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return StudentAdmissionDocumentService.createStudentAdmissionDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#admissionDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveStudentAdmissionDocuments(vm.studentCode);
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

        function downloadStudentAdmissionDocument(guid) {
            return StudentAdmissionDocumentService.downloadStudentAdmissionDocument(guid);
        }
    }
})();
