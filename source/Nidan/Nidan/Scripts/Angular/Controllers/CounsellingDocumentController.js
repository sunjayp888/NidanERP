(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CounsellingDocumentController', CounsellingDocumentController);

    CounsellingDocumentController.$inject = ['$window', 'DocumentService', 'Paging'];

    function CounsellingDocumentController($window, DocumentService, Paging) {
        /* jshint validthis:true */
        var vm = this;        
        vm.counsellingDocuments = [];
        vm.jobTitleId;
        vm.name;
        vm.documentFile;
        vm.IsJobTitleDocumentMapped;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.Errors = [];
        vm.createCounsellingDocument = createCounsellingDocument;
        vm.removeError = removeError;
        vm.deleteJobTitleDocument = deleteJobTitleDocument;
        vm.retrieveStudentDocuments = retrieveStudentDocuments;
        vm.retrieveDocumentsType = retrieveDocumentsType;
        vm.initialise = initialise;

        function initialise(jobTitleId) {
            vm.jobTitleId = jobTitleId;
            retrieveCounsellingDocuments();
        }

        function createCounsellingDocument() {
            vm.Errors = [];
            if (vm.documentFile !== undefined && vm.name !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return CounsellingDocumentService.createCounsellingDocument(vm.jobTitleId, vm.name, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#counsellingDocumentModal").modal('hide');
                        vm.documentFile = null;
                        vm.name = "";
                        retrieveCounsellingDocuments();
                    }
                    else {
                        vm.Errors = response.data;
                    }
                });
            }
            else {
                if (vm.name === undefined) {
                    vm.Errors.push('File name required');
                }
                if (vm.documentFile === undefined) {
                    vm.Errors.push('No file available');
                }
            }
        }

        function removeError() {
            vm.Errors.length = 0;
        }

        //function retrieveCounsellingDocuments() {
        //    return DocumentService.retrieveCounsellingDocuments(vm.jobTitleId, vm.paging)
        //        .then(function (response) {
        //            vm.counsellingDocuments = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;                    
        //            return vm.counsellingDocuments;
        //        });
        //}

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

        //function deleteJobTitleDocument(id) {
        //    return CounsellingDocumentService.deleteCounsellingDocument(id)
        //      .then(function () {
        //          retrieveCounsellingDocuments();
        //      });
        //}

        function pageChanged() {
            return retrieveCounsellingDocuments();
        }
                
    }
})();
