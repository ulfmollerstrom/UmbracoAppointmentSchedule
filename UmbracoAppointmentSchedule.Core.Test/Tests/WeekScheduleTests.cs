using System;
using System.Collections.Generic;
using NUnit.Framework;
using UmbracoAppointmentSchedule.Core.Factories;
using UmbracoAppointmentSchedule.Core.Models;

namespace UmbracoAppointmentSchedule.Core.Test.Tests
{
    [TestFixture]
    public class WeekScheduleTests
    {
        [Test]
        public void CanCreateAWeekSchedule()
        {
            //Arrange
            var expected = new WeekSchedule
            {
                Today = DateTime.Today,
                DaySchedules = new List<DaySchedule>()
            };
           
            //Act
            //var actual = new

            //Assert
            Assert.IsNotNull(expected);
        }

        [Test]
        public void CanAddAnAppointmentToWeekSchedule()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3,
                AssignOnWeekends = true
            };

            var weekSchedule = factory.Create();

            //Act
            var success = weekSchedule.Add(new Appointment
                                                {
                                                    Date = DateTime.Today,
                                                    Slot = 2,
                                                    Name = "Nisse Hult",
                                                    Phone = "24682468"
                                                });

            //Assert
            Assert.IsTrue(success);

        }

        [Test]
        public void CanAddAnAppointmentToWeekScheduleWithAddMetodOverload()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3,
                AssignOnWeekends = true
            };

            var weekSchedule = factory.Create();

            //Act
            var success = weekSchedule.Add(date: DateTime.Today, slot: 2, name: "Nisse Hult", phone: "24682468");

            //Assert
            Assert.IsTrue(success);

        }

        [Test]
        public void GetAnAppointmentFromWeekSchedule()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3,
                AssignOnWeekends = true
            };

            var weekSchedule = factory.Create();

            var nisseHult = new Appointment
            {
                Date = DateTime.Today,
                Slot = 2,
                Name = "Nisse Hult",
                Phone = "24682468"
            };

            weekSchedule.Add(nisseHult);

            //Act
            var expected = weekSchedule.GetAppointment(date: DateTime.Today, slot: 2);
            
            //Assert
            Assert.AreEqual(expected, nisseHult);
        }

        [Test]
        public void AddRangeAppointments()
        {
            //Arrange
            var today = DateTime.Today;
            var factory = new WeekScheduleFactory
            {
                Today = today,
                NumberOfTimeSlotsForAppointments = 3,
                AssignOnWeekends = true,
                AssignOnHolidays = true
            };

            var weekSchedule = factory.Create();

            var success = weekSchedule.AddRange(AppointmentsRange(today));

            //Act
            var expected = weekSchedule.GetAppointments();

            //Assert
            Assert.AreEqual(success, 3);
            Assert.AreEqual(expected.Count, 3);


        }

        private IEnumerable<Appointment> AppointmentsRange(DateTime today)
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Date = today,
                    Slot = 2,
                    Name = "Nisse Hult",
                    Phone = "24682468"
                },
                new Appointment
                {
                    Date = today,
                    Slot = 1,
                    Name = "Ebba Hult",
                    Phone = "24682468"
                },
                new Appointment
                {
                    Date = today,
                    Slot = 0,
                    Name = "Arne Hult",
                    Phone = "24682468"
                }
            };
        }

        [Test]
        public void GetAppointmentsFromWeekSchedule()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3,
                AssignOnWeekends = true
            };

            var weekSchedule = factory.Create();

            var nisseHult = new Appointment
            {
                Date = DateTime.Today,
                Slot = 2,
                Name = "Nisse Hult",
                Phone = "24682468"
            };

            var ebbaHult = new Appointment
            {
                Date = DateTime.Today,
                Slot = 1,
                Name = "Ebba Hult",
                Phone = "24682468"
            };

            var arneHult = new Appointment
            {
                Date = DateTime.Today.AddDays(-1),
                Slot = 2,
                Name = "Arne Hult",
                Phone = "24682468"
            };

            weekSchedule.Add(nisseHult);
            weekSchedule.Add(ebbaHult);
            weekSchedule.Add(arneHult);

            //Act
            var expected = weekSchedule.GetAppointments();

            //Assert
            Assert.IsNotEmpty(expected);
        }

    }
}
