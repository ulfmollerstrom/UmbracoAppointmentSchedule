using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UmbracoAppointmentSchedule.Core
{
    public class ScheduleRepository
    {
        public bool AssignOnWeekends { get; set; }
        public bool AssignOnHolidays { get; set; }
        public int NumberOfTimeSlotsForAppointments { get; set; }
        public List<DateTime> HolidayDates { get; set; }
        public List<Appointment> Appointments { get; set; }
        public ScheduleConfiguration ScheduleConfig { get; set; }

        [JsonIgnore]
        public DateTime CurrentDate { get; set; }
    }
}