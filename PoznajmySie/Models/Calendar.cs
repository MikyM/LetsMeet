using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using PoznajmySie.CustomValidator;

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
            this.SetWorkingHours(workStart, workEnd);
            this.SetMeetings(plannedMeetings);
        }

        public void SetWorkingHours(TimeSpan workStart, TimeSpan workEnd)
        {
            this.WorkingHours = new WorkingHours(workStart, workEnd);
        }

        public void SetMeetings(List<PlannedMeeting> plannedMeetings)
        {
            this.PlannedMeetings = plannedMeetings;
        }

        public void ClearMeetings()
        {
            this.PlannedMeetings.Clear();
        }

        public void AddMeeting(PlannedMeeting meeting)
        {
            this.PlannedMeetings.Add(meeting);
        }
        
        public void RemoveMeeting(Guid id)
        {
            var meetingToRemove = GetMeetingById(id);
            if(meetingToRemove is not null)
            {
                this.PlannedMeetings.Remove(meetingToRemove);
            }         
        }

        public PlannedMeeting GetMeetingById(Guid id)
        {
            return this.PlannedMeetings.SingleOrDefault(x => x.Id.Equals(id));
        }

        public List<PlannedMeeting> GetFreeTimeIntervals(TimeSpan minimumLength)
        {
            List<PlannedMeeting> plannedMeetings = this.PlannedMeetings.OrderBy(x => x.Start).ToList();
            List<PlannedMeeting> possibleMeetings = new List<PlannedMeeting>();

            if (plannedMeetings.Count.Equals(0))
            {
                return new List<PlannedMeeting>() { new PlannedMeeting(this.WorkingHours.Start, this.WorkingHours.End) };
            }

            if (plannedMeetings.First().Start > this.WorkingHours.Start && plannedMeetings.First().Start.Subtract(this.WorkingHours.Start) >= minimumLength)
            {
                possibleMeetings.Add(new PlannedMeeting(this.WorkingHours.Start, plannedMeetings.First().Start));
            }

            if (!plannedMeetings.Count.Equals(1))
            {
                for (int i = 1; i < plannedMeetings.Count; i++)
                {
                    if (plannedMeetings[i].Start.Subtract(plannedMeetings[i - 1].End) >= minimumLength)
                    {
                        possibleMeetings.Add(new PlannedMeeting(plannedMeetings[i - 1].End, plannedMeetings[i].Start));
                    }
                }

            }

            if (plannedMeetings.Last().End < this.WorkingHours.End && this.WorkingHours.End.Subtract(plannedMeetings.First().End) >= minimumLength)
            {
                possibleMeetings.Add(new PlannedMeeting(plannedMeetings.Last().End, this.WorkingHours.End));
            }


            return possibleMeetings;
        }
    }
}
