using System;

namespace UmbracoAppointmentSchedule.Core
{
    public class Appointment
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }
    }
}