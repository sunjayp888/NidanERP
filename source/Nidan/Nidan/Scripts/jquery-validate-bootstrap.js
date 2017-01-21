(function ($) {
    var defaultOptions = {
        validClass: 'has-success',
        errorClass: 'has-error',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(validClass)
                .addClass('has-error');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
            .removeClass('has-error')
            .addClass(validClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };

    $('input[data-val=true]').on('blur', function () {
        $(this).valid();
    });

    initValidators();

    function initValidators() {
        if ($.validator && $.validator.unobtrusive) {
            $.validator.addMethod("phone", function (value, element) {
                if (this.optional(element)) {
                    return true;
                }
                var reverseValue = $.trim(value).split("").reverse().join("");
                var reverseRegEx = new RegExp("^(\\d+\\s?(x|\\.txe?)\\s?)?((\\)(\\d+[\\s\\-\\.]?)?\\d+\\(|\\d+)[\\s\\-\\.]?)*(\\)([\\s\\-\\.]?\\d+)?\\d+\\+?\\((?!\\+.*)|\\d+)(\\s?\\+)?$", "i");
                var match = reverseRegEx.exec(reverseValue);
                return (match && (match.index === 0) && (match[0].length === value.length));
            });
            $.validator.unobtrusive.adapters.addBool("phone");

            $.validator.addMethod('dategreaterthan', function (value, element, params) {
                value = Date.parse(value);
                var elemCompareTo = $('[id*="_' + params.compareTo + '"]');
                var otherDate = Date.parse(elemCompareTo.val());
                if (isNaN(value) || isNaN(otherDate))
                    return true;
                return value > otherDate || (value == otherDate && params.allowEqualDates);
            });
            $.validator.unobtrusive.adapters.add('dategreaterthan', ['compareto', 'allowequaldates'], function (options) {
                options.rules['dategreaterthan'] = {
                    'allowEqualDates': options.params['allowequaldates'],
                    'compareTo': options.params['compareto']
                };
                options.messages['dategreaterthan'] = options.message;
            });
        }
    }
})(jQuery);