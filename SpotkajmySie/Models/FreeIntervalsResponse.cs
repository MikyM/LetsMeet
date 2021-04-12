using System.Collections.Generic;
using SpotkajmySie.DataTransferObjects;

namespace SpotkajmySie.Models
{
    public class FreeIntervalsResponse
    {
        public List<FreeTimeIntervalDto> FreeIntervals { get; set; }

        public FreeIntervalsResponse(List<FreeTimeIntervalDto> freeTimeIntervals)
        {
            this.FreeIntervals = freeTimeIntervals;
        }
    }
}
