(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('CentreController', CentreController);

    CentreController.$inject = ['$window', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CentreController($window, CentreService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.centres = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCentre = editCentre;
        vm.viewCentre = viewCentre;
        vm.courseInstallments = [];
        vm.retrieveCourseInstallments = retrieveCourseInstallments;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveCentres() {
            return CentreService.retrieveCentres(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.centres = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.centres;
                });
        }

        function pageChanged() {
            return retrieveCentres();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCentres();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCentre(id) {
            $window.location.href = "/Centre/Edit/" + id;
        }

        function retrieveCourseInstallments(courseId) {
            return CentreService.retrieveCourseInstallments(courseId).then(function () {
                vm.courseInstallments = response.data;
            });
        };

        function viewCentre(centreId) {
            $window.location.href = "/Centre/Edit/" + centreId;
        }
    }

})();
