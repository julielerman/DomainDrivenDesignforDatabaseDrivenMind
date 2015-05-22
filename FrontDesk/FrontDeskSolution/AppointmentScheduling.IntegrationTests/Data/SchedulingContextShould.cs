using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using NUnit.Framework;
using AppointmentScheduling.Data;

namespace AppointmentScheduling.IntegrationTests.Data
{
    [TestFixture]
    public class SchedulingContextShould
    {
        public SchedulingContextShould()
        {
            Database.SetInitializer<SchedulingContext>(null);
        }

        [Test]
        public void GetClientReferenceList()
        {
            var db = new SchedulingContext();
            Assert.IsNotEmpty(db.Clients.ToList());
        }

        [Test]
        public void ReturnSchedulingClientType()
        {
            var db = new SchedulingContext();
            Assert.IsInstanceOf<AppointmentScheduling.Core.Model.ScheduleAggregate.Client>(db.Clients.FirstOrDefault());
        }

        [Test]

        public void ReturnClientsWithPatients()
        {
            using (var db = new SchedulingContext())
            {
                Assert.IsNotEmpty(db.Clients.Include(c => c.Patients).ToList());
            }
        }

       [Test]
      public void CanReturnMaterializedAppointments() {
          using (var db = new SchedulingContext()) {
            var appointment = db.Appointments.FirstOrDefault();
            Assert.IsNotNullOrEmpty(appointment.Title);
            var apptProps = System.ComponentModel.TypeDescriptor.GetProperties(appointment);
            foreach (PropertyDescriptor pd in apptProps) {
              Debug.WriteLine("{0}:{1}",pd.DisplayName, pd.GetValue(appointment));
            }
          }
        }
      [Test]
      public void CanReturnScheduleWithAppointments()
      {
        using (var db = new SchedulingContext())
        {
          var schedule=db.Schedules.Include(c => c.Appointments).FirstOrDefault();
          Assert.IsNotEmpty(schedule.Appointments);
        }
      }
      [Test]
      public void CanReturnScheduleWithAppointmentsViaPersistenceModel() {
        using (var db = new SchedulingPersistenceContext()) {
          var persSchedule = db.Schedules.Include(c => c.Appointments).FirstOrDefault();
          var apptList = new List<Appointment>();
          persSchedule.Appointments.ToList()
            .ForEach(
              a =>
                apptList.Add(Appointment.Create(a.ScheduleId, a.ClientId, a.PatientId, a.RoomId, a.TimeRange.Start,
                  a.TimeRange.End,
                  a.AppointmentTypeId, a.DoctorId, a.Title)));


          var dddSchedule = new Schedule(persSchedule.Id, null, persSchedule.ClinicId, apptList);
          Assert.IsNotEmpty(dddSchedule.Appointments);
        }
      }
    }
}
