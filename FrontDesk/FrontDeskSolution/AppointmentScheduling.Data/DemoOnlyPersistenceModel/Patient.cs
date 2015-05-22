using AppointmentScheduling.Core.Model.ValueObjects;
using FrontDesk.SharedKernel.Enums;

namespace AppointmentScheduling.Data.PersistenceModel
{
  public class Patient
  {
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public AnimalType AnimalType { get;  set; }
    public int? PreferredDoctorId { get; set; }
   
  }
}