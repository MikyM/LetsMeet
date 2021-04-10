using System;
using PoznajmySie.Models;
using System.Collections.Generic;
using AutoMapper;
using PoznajmySie.DataTransferObjects;
using System.Linq;

namespace PoznajmySie.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IMapper _mapper;

        public CalendarService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<FreeTimeIntervalDto> GetPossibleTimeSpans(TimeSpan minimumLength, List<CalendarDto> calendarsDto)
        {
            List<Calendar> calendars = _mapper.Map<List<CalendarDto>, List<Calendar>>(calendarsDto);

            if (calendars.Count.Equals(1))
            {
                return _mapper.Map<List<FreeTimeInterval>, List<FreeTimeIntervalDto>>(calendars[0].GetFreeTimeIntervals(minimumLength));
            }

            return _mapper.Map<List<FreeTimeInterval>, List<FreeTimeIntervalDto>>(Calendar.CompareMultipleCalendars(calendars, minimumLength));
        }
    }
}