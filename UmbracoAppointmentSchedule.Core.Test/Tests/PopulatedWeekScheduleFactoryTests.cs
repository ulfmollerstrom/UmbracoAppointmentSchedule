using System;
using System.Collections.Generic;
using NUnit.Framework;
using UmbracoAppointmentSchedule.Core.Common;
using UmbracoAppointmentSchedule.Core.Factories;
using UmbracoAppointmentSchedule.Core.Models;
using UmbracoAppointmentSchedule.Core.Repositories;

namespace UmbracoAppointmentSchedule.Core.Test.Tests
{
    [TestFixture]
    public class PopulatedWeekScheduleFactoryTests
    {
        [Test]
        public void CreatePopulatedWeekScheduleFactory()
        {
            //Arrange
            var scheduleRepository = new ScheduleRepository();

            //Act
            var actual = new PopulatedWeekScheduleFactory(scheduleRepository);

            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void PopulatedFullWeek()
        {
            //Arrange
            var scheduleRepository = GetScheduleRepository();

            //Act
            var scheduleFactory = new PopulatedWeekScheduleFactory(scheduleRepository);

            var weekSchedule = scheduleFactory.Create();
            var nh = weekSchedule.GetAppointments()[0];
            //Assert
            Assert.IsNotEmpty(weekSchedule.GetAppointments());
            Assert.AreEqual("Nisse Hult", nh.Name);
        }

        private static ScheduleRepository GetScheduleRepository()
        {
            var scheduleRepository = new ScheduleRepository
            {
                CurrentDate = new DateTime(2016, 6, 10),
                AssignOnHolidays = true,
                AssignOnWeekends = true,
                NumberOfTimeSlotsForAppointments = 3,
                HolidayDates = new List<DateTime> {new DateTime(2016, 6, 6)},
                Appointments =
                    new List<Appointment>
                    {
                        new Appointment
                        {
                            Name = "Nisse Hult",
                            Phone = "24682468",
                            Date = new DateTime(2016, 6, 10),
                            Slot = 0
                        }
                    },
                ScheduleConfig = ScheduleConfiguration.FullWeek
            };
            return scheduleRepository;
        }
    }
}
