using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Santiago.Web.Attributes.ValidationAttributes.Common
{
    public class NotEqualAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "Значение этого поля не должно быть равно значению поля {0}";

        public string PropertyForComparisonName { get; set; }

        public NotEqualAttribute(string propertyForComparisonName)
        {
            PropertyForComparisonName = propertyForComparisonName;
        }

        public override string FormatErrorMessage(string displayName)
        {
            return string.Format(CultureInfo.InvariantCulture, !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : DefaultErrorMessage, displayName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyForComparison = validationContext.ObjectType.GetProperty(PropertyForComparisonName);
            var propertyForComparisonValue = propertyForComparison.GetValue(validationContext.ObjectInstance, null);

            return Equals(value, propertyForComparisonValue) ? new ValidationResult(FormatErrorMessage(PropertyForComparisonName)) : ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "notequal",
                ErrorMessage = FormatErrorMessage(PropertyForComparisonName)
            };

            rule.ValidationParameters["propertyforcomparison"] = PropertyForComparisonName;

            yield return rule;
        }
    }
}