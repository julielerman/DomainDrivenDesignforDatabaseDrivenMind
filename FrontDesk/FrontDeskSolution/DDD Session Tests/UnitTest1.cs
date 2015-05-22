
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using AppointmentScheduling.Data;
//using Assert = NUnit.Framework.Assert;
using System.Data.Entity;
using System.Diagnostics;
using AppointmentScheduling.Core.Model.ScheduleAggregate;


namespace DDD_Session_Tests
{
  [TestFixture]
  public class Tests
  {
    [Test]
    public void CanReturnMaterializedAppointments() {
      using (var db = new SchedulingContext()) {
        var appointment = db.Appointments.FirstOrDefault();
        Assert.IsNotNullOrEmpty(appointment.Title);
        var apptProps = TypeDescriptor.GetProperties(appointment);
        foreach (PropertyDescriptor pd in apptProps) {
          Debug.WriteLine("{0}:{1}", pd.DisplayName, pd.GetValue(appointment));
        }
      }
    }
    [Test]
    public void CanReturnScheduleWithAppointments() {
      using (var db = new SchedulingContext()) {
        var schedule = db.Schedules.Include(c => c.Appointments).FirstOrDefault();
        Assert.IsNotEmpty(schedule.Appointments);
      }
    }

    [Test]
    public void CanReturnScheduleWithAppointmentsViaPersistenceModel() {
      using (var db = new SchedulingPersistenceContext()) {
        var persSchedule = db.Schedules.Include(c => c.Appointments).FirstOrDefault();
        var apptList = new List<AppointmentScheduling.Core.Model.ScheduleAggregate.Appointment>();
        persSchedule.Appointments.ToList()
          .ForEach(
            a =>
              apptList.Add(Appointment.Create(a.ScheduleId, a.ClientId, a.PatientId, a.RoomId, a.TimeRange.Start,
                a.TimeRange.End,
                a.AppointmentTypeId, a.DoctorId, a.Title)));
        var dddSchedule = new Schedule(persSchedule.Id, null, persSchedule.ClinicId, apptList);
        Assert.IsNotEmpty(dddSchedule.Appointments);
        Debug.WriteLine("Schedule" + dddSchedule.ClinicId);
        foreach (var appt in dddSchedule.Appointments) {
          Debug.WriteLine(appt.TimeRange.ToString());
        }
      }
    }
 

    [Test]
    public void CanInitializeDatabase() {
      using (var db = new PetMedsContext()) {
        Database.SetInitializer(new DropCreateDatabaseAlways<PetMedsContext>());
        db.Database.Initialize(force: true);
      }
    }

    [Test]
    public void CanStoreEntitiesWithValueObjectProperties() {
      int petId;

      var petSampson = Pet.Create("Sampson");
    
      petSampson.Photo = new PetPhotoValueObject
        (System.IO.File.ReadAllBytes("..\\..\\sampson.gif"),
          "Sampson's Bed?");
      using (var db = new PetMedsContext()) {
        db.Pets.Add(petSampson);
        db.SaveChanges();
        petId = petSampson.Id;

      }
      using (var db = new PetMedsContext()) {
        var petfromdb = db.Pets.Find(petId);
        Assert.AreEqual(petSampson.Photo.ToString(), petfromdb.Photo.ToString());
      }
    }

    [Test]
    public void CanInstantiateMultiplePetsWithSameValueObjectDefinitionInConstructor() {
      var petSampson = Pet.Create("Sampson");
      var petTrudie = Pet.Create("Trudie");
      Assert.IsTrue(petSampson.Photo.Equals(petTrudie.Photo));
    }


    [Test]
    public void CanPersistForeignKeysWithoutNavigationProperty() {
      var pet = Pet.Create("Sampson");
      pet.Meds.Add(new Medication { Name = "Happy Pills" });
      Database.SetInitializer(new DropCreateDatabaseAlways<PetMedsContext>());
      using (var db = new PetMedsContext()) {
        db.Database.Initialize(force: true);
        db.Database.Log = Console.WriteLine;
        db.Pets.Add(pet);
        db.SaveChanges();
      }
      using (var db = new PetMedsContext()) {
        var dbPet = db.Pets.Include(p => p.Meds).FirstOrDefault();
        Assert.AreNotEqual(0, dbPet.Meds.Count());
      }
    }
  }
}
