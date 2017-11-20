(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('StudentCounsellingDocumentController', StudentCounsellingDocumentController);

    StudentCounsellingDocumentController.$inject = ['$window', 'StudentCounsellingDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function StudentCounsellingDocumentController($window, StudentCounsellingDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.studentCounsellingDocuments = [];
        vm.studentCounsellingDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrieveStudentCounsellingDocuments = retrieveStudentCounsellingDocuments;
        vm.createStudentCounsellingDocument = createStudentCounsellingDocument;
        vm.downloadStudentCounsellingDocument = downloadStudentCounsellingDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrieveStudentCounsellingDocuments(vm.StudentCode);
        }

        function retrieveStudentCounsellingDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return StudentCounsellingDocumentService.retrieveStudentCounsellingDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.studentCounsellingDocuments = response.data;
                    return vm.studentCounsellingDocuments;
                });
        }

        function createStudentCounsellingDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return StudentCounsellingDocumentService.createStudentCounsellingDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#counsellingDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrieveStudentCounsellingDocuments(vm.studentCode);
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

        function downloadStudentCounsellingDocument(guid) {
            return StudentCounsellingDocumentService.downloadStudentCounsellingDocument(guid);
        }
    }
})();
