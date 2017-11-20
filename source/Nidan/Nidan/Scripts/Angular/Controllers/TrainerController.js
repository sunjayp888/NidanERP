(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('TrainerController', TrainerController);

    TrainerController.$inject = ['$window', 'TrainerService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function TrainerController($window, TrainerService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.trainers = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editTrainer = editTrainer;
        vm.searchTrainer = searchTrainer;
        vm.viewTrainer = viewTrainer;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.courses = [];
        vm.retrieveCourses = retrieveCourses;
        vm.disticts = [];
        vm.retrieveDistricts = retrieveDistricts;
        vm.talukas = [];
        vm.retrieveTalukas = retrieveTalukas;
        vm.deleteSubjectTrainer = deleteSubjectTrainer;
        vm.deleteBatchTrainer = deleteBatchTrainer;
        initialise();

        function initialise() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "desc";
            order("CreatedDate");
        }

        function retrieveTrainers() {
            return TrainerService.retrieveTrainers(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.trainers = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.trainers;
                });
        }

        function searchTrainer(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return TrainerService.searchTrainer(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.trainers = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.trainers.length === 0 ? "No Records Found" : "";
                  return vm.trainers;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                searchTrainer(vm.searchKeyword);
            } else {
                return retrieveTrainers();
            }
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveTrainers();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editTrainer(id) {
            $window.location.href = "/Trainer/Edit/" + id;
        }

        function viewTrainer(trainerId) {
            $window.location.href = "/Trainer/View/" + trainerId;
        }

        function retrieveDistricts(stateId) {
            return TrainerService.retrieveDistricts(stateId).then(function () {
                vm.disticts = response.data;
            });
        };

        function retrieveTalukas(districtId) {
            return TrainerService.retrieveTalukas(districtId).then(function () {
                vm.talukas = response.data;
            });
        };

        function retrieveCourses(sectorId) {
            return TrainerService.retrieveCourses(sectorId).then(function () {
                vm.courses = response.data;
            });
        };

        function deleteSubjectTrainer(subjectId, $item) {
            return TrainerService.deleteSubjectTrainer(subjectId, $item.TrainerId)
              .then(function () {
              });
        }

        function deleteBatchTrainer(batchId, $item) {
            return TrainerService.deleteBatchTrainer(batchId, $item.TrainerId)
              .then(function () {
              });
        }
    }

})();
