using System.Collections.Generic;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Models
{
    public class CalendarCompareResponse
    {
        public List<FreeTimeIntervalDto> Intervals { get; set; }

        public CalendarCompareResponse(List<FreeTimeIntervalDto> freeTimeIntervals)
        {
            this.Intervals = freeTimeIntervals;
        }
    }
}
