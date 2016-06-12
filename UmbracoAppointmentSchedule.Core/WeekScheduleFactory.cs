using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoAppointmentSchedule.Core
{
    public class WeekScheduleFactory
    {
        public bool AssignOnWeekends { get; set; } = false;
        public bool AssignOnHolidays { get; set; } = false;
        public DateTime Today { get; set; } = DateTime.Today;
        public int NumberOfTimeSlotsForAppointments { get; set; } = 1;
        public List<DateTime> Holidays { get; set; } = new List<DateTime>(0);

        public WeekSchedule Create()
        {
            var dates = GetThisWeek(Today, AssignOnWeekends);

            var daySchedules = dates.Select(date => new DaySchedule
                                                    {
                                                      Date = date,
                                                      NumberOfTimeSlotsForAppointments = NumberOfTimeSlotsForAppointments
                                                     });

            var weekSchedule = new WeekSchedule {Today = Today};
            weekSchedule.DaySchedules.AddRange(daySchedules);

            return weekSchedule;
        }

        private IEnumerable<DateTime> GetThisWeek(DateTime today, bool assignOnWeekends = false)
        {
            return GetThisWeek(today, new List<DateTime>(0), false, assignOnWeekends);
        }

        private IEnumerable<DateTime> GetThisWeek(DateTime today, IEnumerable<DateTime> holidays, bool assignOnHolidays = false, bool assignOnWeekends = false)
        {
            var monday = GetDateForMonday(today);

            var numberOfDaysToAdd = assignOnWeekends ? 7 : 5;
            var dates = Enumerable.Range(0, numberOfDaysToAdd).Select(i => monday.AddDays(i));

            if (!assignOnHolidays)
                dates = dates.Except(holidays);

            return dates.ToList();
        }

        private DateTime GetDateForMonday(DateTime today)
        {
            if (today.DayOfWeek == DayOfWeek.Monday)
                return today;

            var retval = Enumerable.Range(-7, 6)
                .Where(i => today.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                .Select(i => today.AddDays(i)).FirstOrDefault();

            return retval;
        }
    }
}