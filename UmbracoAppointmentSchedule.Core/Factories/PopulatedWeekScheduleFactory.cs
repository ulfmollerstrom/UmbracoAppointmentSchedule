using System;
using UmbracoAppointmentSchedule.Core.Common;
using UmbracoAppointmentSchedule.Core.Models;
using UmbracoAppointmentSchedule.Core.Repositories;

namespace UmbracoAppointmentSchedule.Core.Factories
{
    public class PopulatedWeekScheduleFactory
    {
        public ScheduleRepository ScheduleRepository { get; }

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
                    weekSchedule = CreateWeekWithOutHolidays();
                    break;
                case ScheduleConfiguration.WeekWithOutWeekendAndHolidays:
                    weekSchedule = CreateWeekWithOutWeekendAndHolidays();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return weekSchedule;
        }

        private WeekSchedule CreateWeekWithOutWeekendAndHolidays()
        {
            var weekSchedule = new WeekScheduleFactory().CreateWeekWithOutWeekendAndHolidays(ScheduleRepository.CurrentDate, ScheduleRepository.NumberOfTimeSlotsForAppointments, ScheduleRepository.HolidayDates);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }

        private WeekSchedule CreateWeekWithOutHolidays()
        {
            var weekSchedule = new WeekScheduleFactory().CreateWeekWithOutHolidays(ScheduleRepository.CurrentDate, ScheduleRepository.NumberOfTimeSlotsForAppointments, ScheduleRepository.HolidayDates);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }

        private WeekSchedule CreateWeekWithOutWeekend()
        {
            var weekSchedule = new WeekScheduleFactory().CreateWeekWithOutWeekend(ScheduleRepository.CurrentDate, ScheduleRepository.NumberOfTimeSlotsForAppointments);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }

        private WeekSchedule CreateFullWeek()
        {
            var weekSchedule = new WeekScheduleFactory().CreateFullWeek(ScheduleRepository.CurrentDate, ScheduleRepository.NumberOfTimeSlotsForAppointments);
            weekSchedule.AddRange(ScheduleRepository.Appointments);
            return weekSchedule;
        }
    }
}