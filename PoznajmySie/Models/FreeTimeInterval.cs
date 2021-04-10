using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.Models
{
    public class FreeTimeInterval
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public FreeTimeInterval(TimeSpan start, TimeSpan end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
