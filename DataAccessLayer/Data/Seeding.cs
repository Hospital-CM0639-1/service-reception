using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public static class Seeding
{
    static Seeding(){}

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var patient = PreparePatient();
        var staff = PrepareStaff();
        var hospitalBed = PrepareHospitalBeds();

        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Staff>().HasData(staff);
        modelBuilder.Entity<HospitalBed>().HasData(hospitalBed);
    }

    public static List<Patient> PreparePatient()
    {
        return new List<Patient>()
        {
            new Patient
            {
                FirstName = "Pepe", LastName = "Pizza", PatientId = 1, InsurancePolicyNumber = "12345",
                Address = "1234 Main St"
            },
            new Patient
            {
                FirstName = "Tom", LastName = "Fredy", PatientId = 2, InsurancePolicyNumber = "12346",
                Address = "1000 Main St"
            },
            new Patient
            {
                FirstName = "Sofia", LastName = "Falcony", PatientId = 3, InsurancePolicyNumber = "12347",
                Address = "5000 Main St"
            },
            new Patient
            {
                FirstName = "Labra", LastName = "Dor", PatientId = 4, InsurancePolicyNumber = "12345",
                Address = "3333 Haf Baf"
            },
        };
    }

    public static List<Staff> PrepareStaff()
    {
        return new List<Staff>()
        {
            new Staff{FirstName = "Doctor",LastName = "1",StaffId = 1,Email = "contact@email.com"}
        };
    }

    public static List<HospitalBed> PrepareHospitalBeds()
    {
        return new List<HospitalBed>()
        {
            new HospitalBed { BedId = 1, BedNumber = "1", BedStatusEnum = HospitalBedStatusEnum.Available, WardSection = "PRIORITY"}
        };
    }
    
}