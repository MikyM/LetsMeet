using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.DataTransferObjects
{
    public class PlannedMeetingDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
    }
}