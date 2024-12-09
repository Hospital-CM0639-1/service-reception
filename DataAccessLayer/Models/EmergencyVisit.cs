using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class EmergencyVisit
{
    public int VisitId { get; set; }

    public DateTime? AdmissionTimestamp { get; set; }

    public DateTime? DischargeTimestamp { get; set; }
    
    public PatientStatusEnum CurrentStatusEnum { get; set; } // ag

    public string? TriageNotes { get; set; }

    public string PriorityLevel { get; set; } = null!;

    public int PatientId { get; set; }

    public virtual ICollection<HospitalBed> HospitalBeds { get; set; } = new List<HospitalBed>();

    public virtual ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientInvoice> PatientInvoices { get; set; } = new List<PatientInvoice>();

    public virtual ICollection<PatientVital> PatientVitals { get; set; } = new List<PatientVital>();

    public virtual PriorityLevel PriorityLevelNavigation { get; set; } = null!;
}
