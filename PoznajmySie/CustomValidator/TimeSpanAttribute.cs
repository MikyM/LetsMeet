using System;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TimeSpanAttribute : ValidationAttribute
    {
        public TimeSpanAttribute() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            string strValue = value as string;
            if (!string.IsNullOrEmpty(strValue)) {
                if (!TimeSpan.TryParse(strValue, out TimeSpan result) || result.TotalHours > 24 || result.TotalHours < 0) {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
