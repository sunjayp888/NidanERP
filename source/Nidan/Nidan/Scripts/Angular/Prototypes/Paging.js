(function () {
    'use strict';

    angular
        .module('Nidan')
        .factory('Paging', Paging);

    function Paging() {

        /// Constructor
        function Paging() {
            this.page = 1;
            this.pageSize = 10;
            this.totalPages = 0;
            this.totalResults = 0;
            this.pageSizeOptions = [10, 50, 100, 200];
            this.pagingInformation = pagingInformation;
        }

        return Paging;

        function pagingInformation() {
            var pageStartNumber = this.totalResults === 0
                ? 0
                : (this.page - 1) * this.pageSize + 1;

            var pageEndNumber = this.page * this.pageSize > this.totalResults
                ? this.totalResults
                : this.page * this.pageSize;

            return "Showing " + pageStartNumber + " to " + pageEndNumber + " of " + this.totalResults + " entries";
        }

    }
})();