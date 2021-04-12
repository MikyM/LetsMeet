using PoznajmySie.DataTransferObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PoznajmySie.CustomValidator;

namespace PoznajmySie.Models
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
