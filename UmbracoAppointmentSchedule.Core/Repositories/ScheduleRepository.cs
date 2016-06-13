using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UmbracoAppointmentSchedule.Core.Common;
using UmbracoAppointmentSchedule.Core.Models;

namespace UmbracoAppointmentSchedule.Core.Repositories
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

        public string Serialize()
        {
            return Serialize(this);
        }

        private static string Serialize(ScheduleRepository scheduleRepository)
        {
            return JsonConvert.SerializeObject(scheduleRepository);
        }

        public void Deserialize(string json, DateTime currentDate)
        {
            var tmp = Deserialize(json);
            AssignOnWeekends = tmp.AssignOnWeekends;
            AssignOnHolidays = tmp.AssignOnWeekends;
            NumberOfTimeSlotsForAppointments = tmp.NumberOfTimeSlotsForAppointments;
            HolidayDates = tmp.HolidayDates;
            Appointments = tmp.Appointments;
            ScheduleConfig = tmp.ScheduleConfig;
            CurrentDate = currentDate;
        }

        private static ScheduleRepository Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ScheduleRepository>(json);
        }
    }
}