using System;
using System.Collections.Generic;
using System.Linq;

namespace PoznajmySie.Models
{
    public class Calendar
    {
        public Guid Id { get; set; }
        public WorkingHours WorkingHours { get; set; }
        public List<PlannedMeeting> PlannedMeetings { get; set; }

        public Calendar()
        {
            this.PlannedMeetings = new List<PlannedMeeting>();
        }
        public Calendar(TimeSpan workStart, TimeSpan workEnd, List<PlannedMeeting> plannedMeetings)
        {

            this.WorkingHours = new WorkingHours(workStart, workEnd);
            this.PlannedMeetings = plannedMeetings;
        }

        public void SetWorkingHours(TimeSpan workStart, TimeSpan workEnd)
        {
            if (workStart >= workEnd) {
                throw new ArgumentException("workEnd must be greater than workStart");
            } else if (workStart.TotalHours > 24 || workEnd.TotalHours > 24) {
                throw new ArgumentException("Spans can't be greater than 24 hours");
            } else if (workStart.TotalHours < 0 || workEnd.TotalHours < 0) {
                throw new ArgumentException("Spans can't be negative");
            }
            this.WorkingHours = new WorkingHours(workStart, workEnd);
        }

        public void SetMeetings(List<PlannedMeeting> plannedMeetings)
        {
            if (plannedMeetings is null) {
                throw new ArgumentNullException("plannedMeetings");
            }
            this.PlannedMeetings = plannedMeetings;
        }

        public void ClearMeetings()
        {
            this.PlannedMeetings.Clear();
        }

        public void AddMeeting(PlannedMeeting meeting)
        {
            if (meeting is null) {
                throw new ArgumentNullException("meeting");
            }
            this.PlannedMeetings.Add(meeting);
        }

        public void RemoveMeeting(Guid id)
        {
            var meetingToRemove = GetMeetingById(id);
            if (meetingToRemove is not null) {
                this.PlannedMeetings.Remove(meetingToRemove);
            }
        }

        public PlannedMeeting GetMeetingById(Guid id)
        {
            return this.PlannedMeetings.SingleOrDefault(x => x.Id.Equals(id));
        }

        public List<FreeTimeInterval> GetFreeTimeIntervals(TimeSpan meetingDuration)
        {
            if (meetingDuration.TotalHours >= 24 || meetingDuration.TotalHours <= 0) {
                throw new ArgumentException("Minimum length value must be greater than 0 and smaller than 24");
            }

            List<PlannedMeeting> plannedMeetings = this.PlannedMeetings.OrderBy(x => x.Start).ToList();
            List<FreeTimeInterval> result = new List<FreeTimeInterval>();

            if (plannedMeetings.Count.Equals(0)) {
                return new List<FreeTimeInterval>() { new FreeTimeInterval(this.WorkingHours.Start, this.WorkingHours.End) };
            }

            if (plannedMeetings.First().Start > this.WorkingHours.Start && plannedMeetings.First().Start.Subtract(this.WorkingHours.Start) >= meetingDuration) {
                result.Add(new FreeTimeInterval(this.WorkingHours.Start, plannedMeetings.First().Start));
            }

            if (!plannedMeetings.Count.Equals(1)) {
                for (int i = 1; i < plannedMeetings.Count; i++) {
                    if (plannedMeetings[i].Start.Subtract(plannedMeetings[i - 1].End) >= meetingDuration) {
                        result.Add(new FreeTimeInterval(plannedMeetings[i - 1].End, plannedMeetings[i].Start));
                    }
                }
            }

            if (plannedMeetings.Last().End < this.WorkingHours.End && this.WorkingHours.End.Subtract(plannedMeetings.Last().End) >= meetingDuration) {
                result.Add(new FreeTimeInterval(plannedMeetings.Last().End, this.WorkingHours.End));
            }

            return result;
        }

        public static List<FreeTimeInterval> CompareMultipleCalendars(List<Calendar> calendarsToCompare, TimeSpan meetingDuration)
        {
            if (meetingDuration.TotalHours >= 24 || meetingDuration.TotalHours <= 0) {
                throw new ArgumentException("Minimum length value must be greater than 0 and smaller than 24");
            }
            if (calendarsToCompare is null) {
                throw new ArgumentNullException("calendarsToCompare");
            }
            if (calendarsToCompare.Count.Equals(0)) {
                throw new ArgumentException("At least one calendar must be supplied");
            }

            TimeSpan start = new TimeSpan(0, 0, 0);
            TimeSpan end = new TimeSpan(23, 59, 59);
            Calendar dummyCalendar = new Calendar();
            List<PlannedMeeting> dummyMeetings = new List<PlannedMeeting>();
            List<PlannedMeeting> helpList = new List<PlannedMeeting>();

            calendarsToCompare.ForEach(x => {
                if (x.WorkingHours.Start > start) {
                    start = x.WorkingHours.Start;
                }
                if (x.WorkingHours.End < end) {
                    end = x.WorkingHours.End;
                }
            });

            dummyCalendar.SetWorkingHours(start, end);

            calendarsToCompare.ForEach(calendar => {
                foreach(PlannedMeeting meeting in calendar.PlannedMeetings) {
                    if(meeting.Start < dummyCalendar.WorkingHours.Start) {
                        meeting.Start = dummyCalendar.WorkingHours.Start;
                    }
                    if(meeting.End > dummyCalendar.WorkingHours.End) {
                        meeting.End = dummyCalendar.WorkingHours.End;
                    }

                    if(dummyMeetings.Any(x => x.Start <= meeting.Start && x.End >= meeting.End)) {
                        continue;
                    }

                    helpList = dummyMeetings.Where(x => meeting.Start <= x.Start && meeting.End >= x.End).ToList();
                    if (helpList.Count.Equals(0)) {
                        dummyMeetings.Add(meeting);
                    } else {
                        helpList.ForEach(x => dummyMeetings.Remove(x));
                        dummyMeetings.Add(meeting);
                    }
                }
            });

            dummyCalendar.SetMeetings(dummyMeetings);

            return dummyCalendar.GetFreeTimeIntervals(meetingDuration);
        }
    }
}
