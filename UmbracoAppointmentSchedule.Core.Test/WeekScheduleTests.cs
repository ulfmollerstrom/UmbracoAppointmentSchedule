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
        public void CanCreateAWeeklySchedule()
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

    }
}
