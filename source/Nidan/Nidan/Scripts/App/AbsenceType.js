(function (jQuery) {


    jQuery(function () {
        var hasEntitlement = jQuery("#DivisionCountryAbsenceTypeEntitlement_HasEntitlement");

        disableEntitlement(!hasEntitlement.prop("checked"));

        hasEntitlement.on("click", function () {
            disableEntitlement(!jQuery(this).prop("checked"));
        });

    });

    function disableEntitlement(disabled) {
        var frequencyId = jQuery("#DivisionCountryAbsenceTypeEntitlement_FrequencyId"),
            entitlement = jQuery("#DivisionCountryAbsenceTypeEntitlement_Entitlement"),
            maximumCarryForward = jQuery("#DivisionCountryAbsenceTypeEntitlement_MaximumCarryForward");
 
        frequencyId.prop("disabled", disabled);
        entitlement.prop("disabled", disabled);
        maximumCarryForward.prop("disabled", disabled);
       
        if (!disabled) {
            frequencyId.removeProp("disabled");
            entitlement.removeProp("disabled");
            maximumCarryForward.removeProp("disabled");
        }
    }

    jQuery("#DivisionCountryAbsenceTypeEntitlement_FrequencyId").on("change", function () {
        jQuery("#FrequencyId").val($(this).val());
    });

})(window.jQuery);