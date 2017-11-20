(function () {
    'use strict';

    angular
        .module('Nidan')
        .controller('ExpenseController', ExpenseController);

    ExpenseController.$inject = ['$window', 'ExpenseService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function ExpenseController($window, ExpenseService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.expenses = [];
        vm.centres = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editExpense = editExpense;
        //vm.canDeleteExpense = canDeleteExpense;
        vm.deleteExpense = deleteExpense;
        vm.searchExpense = searchExpense;
        vm.viewExpense = viewExpense;
        //vm.addExpense = addExpense;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.retrieveExpensesByCashMemo = retrieveExpensesByCashMemo;
        vm.initialise = initialise;
        vm.cashMemo = "";
        vm.isExpenseLimitExceed = "False";
        vm.isDateInCurrentWeek = null;
        vm.expenseLimitCheck = expenseLimitCheck;
        vm.dateInCurrentWeek = dateInCurrentWeek;
        vm.expenseHeaderId = "";
        vm.setDefaultExpenseHeaderId = setDefaultExpenseHeaderId;
        vm.testcalender = testcalender;
        vm.retrieveCentres = retrieveCentres;
        vm.centreId;
        vm.searchExpenseByDate = searchExpenseByDate;

        function initialise() {
            vm.orderBy.property = "ExpenseGeneratedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("ExpenseGeneratedDate");
            retrieveCentres();
        }

        function retrieveExpenses() {
            return ExpenseService.retrieveExpenses(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.expenses;
                });
        }

        function retrieveExpensesByCashMemo(cashMemo) {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            vm.cashMemo = cashMemo == undefined ? $("#Expense_CashMemoNumbers").val() : cashMemo;
            return ExpenseService.retrieveExpensesByCashMemo(vm.cashMemo, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    return vm.expenses;
                });
        }

        function searchExpense(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ExpenseService.searchExpense(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.expenses.length === 0 ? "No Records Found" : "";
                    return vm.expenses;
                });
        }

        function searchExpenseByDate(fromDate, toDate, centreId) {
            vm.fromDate = fromDate;
            vm.toDate = toDate;
            vm.centreId = centreId;
            vm.orderBy.property = "ExpenseGeneratedDate";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            //vm.batchId = batchId;
            return ExpenseService.searchExpenseByDate(vm.fromDate, vm.toDate, vm.centreId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.expenses = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.expenses.length === 0 ? "No Records Found" : "";
                    return vm.expenses;
                });
        }

        function pageChanged() {
            return retrieveExpenses();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveExpenses();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editExpense(id) {
            $window.location.href = "/Expense/Edit/" + id;
        }

        //function canDeleteOtherFee(id) {
        //    vm.loadingActions = true;
        //    vm.CanDeleteOtherFee = false;
        //    $('.dropdown-menu').slideUp('fast');
        //    $('.' + id).toggle();
        //    OtherFeeService.canDeleteOtherFee(id).then(function (response) { vm.CanDeleteOtherFee = response.data, vm.loadingActions = false });
        //}

        function viewExpense(cashMemo) {
            $window.location.href = "/Expense/Create/" + cashMemo;
        }

        function deleteExpense(centreId, expenseId, cashMemo) {
            return ExpenseService.deleteExpense(centreId, expenseId).then(function () {
                retrieveExpensesByCashMemo(cashMemo);
            });
        }

        function expenseLimitCheck() {
            return ExpenseService.expenseLimitCheck(vm.expenseHeaderId).then(function (response) {
                vm.isExpenseLimitExceed = response.data;
                testcalender();
                return vm.isExpenseLimitExceed;
            });
        } 
        function dateInCurrentWeek(expenseId) {
            return ExpenseService.dateInCurrentWeek(expenseId).then(function (response) {
                vm.isDateInCurrentWeek = response.data;
            });
        }

        function setDefaultExpenseHeaderId(expenseHeaderId) {
            vm.expenseHeaderId = expenseHeaderId;
            if (expenseHeaderId != 0) {
                //$("#slc option[value=3]").prop('selected', 'selected');
                //$('#Expense_ExpenseHeaderId option').eq(expenseHeaderId).prop('selected', true);
            }
        }

        function retrieveCentres() {
            return ExpenseService.retrieveCentres().then(function (response) {
                vm.centres = response.data;
                return vm.centres;
            });
        }

        function testcalender () {
            var today = new Date();
            var day = today.getDay();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            var daterangepickerOptions = {
                autoApply: true,
                singleDatePicker: true,
                showDropdowns: true,
                showCustomRangeLabel: false,
                opens: 'left',
                //minDate: moment(),
                minDate: day == 1 || day == 7 ? moment().subtract('0', 'days').format('DD MMMM YYYY') : day == 2 ? moment().subtract('1', 'days').format('DD MMMM YYYY') : day == 3 ? moment().subtract('2', 'days').format('DD MMMM YYYY') : day == 4 ? moment().subtract('3', 'days').format('DD MMMM YYYY') : day == 5 ? moment().subtract('4', 'days').format('DD MMMM YYYY') : moment().subtract('5', 'days').format('DD MMMM YYYY'),
                maxDate: day == 1 ? moment().add('5', 'days').format('DD MMMM YYYY') : day == 2 ? moment().add('4', 'days').format('DD MMMM YYYY') : day == 3 ? moment().add('3', 'days').format('DD MMMM YYYY') : day == 4 ? moment().add('2', 'days').format('DD MMMM YYYY') : day == 5 ? moment().add('1', 'days').format('DD MMMM YYYY') : day == 6 ? moment().add('0', 'days').format('DD MMMM YYYY') : moment().add('6', 'days').format('DD MMMM YYYY'),
                onSelect: function () {
                    selectedDate = moment().format('DD MMMM YYYY');
                },
                locale: {
                    "format": "DD MMMM YYYY"
                }
            };

            jQuery(function () {
                jQuery(".date").daterangepicker(daterangepickerOptions);
                $('#GeneratedDate').val(moment().format('DD MMMM YYYY'));
                $(".generated .date").on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('DD MMMM YYYY'));
                });
            });
        };
    }

})();
