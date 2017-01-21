using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nidan.Entity.Validation
{
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string _compareTo;
        private readonly bool _allowEqualDates;

        public DateGreaterThanAttribute(string compareTo, bool allowEqualDates = false)
        {
            _compareTo = compareTo;
            _allowEqualDates = allowEqualDates;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class. 
        /// </returns>
        /// <param name="value">The value to validate.</param><param name="validationContext">The context information about the validation operation.</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(_compareTo);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult($"Unknown property {_compareTo}");
            }
            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);
            if (!(value is DateTimeOffset))
            {
                return ValidationResult.Success;
            }
            if (!(propertyTestedValue is DateTimeOffset))
            {
                return ValidationResult.Success;
            }
            if ((DateTimeOffset)value < (DateTimeOffset)propertyTestedValue ||
                (!_allowEqualDates && value == propertyTestedValue))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        /// <param name="metadata">The model metadata.</param><param name="context">The controller context.</param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "dategreaterthan",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };

            rule.ValidationParameters.Add("compareto", _compareTo);
            if (_allowEqualDates)
                rule.ValidationParameters.Add("allowequaldates", "true");

            yield return rule;
        }
    }
}
