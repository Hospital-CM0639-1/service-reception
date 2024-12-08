using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class PatientVital
{
    public int VitalRecordId { get; set; }

    public int VisitId { get; set; }

    public int RecordedByStaffId { get; set; }

    public DateTime? RecordedAt { get; set; }

    public decimal? BodyTemperature { get; set; }

    public int? BloodPressureSystolic { get; set; }

    public int? BloodPressureDiastolic { get; set; }

    public int? HeartRate { get; set; }

    public int? RespiratoryRate { get; set; }

    public decimal? OxygenSaturation { get; set; }

    public string? AdditionalObservations { get; set; }

    public virtual Staff RecordedByStaff { get; set; } = null!;

    public virtual EmergencyVisit Visit { get; set; } = null!;
}
