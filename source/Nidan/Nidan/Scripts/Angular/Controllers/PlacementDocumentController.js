(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PlacementDocumentController', PlacementDocumentController);

    PlacementDocumentController.$inject = ['$window', 'PlacementDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function PlacementDocumentController($window, PlacementDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.placementDocuments = [];
        vm.placementDocumentsTypes = [];
        vm.documentFile;
        vm.documentTypeId;
        vm.StudentCode;
        vm.Errors = [];
        vm.retrievePlacementDocuments = retrievePlacementDocuments;
        vm.createPlacementDocument = createPlacementDocument;
        vm.downloadPlacementDocument = downloadPlacementDocument;
        vm.removeError = removeError;
        vm.initialise = initialise;

        function initialise(studentCode) {
            vm.StudentCode = studentCode;
            retrievePlacementDocuments(vm.StudentCode);
        }

        function retrievePlacementDocuments(studentCode) {
            vm.StudentCode = studentCode;
            return PlacementDocumentService.retrievePlacementDocuments(vm.StudentCode)
                .then(function (response) {
                    $('#documentDiv').show();
                    vm.placementDocuments = response.data;
                    return vm.placementDocuments;
                });
        }

        function createPlacementDocument(studentCode) {
            vm.Errors = [];
            vm.studentCode = studentCode;
            if (vm.documentFile !== undefined) {
                var documentName = vm.documentFile.name;
                var documentByteString;
                var reader = new FileReader();
                return PlacementDocumentService.createPlacementDocument(vm.studentCode, vm.documentTypeId, vm.documentFile).then(function (response) {
                    if (response.data.length === 0) {
                        $("#PlacementDocumentModal").modal('hide');
                        vm.documentFile = null;
                        retrievePlacementDocuments(vm.studentCode);
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

        function downloadPlacementDocument(guid) {
            return PlacementDocumentService.downloadPlacementDocument(guid);
        }
    }
})();
