using System.Collections.Generic;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Models
{
    public class FreeIntervalsResponse
    {
        public List<FreeTimeIntervalDto> Intervals { get; set; }

        public FreeIntervalsResponse(List<FreeTimeIntervalDto> freeTimeIntervals)
        {
            this.Intervals = freeTimeIntervals;
        }
    }
}
