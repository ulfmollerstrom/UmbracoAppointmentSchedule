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
    public class SerializeScheduleRepositoryTests
    {
        [Test]
        public void SerializeToJsonTest()
        {
            //Arrange
            //var expected = new
            

            //Act
            var scheduleRepository = GetScheduleRepository();
            var jsonString = scheduleRepository.Serialize();


            //Assert
            Assert.IsNotEmpty(jsonString);

        }

        [Test]
        public void DeserializeToScheduleRepositoryTest()
        {
            //Arrange
            //var expected = new
            
            //Act
            var scheduleRepository = GetScheduleRepository();
            var jsonString = scheduleRepository.Serialize();

            var actual = new ScheduleRepository();
            actual.Deserialize(jsonString, DateTime.Today);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(jsonString, actual.Serialize());

        }


        private static ScheduleRepository GetScheduleRepository()
        {
            var scheduleRepository = new ScheduleRepository
            {
                CurrentDate = new DateTime(2016, 6, 10),
                AssignOnHolidays = true,
                AssignOnWeekends = true,
                NumberOfTimeSlotsForAppointments = 3,
                HolidayDates = new List<DateTime> { new DateTime(2016, 6, 6) },
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
