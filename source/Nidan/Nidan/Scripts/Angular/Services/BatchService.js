(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('BatchService', BatchService);

    BatchService.$inject = ['$http'];

    function BatchService($http) {
        var service = {
            retrieveBatches: retrieveBatches,
            //canDeleteMobilization: canDeleteMobilization,
            //deleteMobilization: deleteMobilization,
            //searchMobilization: searchMobilization
        };

        return service;

        function retrieveBatches(Paging, OrderBy) {

            var url = "/Batch/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        //function searchMobilization(SearchKeyword, Paging, OrderBy) {
        //    var url = "/Mobilization/Search",
        //    data = {
        //        searchKeyword: SearchKeyword,
        //        paging: Paging,
        //        orderBy: new Array(OrderBy)
        //    };

        //    return $http.post(url, data);
        //}

        //function canDeleteMobilization(id) {
        //    var url = "/Mobilization/CanDeleteMobilization",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}

        //function deleteMobilization(id) {
        //    var url = "/Mobilization/Delete",
        //        data = { id: id };

        //    return $http.post(url, data);
        //}
    }
})();