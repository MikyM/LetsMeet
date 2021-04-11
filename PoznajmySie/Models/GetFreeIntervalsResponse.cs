using System.Collections.Generic;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Models
{
    public class GetFreeIntervalsResponse
    {
        public List<FreeTimeIntervalDto> Intervals { get; set; }

        public GetFreeIntervalsResponse(List<FreeTimeIntervalDto> freeTimeIntervals)
        {
            this.Intervals = freeTimeIntervals;
        }
    }
}
