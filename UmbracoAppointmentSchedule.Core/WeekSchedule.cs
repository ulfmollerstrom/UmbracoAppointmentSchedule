using System;
using System.Collections.Generic;

namespace UmbracoAppointmentSchedule.Core
{
    public class WeekSchedule
    {
        public List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();
        public DateTime Today { get; set; } = DateTime.Today;
    }
}