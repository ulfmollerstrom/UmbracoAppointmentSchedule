using System;

namespace UmbracoAppointmentSchedule.Core
{
    public class PopulatedWeekScheduleFactory
    {
        public ScheduleRepository ScheduleRepository { get; private set; }

        public PopulatedWeekScheduleFactory(ScheduleRepository scheduleRepository)
        {
            ScheduleRepository = scheduleRepository;

        }

        public WeekSchedule Create()
        {
            var weekSchedule = new WeekSchedule();

            switch (ScheduleRepository.ScheduleConfig)
            {
                case ScheduleConfiguration.FullWeek:
                    weekSchedule = CreateFullWeek();
                    break;
                case ScheduleConfiguration.WeekWithOutWeekend:
                    weekSchedule = CreateWeekWithOutWeekend();
                    break;
                case ScheduleConfiguration.WeekWithOutHolidays:
                    break;
                case ScheduleConfiguration.WeekWithOutWeekendAndHolidays:
                    break;
                default:
                    break;
            }
            
            return weekSchedule;
        }

        private WeekSchedule CreateWeekWithOutWeekend()
        {
            var weekSchedule = new WeekScheduleFactory().CreateWeekWithOutWeekend(ScheduleRepository.CurrentDate,
                                                                                  ScheduleRepository.NumberOfTimeSlotsForAppointments);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }

        private WeekSchedule CreateFullWeek()
        {
            var weekSchedule = new WeekScheduleFactory().CreateFullWeek(ScheduleRepository.CurrentDate,
                                                                        ScheduleRepository.NumberOfTimeSlotsForAppointments);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }
    }
}