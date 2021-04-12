using System;

namespace SpotkajmySie.Models
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
