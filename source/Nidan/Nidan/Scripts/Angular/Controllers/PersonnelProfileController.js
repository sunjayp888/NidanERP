(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('PersonnelProfileController', PersonnelProfileController);

    PersonnelProfileController.$inject = ['$window', 'PersonnelProfileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function PersonnelProfileController($window, PersonnelProfileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.admissions = [];
        vm.batches = [];
        vm.personnelId;
        vm.initialise = initialise;
        vm.uploadPhoto = uploadPhoto;
        vm.deletePhoto = deletePhoto;
        vm.retrieveBatches = retrieveBatches;
        vm.retrieveAdmissions = retrieveAdmissions;

        var cropImage


        //Cropper
        var cropImage = $('#UploadProfilePicture');
        var options = {
            aspectRatio: 1 / 1,
            responsive: true,
            crop: function (e) {
            }
        };

        $('#inputImage').change(function () {
            var files = this.files;
            var file;

            if (!cropImage.data('cropper')) {
                return;
            }

            if (files && files.length) {
                file = files[0];
                if (/^image\/\w+$/.test(file.type)) {
                    var blobURL = URL.createObjectURL(file);
                    cropImage.one('built.cropper', function () {
                        URL.revokeObjectURL(blobURL);
                    }).cropper('reset').cropper('replace', blobURL);
                } else {
                    window.alert('Please choose an image file.');
                }
            }
        });
        //Cropper

        function initialise(personnelId) {
            vm.personnelId = personnelId;
            cropImage.on({
            }).cropper(options);
        }

        function uploadPhoto() {
            cropImage.cropper('getCroppedCanvas').toBlob(function (blob) {
                return PersonnelProfileService.UploadPhoto(vm.personnelId, blob)
                        .then(function (response) {
                            $('#ProfilePicture').attr('src', URL.createObjectURL(blob));
                            $("#ProfilePictureModal").modal("hide")
                        });
            });
        }

        function deletePhoto() {
            return PersonnelProfileService.DeletePhoto(vm.personnelId)
                    .then(function (response) {
                        document.getElementById('ProfilePicture').setAttribute('src', location.protocol + '//' + location.host + '/Content/images/user.png');
                        $("#ProfilePictureModal").modal("hide")
                    });
        }

        function retrieveAdmissions(personnelId) {
            vm.personnelId = personnelId;
            return PersonnelProfileService.retrieveAdmissions(vm.personnelId)
                .then(function (response) {
                    vm.admissions = response.data.Items;
                    return vm.admissions;
                });
        }

        function retrieveBatches(personnelId) {
            vm.personnelId = personnelId;
            return PersonnelProfileService.retrieveBatches(vm.personnelId)
                .then(function (response) {
                    vm.batches = response.data.Items;
                    return vm.batches;
                });
        }
    }
})();
