using AutoMapper;
using PoznajmySie.DataTransferObjects;
using PoznajmySie.Models;
using System;

namespace PoznajmySie
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Calendar, CalendarDto>();
            CreateMap<CalendarDto, Calendar>()
                .ForMember(au => au.WorkingHours, map => map.MapFrom(vm => new WorkingHours(TimeSpan.Parse(vm.WorkingHours.Start), TimeSpan.Parse(vm.WorkingHours.End))));
            CreateMap<WorkingHours, WorkingHoursDto>();
            CreateMap<WorkingHoursDto, WorkingHours>();
            CreateMap<PlannedMeeting, PlannedMeetingDto>()
                .ForMember(au => au.Start, map => map.MapFrom(vm => vm.Start.ToString(@"hh\:mm")))
                .ForMember(au => au.End, map => map.MapFrom(vm => vm.End.ToString(@"hh\:mm")));
            CreateMap<PlannedMeetingDto, PlannedMeeting>();
            CreateMap<FreeTimeIntervalDto, FreeTimeInterval>();
            CreateMap<FreeTimeInterval, FreeTimeIntervalDto>()
                .ForMember(au => au.Start, map => map.MapFrom(vm => vm.Start.ToString(@"hh\:mm")))
                .ForMember(au => au.End, map => map.MapFrom(vm => vm.End.ToString(@"hh\:mm")));
        }
    }
}