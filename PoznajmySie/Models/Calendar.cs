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

        public List<FreeTimeInterval> GetFreeTimeIntervals(TimeSpan minimumLength)
        {
            List<PlannedMeeting> plannedMeetings = this.PlannedMeetings.OrderBy(x => x.Start).ToList();
            List<FreeTimeInterval> result = new List<FreeTimeInterval>();

            if (plannedMeetings.Count.Equals(0))
            {
                return new List<FreeTimeInterval>() { new FreeTimeInterval(this.WorkingHours.Start, this.WorkingHours.End) };
            }

            if (plannedMeetings.First().Start > this.WorkingHours.Start && plannedMeetings.First().Start.Subtract(this.WorkingHours.Start) >= minimumLength)
            {
                result.Add(new FreeTimeInterval(this.WorkingHours.Start, plannedMeetings.First().Start));
            }

            if (!plannedMeetings.Count.Equals(1))
            {
                for (int i = 1; i < plannedMeetings.Count; i++)
                {
                    if (plannedMeetings[i].Start.Subtract(plannedMeetings[i - 1].End) >= minimumLength)
                    {
                        result.Add(new FreeTimeInterval(plannedMeetings[i - 1].End, plannedMeetings[i].Start));
                    }
                }

            }

            if (plannedMeetings.Last().End < this.WorkingHours.End && this.WorkingHours.End.Subtract(plannedMeetings.Last().End) >= minimumLength)
            {
                result.Add(new FreeTimeInterval(plannedMeetings.Last().End, this.WorkingHours.End));
            }

            return result;
        }

        public List<FreeTimeInterval> CompareWith(Calendar calendarToCompare, TimeSpan minimumLength)
        {
            List<FreeTimeInterval> result = new List<FreeTimeInterval>();
            List<FreeTimeInterval> freeIntervalsToCompare = calendarToCompare.GetFreeTimeIntervals(minimumLength);
            List<FreeTimeInterval> freeInvervals = this.GetFreeTimeIntervals(minimumLength);

            foreach (FreeTimeInterval freeSpan in freeInvervals)
            {
                FreeTimeInterval overlap = freeIntervalsToCompare.FirstOrDefault(x => x.Start < freeSpan.End && freeSpan.Start < x.End);
                if(overlap is not null)
                {
                    result.Add(new FreeTimeInterval(freeSpan.Start > overlap.Start ? freeSpan.Start : overlap.Start, freeSpan.End < overlap.End ? freeSpan.End : overlap.End));
                }
            }

            return result;
        }

        public static List<FreeTimeInterval> CompareMultipleCalendars(List<Calendar> calendarsToCompare, TimeSpan minimumLength)
        {
            List<FreeTimeInterval> result = calendarsToCompare[0].CompareWith(calendarsToCompare[1], minimumLength);
            List<FreeTimeInterval> intervalsToCompare = new List<FreeTimeInterval>();

            for (int i = 2; i < calendarsToCompare.Count; i++)
            {
                intervalsToCompare = calendarsToCompare[i].GetFreeTimeIntervals(minimumLength);
                if(intervalsToCompare.Count.Equals(0))
                {
                    result.Clear();
                }

                //result.RemoveAll(x => !intervalsToCompare.Any(y => x.Start <= y.End && y.Start <= x.End));

                foreach (FreeTimeInterval interval in intervalsToCompare)
                {
                    
                    List<FreeTimeInterval> overlaps = result.Where(x => interval.Start < x.End && x.Start < interval.End).ToList();
                    bool isEndOverlapping = false;
                    bool isFullyOverlapping = false;
                    bool isStartOverlapping = false;

                    foreach (FreeTimeInterval overlap in overlaps)                   
                    {
                        isEndOverlapping = interval.Start < overlap.Start && interval.End < overlap.End;
                        isFullyOverlapping = interval.Start > overlap.Start && interval.End < overlap.End;
                        isStartOverlapping = interval.Start > overlap.Start && interval.End > overlap.End;

                        if (isEndOverlapping)
                        {
                            if(interval.End.Subtract(overlap.Start) >= minimumLength)
                            {
                                overlap.End = interval.End;
                            }
                            else
                            {
                                result.Remove(overlap);
                            }                       
                        }
                        else if (isFullyOverlapping)
                        {
                            if (interval.End.Subtract(interval.Start) >= minimumLength)
                            {
                                overlap.Start = interval.Start;
                                overlap.End = interval.End;
                            }
                            else
                            {
                                result.Remove(overlap);
                            }
                        }
                        else if (isStartOverlapping)
                        {
                            if (overlap.End.Subtract(interval.Start) >= minimumLength)
                            {
                                overlap.Start = interval.Start;
                            }
                            else
                            {
                                result.Remove(overlap);
                            }                          
                        }
                    }
                }
                result.RemoveAll(x => !intervalsToCompare.Any(y => y.Start <= x.End && x.Start <= y.End));
            }        
            return result;
        }
    }
}
