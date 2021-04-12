using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SpotkajmySie.CustomValidator;

namespace SpotkajmySie.DataTransferObjects
{
    public class PlannedMeetingDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        [TimeSpanLessThan("End")]
        public string Start { get; set; }
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string End { get; set; }
    }
}