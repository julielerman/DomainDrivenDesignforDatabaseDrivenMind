using DomainEventsConsole.Events;
using DomainEventsConsole.Model;
using NUnit.Framework;
using System;
using System.Linq;

namespace DomainEventsConsole.UnitTests
{
    [TestFixture]
    public class AppointmentCreateShould
    {
        [Test]
        public void RaiseAppointmentCreatedEvent()
        {
            string testCustomerEmail = "foo@bar.com";
            string notificationSentToEmail = "";
            DomainEvents.Register<AppointmentCreated>(ac => notificationSentToEmail = ac.Appointment.EmailAddress);

            var appointment = Appointment.Create(testCustomerEmail);

            Assert.AreEqual(testCustomerEmail, notificationSentToEmail);
        }
    }
}
