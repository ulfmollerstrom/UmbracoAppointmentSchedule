using System;

namespace UmbracoAppointmentSchedule.Core.Models
{
    public class Appointment : IEquatable<Appointment>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }

        public bool Equals(Appointment other)
        {
            if (other == null)
                return false;

            return Date.Date.Equals(other.Date.Date) && Slot.Equals(other.Slot);
        }
    }
}