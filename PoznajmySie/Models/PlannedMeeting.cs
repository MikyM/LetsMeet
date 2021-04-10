using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.Models
{
    public class PlannedMeeting
    {
        public Guid Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public PlannedMeeting(TimeSpan start, TimeSpan end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
