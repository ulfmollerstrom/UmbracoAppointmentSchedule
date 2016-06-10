using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace UmbracoAppointmentSchedule.Core.Test
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
                NumberOfTimeSlotsForAppointments = 3
            };

            var weekSchedule = factory.Create();

            //Act
            var success = weekSchedule.Add(new Appointment {Date = DateTime.Today, Slot = 2, Name = "Nisse Hult", Phone = "24682468"});

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
                NumberOfTimeSlotsForAppointments = 3
            };

            var weekSchedule = factory.Create();

            //Act
            var success = weekSchedule.Add(date: DateTime.Today, slot: 2, name: "Nisse Hult", phone: "24682468");

            //Assert
            Assert.IsTrue(success);

        }


        [Test]
        public void GetTheAppointmentsFromWeekSchedule()
        {
            //Arrange
            var factory = new WeekScheduleFactory
            {
                Today = DateTime.Today,
                NumberOfTimeSlotsForAppointments = 3
            };

            //Act
            var actual = factory.Create();

            //var expected = new


            //Act
            //var actual = new

            //Assert

        }

    }
}
