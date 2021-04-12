using SpotkajmySie.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SpotkajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class WithinWorkingHoursAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public WithinWorkingHoursAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var meetingList = value as List<PlannedMeetingDto>;

            if (meetingList is null) {
                return ValidationResult.Success;
            }

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null) {
                throw new ArgumentException("Property with this name was not found");
            }

            var workingHours = property.GetValue(validationContext.ObjectInstance) as WorkingHoursDto;

            if (meetingList.Any(y => TimeSpan.Parse(y.Start) < TimeSpan.Parse(workingHours.Start) || TimeSpan.Parse(y.Start) > TimeSpan.Parse(workingHours.End))) {
                return new ValidationResult("Meeting start time outside of working hours");
            }
            if (meetingList.Any(y => TimeSpan.Parse(y.End) > TimeSpan.Parse(workingHours.End) || TimeSpan.Parse(y.End) < TimeSpan.Parse(workingHours.Start))) {
                return new ValidationResult("Meeting end time outside of working hours");
            }
            return ValidationResult.Success;
        }
    }
}