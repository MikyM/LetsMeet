using System;
using System.ComponentModel.DataAnnotations;
using SpotkajmySie.CustomValidator;

namespace SpotkajmySie.DataTransferObjects
{
    public class WorkingHoursDto
    {
        [Required]
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        [TimeSpanLessThan("End")]
        public string Start { get; set; }
        [TimeSpan(ErrorMessage = "This is not a valid TimeSpan value")]
        public string End { get; set; }
    }
}