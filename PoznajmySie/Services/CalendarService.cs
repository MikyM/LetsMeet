using System;
using PoznajmySie.Models;
using System.Collections.Generic;
using AutoMapper;
using PoznajmySie.DataTransferObjects;

namespace PoznajmySie.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IMapper _mapper;

        public CalendarService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<PlannedMeetingDto> GetPossibleMeetings(TimeSpan minimumLength, List<CalendarDto> calendarsDto)
        {
            List<PlannedMeeting> possibleMeetings = new List<PlannedMeeting>();
            List<Calendar> calendars = _mapper.Map<List<CalendarDto>, List<Calendar>>(calendarsDto);
            Calendar firstCalendar = calendars[0];

            if (calendars.Count.Equals(1))
            {
                return _mapper.Map<List<PlannedMeeting>, List<PlannedMeetingDto>>(firstCalendar.GetFreeTimeIntervals(minimumLength)); 
            }

            List<PlannedMeeting> firstCalendarMeetings = calendars[0].PlannedMeetings;

            for (int i = 1; i <= calendars.Count; i++)
            {
                List<PlannedMeeting> firstCalendarFreeIntervals = calendars[i - 1].GetFreeTimeIntervals(minimumLength);
            }

            //List<PlannedMeeting> firstCalendarMeetings = calendars[0].PlannedMeetings;

            return _mapper.Map<List<PlannedMeeting>, List<PlannedMeetingDto>>(possibleMeetings);
        }
    }
}