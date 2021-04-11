using System;
using System.ComponentModel.DataAnnotations;
using PoznajmySie.CustomValidator;

namespace PoznajmySie.DataTransferObjects
{
    public class WorkingHoursDto
    {
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string Start { get; set; }
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string End { get; set; }
    }
}