using System;
using System.Collections.Generic;

namespace AppointmentScheduling.Data.PersistenceModel
{
    public class Schedule 
    {
        public Guid Id { get; set; }
        public int ClinicId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}