(function() {
    'use strict';

    angular
        .module('Nidan')
        .directive('fileModel', fileModel);

    fileModel.$inject = ['$parse','$filter'];

    function fileModel($parse, $filter) {
        // Usage:
        //     <file-Model="angular model"></fileModel>
        // http://angularcode.com/simple-file-upload-example-using-angularjs/
        // 
        var directive = {
            link: link,
            restrict: 'A'
        };

        return directive;

        function link(fileModel, element, attrs, controller, $transclude) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            //we can put this as central variable if there are other upload function
            // other
            var allowedFileType = [
                { FileType : 'text/plain', FileExtension : '.txt'},
                { FileType : 'application/pdf', FileExtension : '.pdf'},
                { FileType : 'application/msword', FileExtension : '.doc'},
                { FileType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', FileExtension: '.docx' },
                { FileType: 'application/vnd.ms-excel', FileExtension: '.xls' },
                { FileType: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', FileExtension: '.xlsx' },
                { FileType : 'image/jpeg', FileExtension : '.jpg'},
                { FileType : 'image/png', FileExtension : '.png'},
                { FileType : 'image/gif', FileExtension : '.gif'},

            ];
            element.bind('change', function () {
                fileModel.$apply(function () {
                    var file = element[0].files[0];
                    var isAllowedFile = $filter('filter')(allowedFileType, { FileType: file.type }, true).length > 0;
                    //4194304 this is 4mb
                    var fileSizeLimit = 4194304;
                    if (isAllowedFile && file.size <= fileSizeLimit) {
                        modelSetter(fileModel, file);
                    }
                    else {
                        fileModel.model.Errors = [];
                        if (!isAllowedFile) {
                            fileModel.model.Errors.push('File type ' + file.type + ' is not allowed');
                        }
                        if (file.size >= fileSizeLimit) {
                            fileModel.model.Errors.push('File more than 4mb is not allowed.');
                        }
                        element.val(null);
                    }
                });
            });

            fileModel.$watch(function (newValue) {
                if (newValue.model.documentFile === null) {
                    element.val(null);
                }
            });

        }
    }

})();