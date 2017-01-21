(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('OrganisationalChartService', OrganisationalChartService);

    OrganisationalChartService.$inject = ['$http'];

    function OrganisationalChartService($http) {
        var service = {
            retrieveOrganisationalChart: retrieveOrganisationalChart,
            retrieveOrganisationalChartViewModel: retrieveOrganisationalChartViewModel
        };

        return service;

        function retrieveOrganisationalChart(companyIds, departmentIds, divisionIds, colourBy) {
            var url = "/OrganisationalChart/List",
                data = {
                    companyIds: companyIds,
                    departmentIds: departmentIds,
                    divisionIds: divisionIds,
                    showColourBy: colourBy
                };

            return $http.post(url, data);
        }

        function retrieveOrganisationalChartViewModel() {

            var url = "/OrganisationalChart/OrganisationalChartViewModel";

            return $http.post(url);
        }

    }
})();