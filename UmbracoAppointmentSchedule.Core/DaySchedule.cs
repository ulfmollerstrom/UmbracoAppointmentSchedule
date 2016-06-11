using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoAppointmentSchedule.Core
{
    public class DaySchedule
    {
        private int numberOfTimeSlotsForAppointments;
        private bool isAssignable = true;

        public int NumberOfTimeSlotsForAppointments
        {
            get { return numberOfTimeSlotsForAppointments; }
            set
            {
                if (value < 0) value = 0;
                numberOfTimeSlotsForAppointments = value;
                Appointments = new List<Appointment>(value);
                Appointments.AddRange(Enumerable.Repeat<Appointment>(null, value));
            }
        }

        public bool IsAssignable
        {
            get { return isAssignable; }
            set
            {
                isAssignable = value;
                if (value == false) Appointments = new List<Appointment>(0);
            }
        }

        public DateTime Date { get; set; } = DateTime.Today;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>(0);
        public IEnumerable<int> FreeTimeSlots => GetFreeTimeslots();

        public bool AddAppointment(int slot, Appointment appointment)
        {
            try
            {
                if (Appointments[slot] != null)
                    return false;

                Appointments[slot] = appointment;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<int> GetFreeTimeslots()
        {
            var retval = new List<int>();
            retval = Appointments.Where(appointment => appointment == null).Select((appointment, index) => index).ToList();
            return retval;
        }

    }
}