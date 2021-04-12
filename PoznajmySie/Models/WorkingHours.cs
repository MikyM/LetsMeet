using System;

namespace PoznajmySie.Models
{
    public class WorkingHours
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public WorkingHours(TimeSpan start, TimeSpan end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
