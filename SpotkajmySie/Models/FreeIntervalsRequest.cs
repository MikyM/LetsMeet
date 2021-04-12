using SpotkajmySie.DataTransferObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SpotkajmySie.CustomValidator;

namespace SpotkajmySie.Models
{
    public class FreeIntervalsRequest
    {
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string MeetingDuration { get; set; }
        [Required]
        public List<CalendarDto> Calendars {get;set;}
    }
}
