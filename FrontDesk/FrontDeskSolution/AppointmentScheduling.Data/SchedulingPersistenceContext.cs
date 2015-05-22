using System.Data.Entity;
using AppointmentScheduling.Data.PersistenceModel;

namespace AppointmentScheduling.Data
{
    public class SchedulingPersistenceContext : DbContext
    {
      public SchedulingPersistenceContext()
        : base("name=VetOfficeContext")
        {
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}