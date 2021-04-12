using System;
using System.Collections.Generic;
using SpotkajmySie.DataTransferObjects;

namespace SpotkajmySie.Services
{
    public interface ICalendarService
    {
        public List<FreeTimeIntervalDto> GetFreeTimeIntervals(TimeSpan minimumLength, List<CalendarDto> calendarsDto);
    }
}
