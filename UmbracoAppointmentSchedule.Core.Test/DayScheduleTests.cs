using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UmbracoAppointmentSchedule.Core.Test
{
    [TestFixture]
    public class DayScheduleTests
    {
        [Test]
        public void CanCreateAnAppointmentObject()
        {
            //Arrange
            var appointment = new Appointment
            {
                Name = "Olle",
                Phone = "24682468",
            };

            //Assert
            Assert.IsNotNull(appointment);
        }

        [Test]
        public void CanCreateDaySchedule()
        {
            //Arrange
            var expected = new DaySchedule {NumberOfTimeSlotsForAppointments = 3};

            //Assert
            Assert.IsNotNull(expected);
        }

        [Test]
        public void CanAddAppointmentToSchedule()
        {
            //Arrange
            var daySchedule = new DaySchedule { NumberOfTimeSlotsForAppointments = 1 };
            var appointment = new Appointment {Name = "Olle", Phone = "24682468"};
            var expected = "Olle";

            //Act
            var sucsess = daySchedule.AddAppointment(0, appointment);
            var actual = daySchedule.Appointments[0].Name;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CantAddAppointmentToReservedSlot()
        {
            //Arrange
            var daySchedule = new DaySchedule { NumberOfTimeSlotsForAppointments = 1 };
            var appointment = new Appointment { Name = "Olle", Phone = "24682468" };
            daySchedule.AddAppointment(0, appointment);

            //Act
            var success = daySchedule.AddAppointment(0, new Appointment {Name = "Berit", Phone = "88889999"});

            //Assert
            Assert.IsFalse(success);
        }

        [Test]
        public void CheckForFreeTimeSlots()
        {
            //Arrange
            var daySchedule = new DaySchedule { NumberOfTimeSlotsForAppointments = 2 };
            var expected = new int[2];

            //Act
            var actual = daySchedule.FreeTimeSlots.ToArray();

            //Assert
            Assert.AreEqual(expected.Length, actual.Length);
        }

        [Test]
        public void CheckForFreeTimeSlotsWhenNotAsignable()
        {
            //Arrange
            var daySchedule = new DaySchedule { IsAssignable = false };
            var expected = new int[0];

            //Act
            var actual = daySchedule.FreeTimeSlots.ToArray();

            //Assert
            Assert.AreEqual(expected.Length, actual.Length);
        }

        [Test]
        public void AddAppointmentTothirdTimeSlot()
        {
            //Arrange
            var daySchedule = new DaySchedule { NumberOfTimeSlotsForAppointments = 3 };
            var appointment = new Appointment { Name = "Olle", Phone = "24682468" };
            var expected = "Olle";

            //Act
            var sucsess = daySchedule.AddAppointment(2, appointment);
            var actual = daySchedule.Appointments[2].Name;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MiscJunk()
        {
            //Arrange
            var daySchedule = new DaySchedule();
            daySchedule.NumberOfTimeSlotsForAppointments = 1;
            var appointment = new Appointment { Name = "Olle", Phone = "24682468" };
            daySchedule.AddAppointment(0, appointment);

            //Act
            var success = daySchedule.AddAppointment(0, new Appointment { Name = "Berit", Phone = "88889999" });

            //Assert
            Assert.IsFalse(success);
        }

    }
}
