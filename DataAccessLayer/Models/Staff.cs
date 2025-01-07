using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Department { get; set; }

    public string? Specialization { get; set; }

    public DateOnly HireDate { get; set; }

    public bool? IsActive { get; set; }
    
    public StaffRoleEnum Role { get; set; } //ag

    public virtual ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();

    public virtual ICollection<PatientInvoice> PatientInvoices { get; set; } = new List<PatientInvoice>();

    public virtual ICollection<PatientVital> PatientVitals { get; set; } = new List<PatientVital>();
}
