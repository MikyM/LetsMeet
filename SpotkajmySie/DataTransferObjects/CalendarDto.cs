using SpotkajmySie.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpotkajmySie.DataTransferObjects
{
    public class CalendarDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required, ValidateObject]
        public WorkingHoursDto WorkingHours { get; set; }
        [WithinWorkingHours("WorkingHours"), ArentOverlapping, ValidateObject]
        public List<PlannedMeetingDto> PlannedMeetings { get; set; }
    }
}
