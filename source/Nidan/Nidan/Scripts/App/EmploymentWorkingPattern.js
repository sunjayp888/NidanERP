(function (jQuery) {

    'use strict';

    function displayWorkingDays() {

        jQuery("#workingDays input:checkbox").prop("disabled", function () {
            return !jQuery("#overrideWorkingDays").is(':checked');
        });

        jQuery("#submit").prop("disabled", function () {
            return jQuery("#workingDays").html().length === 0;
        });


        if (jQuery("#workingDays").html.length === 0) {
            jQuery("#employmentError")
                .addClass("row alert alert-error")
                .html("<ul><li>Unable to find the default working pattern.</li></ul>")
                .show();
            jQuery("#employmentError")
                .parent()
                .closest(".row")
                .removeClass("hidden");
        }
        else {
            if(jQuery('#ModelStateValue').val())
            {
            jQuery("#employmentError")
                .hide()
                .parent()
                .closest(".row")
                .addClass("hidden");
                   }
        }
    }

    function loadWorkingDays() {

        if (!jQuery("#overrideWorkingDays").is(':checked')) {

            var divisionBuildingId = jQuery("#DivisionBuildingSelectedId").val();
            var dataValue = divisionBuildingId.split("#");
            var divisionId = dataValue[0],
            buildingId = dataValue[1];
            jQuery("#EmploymentViewModel_Employment_DivisionId").val(divisionId);
            jQuery("#EmploymentViewModel_Employment_BuildingId").val(buildingId);

            jQuery.ajax({
                url: '/Employment/GetWorkingPatternRecord?divisionId=' + divisionId + '&buildingId=' + buildingId + '&htmlFieldPrefix=EmploymentViewModel.WorkingPatternDays',
                type: 'GET',
                data: "",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    jQuery("#workingDays").html(data);
                    displayWorkingDays();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    jQuery("#workingDays").empty();
                    displayWorkingDays();
                }
            });
        }
        else {
            displayWorkingDays();
        }

    }

    jQuery(function () {

        jQuery(".begin .date, .dob .date").daterangepicker({
            autoApply: true,
            singleDatePicker: true,
            showDropdowns: true,
            showCustomRangeLabel: false,
            opens: 'left',
            locale: {
                format: 'DD MMMM YYYY'
            }
        });

        $(".end .date, .termination .date, .previousemploymentend .date").daterangepicker({
            autoApply: true,
            singleDatePicker: true,
            autoUpdateInput: false,
            showCustomRangeLabel: false,
            showDropdowns: true,
            opens: 'left',
            locale: {
                format: 'DD MMMM YYYY'
            }
        });
        $(".end .date, .termination .date, .previousemploymentend .date").on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD MMMM YYYY'));
        });

        jQuery("#overrideWorkingDays").on("click", function () {
            loadWorkingDays();
        });

        //jQuery("#EmploymentViewModel_DivisionBuildingId, #DivisionBuildingId").on("change", function () {
        //    loadWorkingDays();
        //});

        loadWorkingDays();
    });
})(window.jQuery);