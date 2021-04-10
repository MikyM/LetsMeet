using System;
using System.Collections.Generic;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Services
{
    public interface ICalendarService
    {
        public List<PlannedMeetingDto> GetPossibleMeetings(TimeSpan minimumLength, List<CalendarDto> calendarsDto);
    }
}
