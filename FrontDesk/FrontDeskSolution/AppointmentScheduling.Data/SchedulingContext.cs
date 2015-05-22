using System.Data.Entity;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AppointmentScheduling.Data
{
  public class SchedulingContext : DbContext
  {
    public SchedulingContext()
    : base("name=VetOfficeContext") {
    }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentType> AppointmentTypes { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.Entity<Client>().HasKey(c => c.Id);

      modelBuilder.Entity<Appointment>().Ignore(a => a.State);
      modelBuilder.Entity<Appointment>().Ignore(a => a.IsPotentiallyConflicting);

      modelBuilder.Entity<Schedule>().Ignore(s => s.DateRange);
      modelBuilder.Conventions.Add(new StringProperties150());
      modelBuilder.Conventions.Add(new StringType());
      base.OnModelCreating(modelBuilder);
    }

    public class StringProperties150 : Convention
    {
      public StringProperties150() {
        Properties<String>()
         .Configure(s => s.HasMaxLength(150));
      }
    }
    public class StringType : Convention
    {
      public StringType() {
        Properties<String>()
         .Configure(p => p.HasColumnType("nvarchar"));
      }

    }

  }
}
