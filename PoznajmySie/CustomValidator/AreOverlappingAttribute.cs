using PoznajmySie.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PoznajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class ArentOverlappingAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var meetingList = value as List<PlannedMeetingDto>;

            if (meetingList is null) {
                return ValidationResult.Success;
            }

            TimeSpan start = new TimeSpan();
            TimeSpan end = new TimeSpan();

            foreach (PlannedMeetingDto meeting in meetingList) {
                start = TimeSpan.Parse(meeting.Start);
                end = TimeSpan.Parse(meeting.End);

                if (meetingList.Any(y => !(TimeSpan.Parse(y.Start) >= end || TimeSpan.Parse(y.End) <= start) && TimeSpan.Parse(y.Start) != start && TimeSpan.Parse(y.End) != end)) {
                    return new ValidationResult("Calendar has overlapping meetings");
                }
            }

            return ValidationResult.Success;
        }
    }
}