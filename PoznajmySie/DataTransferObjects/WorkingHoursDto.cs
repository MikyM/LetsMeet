using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoznajmySie.DataTransferObjects
{
    public class WorkingHoursDto
    {
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
    }
}