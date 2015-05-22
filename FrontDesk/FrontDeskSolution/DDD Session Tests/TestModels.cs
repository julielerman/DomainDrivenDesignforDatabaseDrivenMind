using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AppointmentScheduling.Data.Events;
using DDD_Session_Tests;
using FrontDesk.SharedKernel;


namespace DDD_Session_Tests
{
  public class Pet
  {
    public static Pet Create(string name) {
      return new Pet(name);
    }

    private Pet(string name) {
      Name = name;
      Meds = new List<Medication>();
      Photo=new PetPhotoValueObject(null,"No Image");
    }

    private Pet() {

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Medication> Meds { get; set; }
    //[Required]
    //public PetPhoto Photo { get; set; }

    public PetPhotoValueObject Photo { get; set; }
  }

  public class Medication
  {
    public int Id { get; set; }
    public string Name { get; set; }
    //no pet fk id, to prove a point about EF
  }

  //public class PetPhoto
  //{
  //  [ForeignKey("PhotoOf")]
  //  public int Id { get; set; }
  //  public byte[] Photo { get; set; }
  //  public string Caption { get; set; }
  //  public Pet PhotoOf { get; set; }
  //}

  public class PetPhotoValueObject : ValueObject<PetPhotoValueObject>
  {
    public byte[] Photo { get; private set; }
    public string Caption { get; private set; }

    public PetPhotoValueObject(byte[] photo, string caption) {
      Photo = photo;
      Caption = caption;
    }
    public PetPhotoValueObject NewCaption(string newCaption) {
      return new PetPhotoValueObject(Photo, newCaption);
    }

    public PetPhotoValueObject() {}
  }

  public class PetMedsContext : DbContext
  {
    public DbSet<Pet> Pets { get; set; }
  }
 
}

