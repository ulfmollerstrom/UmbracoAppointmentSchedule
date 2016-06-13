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
        public List<DateTime> HolidayDates { get; set; } = new List<DateTime>(0);

        public WeekSchedule Create()
        {
            var dates = GetThisWeek(Today);

            if (!AssignOnWeekends)
                dates = RemoveWeekend(dates);
            
            if (!AssignOnHolidays)
                dates = RemoveHolidayDates(dates, HolidayDates);

            var weekSchedule = GetWeekSchedule(GetDaySchedules(NumberOfTimeSlotsForAppointments, dates), Today);

            return weekSchedule;
        }

        public WeekSchedule CreateFullWeek(DateTime today, int numberOfTimeSlotsForAppointments)
        {
            Today = today;
            NumberOfTimeSlotsForAppointments = numberOfTimeSlotsForAppointments;
            AssignOnWeekends = true;

            var dates = GetThisWeek(today);
            var weekSchedule = GetWeekSchedule(GetDaySchedules(numberOfTimeSlotsForAppointments, dates), today);

            return weekSchedule;
        }

        public WeekSchedule CreateWeekWithOutWeekend(DateTime today, int numberOfTimeSlotsForAppointments)
        {
            Today = today;
            NumberOfTimeSlotsForAppointments = numberOfTimeSlotsForAppointments;
            AssignOnWeekends = false;
            AssignOnHolidays = true;

            var dates = RemoveWeekend(GetThisWeek(today));
            var weekSchedule = GetWeekSchedule(GetDaySchedules(numberOfTimeSlotsForAppointments, dates), today);

            return weekSchedule;
        }

        public WeekSchedule CreateWeekWithOutHolidays(DateTime today, int numberOfTimeSlotsForAppointments, IEnumerable<DateTime> holidays)
        {
            Today = today;
            NumberOfTimeSlotsForAppointments = numberOfTimeSlotsForAppointments;
            AssignOnWeekends = true;
            AssignOnHolidays = false;

            var dates = RemoveHolidayDates(GetThisWeek(today), holidays);
            var weekSchedule = GetWeekSchedule(GetDaySchedules(numberOfTimeSlotsForAppointments, dates), today);

            return weekSchedule;
        }

        public WeekSchedule CreateWeekWithOutWeekendAndHolidays(DateTime today, int numberOfTimeSlotsForAppointments, IEnumerable<DateTime> holidays)
        {
            Today = today;
            NumberOfTimeSlotsForAppointments = numberOfTimeSlotsForAppointments;
            AssignOnWeekends = false;
            AssignOnHolidays = false;

            var dates = RemoveHolidayDates(RemoveWeekend(GetThisWeek(today)), holidays);
            var weekSchedule = GetWeekSchedule(GetDaySchedules(numberOfTimeSlotsForAppointments, dates), today);

            return weekSchedule;
        }

        private static WeekSchedule GetWeekSchedule(IEnumerable<DaySchedule> daySchedules, DateTime today)
        {
            var weekSchedule = new WeekSchedule {Today = today};
            weekSchedule.CreateWithEmptyTimeSlots(daySchedules);
            return weekSchedule;
        }

        private static IEnumerable<DaySchedule> GetDaySchedules(int numberOfTimeSlotsForAppointments, IEnumerable<DateTime> dates)
        {
            var daySchedules = dates.Select(date => new DaySchedule
            {
                Date = date,
                NumberOfTimeSlotsForAppointments = numberOfTimeSlotsForAppointments
            });
            return daySchedules;
        }

        private static IEnumerable<DateTime> GetThisWeek(DateTime today)
        {
            var monday = GetDateForMonday(today);
            var dates = Enumerable.Range(0, 7).Select(i => monday.AddDays(i));
            return dates.ToList();
        }

        private static IEnumerable<DateTime> RemoveWeekend(IEnumerable<DateTime> dates)
        {
            return dates.Take(5).ToList();
        }

        private static DateTime GetDateForMonday(DateTime today)
        {
            if (today.DayOfWeek == DayOfWeek.Monday)
                return today;

            var retval = Enumerable.Range(-7, 6)
                .Where(i => today.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                .Select(i => today.AddDays(i)).FirstOrDefault();

            return retval;
        }

        private static IEnumerable<DateTime> RemoveHolidayDates(IEnumerable<DateTime> thisWeek, IEnumerable<DateTime> holidays)
        {
            return thisWeek.Except(holidays);
        }

    }
}