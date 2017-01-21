(function (jQuery) {

    'use strict';
    var daterangepickerOptions = {
        autoApply: true,
        singleDatePicker: true,
        showDropdowns: true,
        showCustomRangeLabel: false,
        minDate: jQuery('#period').data("beginDate"),
        maxDate: jQuery('#period').data("endDate"),
        opens: 'left',
        locale: {
            format: 'DD MMMM YYYY'
        }
    };

    jQuery(function () {
        jQuery(".date").daterangepicker(daterangepickerOptions);

        BindApplyDateRangePicker('.date');

        jQuery("#Absence_AbsenceTypeId, input[name='Absence.BeginAbsencePart'], input[name='Absence.EndAbsencePart']").change(function () {
            AbsenceRequest();
        });

        jQuery('.absencePeriod').change(function () {
            window.location.href = "/Absence/Create/" + jQuery('#Absence_PersonnelId').val() + "/" + jQuery(this).val();
        });

        jQuery('.begin .date').data('daterangepicker').setStartDate(jQuery("#Absence_BeginDateUtc").val());
        jQuery('.end .date').data('daterangepicker').setStartDate(jQuery("#Absence_EndDateUtc").val());

        AbsenceRequest();

    });

    function ApplyEndDateDateRange(thisDatePicker) {
        var daterangepickerOptionsExtended = {
            minDate: thisDatePicker.val(),
            maxDate: jQuery('#period').data("endDate"),
            startDate: jQuery("#Absence_EndDateUtc").val()
        };

        if (thisDatePicker.parent().hasClass('begin')) {
            jQuery('.end .date').daterangepicker(jQuery.extend(daterangepickerOptions, daterangepickerOptionsExtended));
            BindApplyDateRangePicker('.end .date');
        }
    }

    function BindApplyDateRangePicker(selector) {
        jQuery(selector).on('apply.daterangepicker', function (ev, picker) {
            ApplyEndDateDateRange(jQuery(this));
            AbsenceRequest();
        });
    }

    function AbsenceRequest() {

        var postData = {
            AbsenceId: jQuery("#Absence_AbsenceId").val(),
            PersonnelAbsenceEntitlementId: jQuery("#Absence_PersonnelAbsenceEntitlementId").val(),
            OrganisationId: jQuery("#Absence_OrganisationId").val(),
            PersonnelId: jQuery("#Absence_PersonnelId").val(),
            CountryId: jQuery("#Absence_CountryId").val(),
            DivisionId: jQuery("#Absence_DivisionId").val(),
            AbsencePeriodId: jQuery("#Absence_AbsencePeriodId").val(),
            AbsenceTypeId: jQuery("#Absence_AbsenceTypeId").val(),
            BeginDateUtc: jQuery("#Absence_BeginDateUtc").val(),
            BeginAbsencePart: jQuery("input[name='Absence.BeginAbsencePart']:checked").val(),
            EndDateUtc: jQuery("#Absence_EndDateUtc").val(),
            EndAbsencePart: jQuery("input[name='Absence.EndAbsencePart']:checked").val()
        };
        
        var posting = jQuery.ajax(
            {
                type: "POST",
                url: "/Absence/AbsenceRequest",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: 'application/json; charset=utf-8'
            })
            .done(function (response) {
                if (!jQuery.isEmptyObject(response)) {
                    jQuery("#absenceDaysTableBody").empty();
                    jQuery.each(response.AbsenceDays, function (i, item) {
                        var jQuerytr = jQuery('<tr>').append(
                            jQuery('<td>', { "data-title": "Date" }).append(jQuery('<span>').text(function () { return moment(item.Date).format("dddd Do MMMM YYYY"); })),
                            jQuery('<td>', { "data-title": "AM" }).append(jQuery('<span>').html(function () { return DisplayCheckIcon(item.AM); })),
                            jQuery('<td>', { "data-title": "PM" }).append(jQuery('<span>').html(function () { return DisplayCheckIcon(item.PM); })),
                            jQuery('<td>', { "data-title": "Can be booked" }).append(jQuery('<span>').html(function () { return DisplayCheckIcon(item.CanBeBookedAsAbsence); })),
                            jQuery('<td>', { "data-title": "Reason" }).append(jQuery('<span>').text(item.Validation))
                        ).appendTo('#absenceDaysTableBody');

                    });
                    jQuery("#absence-report").removeClass("hidden");

                    jQuery("#Absence_PersonnelAbsenceEntitlementId").val(response.PersonnelAbsenceEntitlement.PersonnelAbsenceEntitlementId);
                    jQuery("#period")
                        .text(response.PersonnelAbsenceEntitlement.Period)
                        .data("endDate", moment(response.PersonnelAbsenceEntitlement.EndDate).format("DD MMMM YYYY"));

                    ApplyEndDateDateRange(jQuery('.begin .date'));

                    if (response.PersonnelAbsenceEntitlement.Entitlement) {                        
                        jQuery("#entitlement-remaining").text(response.PersonnelAbsenceEntitlement.Remaining);
                        jQuery("#after-booking").text(response.PersonnelAbsenceEntitlement.Remaining - response.Duration);
                        jQuery("#entitlement-details").show();
                    }
                    else {
                        jQuery("#entitlement-details").hide();
                    }

                    // Enable or disable submit button
                    jQuery("#submit").prop("disabled", function () {
                        return response.Duration === 0 && response.PersonnelAbsenceEntitlement.Entitlement && response.PersonnelAbsenceEntitlement.Remaining - response.Duration >= 0;
                    });
                }
            });
    }

    function DisplayCheckIcon(bool) {
        return bool ? '<i class="fa fa-check fa-fw"></i>' : '';
    }
})(window.jQuery);