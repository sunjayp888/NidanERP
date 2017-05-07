(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$window', 'HomeService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function HomeController($window, HomeService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.statistics = [];
        vm.statisticsByCentre = [];
        vm.retrieveStatistics = retrieveStatistics;
        vm.retrieveStatisticsByCentre = retrieveStatisticsByCentre;
        initialise();

        function initialise() {
            retrieveStatistics();
            retrieveStatisticsByCentre();
        }

        function retrieveStatistics() {
            return HomeService.retrieveStatistics().then(function (response) {
                vm.statistics = response.data;
                Morris.Donut({
                    element: 'graph_donut',
                    data: [
                        { label: vm.statistics[0].Label, value: vm.statistics[0].Value },
                        { label: vm.statistics[1].Label, value: vm.statistics[1].Value },
                        { label: vm.statistics[2].Label, value: vm.statistics[2].Value },
                        { label: vm.statistics[3].Label, value: vm.statistics[3].Value },
                        { label: vm.statistics[4].Label, value: vm.statistics[4].Value }
                    ],
                    colors: ['#26B99A', '#FF69B4', '#800080', '#3498DB', '#FFA500'],
                    formatter: function (y) {
                        return y;
                    }
                });
            });
        };

        function retrieveStatisticsByCentre(centreId) {
            return HomeService.retrieveStatisticsByCentre(centreId).then(function (response) {
                vm.statisticsByCentre = response.data;
            });
        };
    }

})();
