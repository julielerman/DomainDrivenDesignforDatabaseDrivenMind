using DomainEventsConsole.Events;
using DomainEventsConsole.Interfaces;
using System;
using System.Linq;

namespace DomainEventsConsole.Handlers
{
    public class NotifyUserAppointmentCreated : IHandle<AppointmentCreated>
    {
        public void Handle(AppointmentCreated args)
        {
            ConsoleWriter.FromEmailEventHandlers("[EMAIL] Notification email sent to {0}", args.Appointment.EmailAddress);
        }
    }
}
