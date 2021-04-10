using PoznajmySie.DataTransferObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.Models
{
    public class CalendarCompareRequest
    {
        [Required]
        public string MeetingDuration { get; set; }
        [Required]
        public List<CalendarDto> Calendars {get;set;}
    }
}
