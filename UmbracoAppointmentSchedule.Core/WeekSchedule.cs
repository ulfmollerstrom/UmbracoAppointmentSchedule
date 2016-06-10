using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoAppointmentSchedule.Core
{
    public class WeekSchedule
    {
        public List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();
        public DateTime Today { get; set; } = DateTime.Today;

        public bool Add(Appointment appointment)
        {
            return DaySchedules
                    .Single(ds => ds.Date.Equals(appointment.Date))
                    .AddAppointment(appointment.Slot, appointment);
        }

        [Obsolete]
        public bool Add(DateTime date, int slot, string name, string phone)
        {
            return Add(new Appointment {Date = date, Slot = slot, Name = name, Phone = phone});
        }
    }
}