using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? InsuranceProvider { get; set; }

    public string? InsurancePolicyNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<EmergencyVisit> EmergencyVisits { get; set; } = new List<EmergencyVisit>();
}
