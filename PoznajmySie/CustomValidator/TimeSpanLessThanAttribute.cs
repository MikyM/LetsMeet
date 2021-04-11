using System;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TimeSpanLessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public TimeSpanLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            if(!TimeSpan.TryParse(value as string, out TimeSpan currentValue))
            {
                return ValidationResult.Success;
            }

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name was not found");
            }

            if (!TimeSpan.TryParse(property.GetValue(validationContext.ObjectInstance) as string, out TimeSpan comparisonValue))
            {
                return ValidationResult.Success;
            }

            if (currentValue >= comparisonValue)
            {
                return new ValidationResult("End value must be greater than Start value");
            }
            return ValidationResult.Success;
        }
    }
}