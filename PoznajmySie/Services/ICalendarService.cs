using System;
using System.Collections.Generic;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Services
{
    public interface ICalendarService
    {
        public List<FreeTimeIntervalDto> GetFreeTimeIntervals(TimeSpan minimumLength, List<CalendarDto> calendarsDto);
    }
}
