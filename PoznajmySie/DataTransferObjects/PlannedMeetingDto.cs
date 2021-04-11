using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using PoznajmySie.CustomValidator;

namespace PoznajmySie.DataTransferObjects
{
    public class PlannedMeetingDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string Start { get; set; }
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string End { get; set; }
    }
}