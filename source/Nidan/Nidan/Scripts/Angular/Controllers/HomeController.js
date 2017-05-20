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
        vm.centres = [];
        vm.retrieveStatistics = retrieveStatistics;
        vm.retrieveStatisticsByCentre = retrieveStatisticsByCentre;
        vm.retrieveBarGraphStatistics = retrieveBarGraphStatistics;
        vm.retrieveBarGraphStatisticsByCentre = retrieveBarGraphStatisticsByCentre;
        vm.retrieveCentres = retrieveCentres;
        vm.change = change;
        vm.NoRecord = false;
        vm.Donut;

        initialise();

        function initialise() {
            retrieveStatistics();
            retrieveBarGraphStatistics();
            retrieveCentres();
        }
        window.Morris.Donut.prototype.setData = function (data, redraw) {
            if (redraw == null) {
                redraw = true;
            }
            this.data = data;
            this.values = (function () {
                var _i, _len, _ref, _results, row;
                _ref = this.data;
                _results = [];
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    row = _ref[_i];
                    _results.push(parseFloat(row.value));
                }
                return _results;
            }).call(this);
            this.dirty = true;
            if (redraw) {
                return this.redraw();
            }
        }
        function retrieveStatistics() {
            return HomeService.retrieveStatistics().then(function (response) {
                vm.statistics = response.data;
                vm.Donut = Morris.Donut({
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
            alert(centreId);
            return HomeService.retrieveStatisticsByCentre(centreId).then(function (response) {
                vm.statistics = response.data;
                if (vm.statistics[0].Value == 0 &&
                    vm.statistics[1].Value == 0 &&
                    vm.statistics[2].Value == 0 &&
                    vm.statistics[4].Value == 0 &&
                    vm.statistics[0].Value == 0) {
                    vm.NoRecord = true;
                } else {
                    vm.NoRecord = false;
                    vm.Donut.setData(
                    [
                        { label: vm.statistics[0].Label, value: vm.statistics[0].Value },
                        { label: vm.statistics[1].Label, value: vm.statistics[1].Value },
                        { label: vm.statistics[2].Label, value: vm.statistics[2].Value },
                        { label: vm.statistics[3].Label, value: vm.statistics[3].Value },
                        { label: vm.statistics[4].Label, value: vm.statistics[4].Value }
                    ]);
                }
            });
        };


        function retrieveBarGraphStatisticsByCentre() {
            return HomeService.retrieveBarGraphStatisticsByCentre(centreId).then(function (response) {
                vm.statistics = response.data;
                var graphData = [
                    { "period": getFormattedDate(vm.statistics[0].Date), "Mobilization": vm.statistics[0].MobilizationCount, "Enquiry": vm.statistics[0].EnquiryCount, "Registration": vm.statistics[0].RegistrationCount, "Admission": vm.statistics[0].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[1].Date), "Mobilization": vm.statistics[1].MobilizationCount, "Enquiry": vm.statistics[1].EnquiryCount, "Registration": vm.statistics[1].RegistrationCount, "Admission": vm.statistics[1].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[2].Date), "Mobilization": vm.statistics[2].MobilizationCount, "Enquiry": vm.statistics[2].EnquiryCount, "Registration": vm.statistics[2].RegistrationCount, "Admission": vm.statistics[2].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[3].Date), "Mobilization": vm.statistics[3].MobilizationCount, "Enquiry": vm.statistics[3].EnquiryCount, "Registration": vm.statistics[3].RegistrationCount, "Admission": vm.statistics[3].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[4].Date), "Mobilization": vm.statistics[4].MobilizationCount, "Enquiry": vm.statistics[4].EnquiryCount, "Registration": vm.statistics[4].RegistrationCount, "Admission": vm.statistics[4].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[5].Date), "Mobilization": vm.statistics[5].MobilizationCount, "Enquiry": vm.statistics[5].EnquiryCount, "Registration": vm.statistics[5].RegistrationCount, "Admission": vm.statistics[5].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[6].Date), "Mobilization": vm.statistics[6].MobilizationCount, "Enquiry": vm.statistics[6].EnquiryCount, "Registration": vm.statistics[6].RegistrationCount, "Admission": vm.statistics[6].AdmissionCount }
                ];
                Morris.Bar({
                    element: 'graph_bar_group',
                    data: graphData,
                    xkey: 'period',
                    barColors: ['#26B99A', '#ff69b4', '#3498DB', '#800080'],
                    ykeys: ['Mobilization', 'Enquiry', 'Registration', 'Admission'],
                    labels: ['Mobilization', 'Enquiry', 'Registration', 'Admission'],
                    hideHover: 'auto',
                    xLabelAngle: 60
                });
            });
        };

        function retrieveBarGraphStatistics() {
            return HomeService.retrieveBarGraphStatistics().then(function (response) {
                vm.statistics = response.data;
                var graphData = [
                    { "period": getFormattedDate(vm.statistics[0].Date), "Mobilization": vm.statistics[0].MobilizationCount, "Enquiry": vm.statistics[0].EnquiryCount, "Registration": vm.statistics[0].RegistrationCount, "Admission": vm.statistics[0].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[1].Date), "Mobilization": vm.statistics[1].MobilizationCount, "Enquiry": vm.statistics[1].EnquiryCount, "Registration": vm.statistics[1].RegistrationCount, "Admission": vm.statistics[1].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[2].Date), "Mobilization": vm.statistics[2].MobilizationCount, "Enquiry": vm.statistics[2].EnquiryCount, "Registration": vm.statistics[2].RegistrationCount, "Admission": vm.statistics[2].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[3].Date), "Mobilization": vm.statistics[3].MobilizationCount, "Enquiry": vm.statistics[3].EnquiryCount, "Registration": vm.statistics[3].RegistrationCount, "Admission": vm.statistics[3].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[4].Date), "Mobilization": vm.statistics[4].MobilizationCount, "Enquiry": vm.statistics[4].EnquiryCount, "Registration": vm.statistics[4].RegistrationCount, "Admission": vm.statistics[4].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[5].Date), "Mobilization": vm.statistics[5].MobilizationCount, "Enquiry": vm.statistics[5].EnquiryCount, "Registration": vm.statistics[5].RegistrationCount, "Admission": vm.statistics[5].AdmissionCount },
                    { "period": getFormattedDate(vm.statistics[6].Date), "Mobilization": vm.statistics[6].MobilizationCount, "Enquiry": vm.statistics[6].EnquiryCount, "Registration": vm.statistics[6].RegistrationCount, "Admission": vm.statistics[6].AdmissionCount }
                ];
                Morris.Bar({
                    element: 'graph_bar_group',
                    data: graphData,
                    xkey: 'period',
                    barColors: ['#26B99A', '#ff69b4', '#3498DB', '#800080'],
                    ykeys: ['Mobilization', 'Enquiry', 'Registration', 'Admission'],
                    labels: ['Mobilization', 'Enquiry', 'Registration', 'Admission'],
                    hideHover: 'auto',
                    xLabelAngle: 60
                });
            });
        };

        function getFormattedDate(dateObject) {
            var d = new Date(dateObject);
            var day = d.getDate();
            var month = d.getMonth() + 1;
            var year = d.getFullYear();
            if (day < 10) {
                day = "0" + day;
            }
            if (month < 10) {
                month = "0" + month;
            }
            var date = day + "/" + month + "/" + year;

            return date;
        };

        function retrieveCentres() {
            return HomeService.retrieveCentres(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.centres = response.data.Items;
                    return vm.centres;
                });
        }

        function change(centreId) {
            retrieveStatisticsByCentre(centreId);
        }
    }

})();
