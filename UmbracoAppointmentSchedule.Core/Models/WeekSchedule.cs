using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoAppointmentSchedule.Core.Models
{
    public class WeekSchedule
    {
        public List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();
        public DateTime Today { get; set; } = DateTime.Today;

        public bool Add(Appointment appointment)
        {
            var success = false;
            try
            {
                success = DaySchedules
                        .Single(daySchedule => daySchedule.Date.Equals(appointment.Date))
                        .AddAppointment(appointment.Slot, appointment);
            }
            catch{}

            return success;
        }

        [Obsolete]
        public bool Add(DateTime date, int slot, string name, string phone)
        {
            return Add(new Appointment {Date = date, Slot = slot, Name = name, Phone = phone});
        }

        public Appointment GetAppointment(DateTime date, int slot)
        {
            return DaySchedules
                .SingleOrDefault(daySchedule => daySchedule.Date.Equals(date))?.Appointments[slot] ?? new Appointment();
        }

        public List<Appointment> GetAppointments()
        {
            return DaySchedules
                    .SelectMany(daySchedule => daySchedule.Appointments)
                    .Where(appointment => appointment != null).ToList();
        }

        public int AddRange(IEnumerable<Appointment> appointments)
        {
            return appointments.Sum(appointment => Add(appointment) ? 1 : 0);
        }

        public void CreateWithEmptyTimeSlots(IEnumerable<DaySchedule> daySchedules)
        {
            DaySchedules.AddRange(daySchedules);
        }
    }
}