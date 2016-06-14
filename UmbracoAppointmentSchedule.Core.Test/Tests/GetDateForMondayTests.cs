using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UmbracoAppointmentSchedule.Core.Test.Tests
{
    [TestFixture]
    public class GetDateForMondayTests
    {

        [Test]
        public void TestWithMonday()
        {
            //Arrange
            var expected = new DateTime(2016,6,13);
            

            //Act
            var actual = GetDateForMonday(expected);

            //Assert
            Assert.AreEqual(expected,actual);

        }

        [Test]
        public void TestWithTuesday()
        {
            //Arrange
            var expected = new DateTime(2016, 6, 13);


            //Act
            var actual = GetDateForMonday(DateTime.Today);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        private static DateTime GetDateForMonday(DateTime today)
        {
            if (today.DayOfWeek == DayOfWeek.Monday)
                return today;

            var dates = new List<DateTime>
            {
                today.AddDays(-7),
                today.AddDays(-6),
                today.AddDays(-5),
                today.AddDays(-4),
                today.AddDays(-3),
                today.AddDays(-2),
                today.AddDays(-1)
            };

            var retval = Enumerable.Range(-7, 7)
                .Where(i => today.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                .Select(i => today.AddDays(i)).ToList();

            return retval.FirstOrDefault();
        }
    }
}
