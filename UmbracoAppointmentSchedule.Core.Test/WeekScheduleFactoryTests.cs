using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace UmbracoAppointmentSchedule.Core.Test
{
    [TestFixture]
    public class WeekScheduleFactoryTests
    {
        [Test]
        public void WeekSchedule()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = true,
                AssignOnHolidays = true,
                Today = new DateTime(2016, 6, 10),
                NumberOfTimeSlotsForAppointments = 3,
                HolidayDates = new List<DateTime> {new DateTime(2016, 6, 6)}
            };

            //Act
            var actual = factory.Create();

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(7, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }

        [Test]
        public void WeekScheduleWithOutWeekend()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = false,
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3,
            };

            //Act
            var actual = factory.Create();

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(5, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }

        [Test]
        public void WeekScheduleWithOutHoliday()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                AssignOnHolidays = false,
                Today = new DateTime(2016, 6, 6),
                HolidayDates = new List<DateTime> { new DateTime(2016, 6, 6) }
            };

            //Act
            var actual = factory.Create();

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(4, actual.DaySchedules.Count);
        }

        [Test]
        public void WeekScheduleWithOutHolidayAndNoHolidaysSpecified()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                AssignOnHolidays = false,
                Today = DateTime.Today
            };

            //Act
            var actual = factory.Create();

            //Assert
            Assert.AreEqual(5, actual.DaySchedules.Count);
        }

        [Test]
        public void CreateFullWeekNoRestrictions()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = true,
                AssignOnHolidays = true,
                Today = new DateTime(2016, 6, 13),
                NumberOfTimeSlotsForAppointments = 3
            };

            //Act
            var actual = factory.CreateFullWeek(today: new DateTime(2016, 6, 10),
                                                numberOfTimeSlotsForAppointments: 3);

            //Assert
            Assert.AreEqual(7, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }

        [Test]
        public void CreateWeekWithOutWeekend()
        {
            //Arrange
            var today = new DateTime(2016, 6, 13);
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = false,
                AssignOnHolidays = true,
                Today = today,
                NumberOfTimeSlotsForAppointments = 3
            };

            //Act
            var actual = factory.CreateWeekWithOutWeekend(today: today,
                                                          numberOfTimeSlotsForAppointments: 3);

            //Assert
            Assert.AreEqual(5, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }

        [Test]
        public void CreateWeekWithOutHolidays()
        {
            //Arrange
            var today = new DateTime(2016, 6, 10);
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = true,
                AssignOnHolidays = false,
                Today = today,
                NumberOfTimeSlotsForAppointments = 3
            };

            //Act
            var actual = factory.CreateWeekWithOutHolidays(today: today,
                                                           numberOfTimeSlotsForAppointments: 3,
                                                           holidays: new List<DateTime> { new DateTime(2016, 6, 6)});

            //Assert
            Assert.AreEqual(6, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }

        [Test]
        public void CreateWeekWithOutWeekendAndHolidays()
        {
            //Arrange
            var today = new DateTime(2016, 6, 10);
            var factory = new WeekScheduleFactory
            {
                AssignOnWeekends = false,
                AssignOnHolidays = false,
                Today = today,
                NumberOfTimeSlotsForAppointments = 3
            };

            //Act
            var actual = factory.CreateWeekWithOutWeekendAndHolidays(today: today,
                                                                     numberOfTimeSlotsForAppointments: 3,
                                                                     holidays: new List<DateTime> { new DateTime(2016, 6, 6) });

            //Assert
            Assert.AreEqual(4, actual.DaySchedules.Count);
            Assert.AreEqual(3, actual.DaySchedules[0].Appointments.Count);

        }
    }
}
