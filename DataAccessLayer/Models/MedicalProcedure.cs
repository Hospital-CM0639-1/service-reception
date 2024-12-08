using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class MedicalProcedure
{
    public int ProcedureId { get; set; }

    public int VisitId { get; set; }

    public int PerformedByStaffId { get; set; }

    public string ProcedureName { get; set; } = null!;

    public DateTime? ProcedureTimestamp { get; set; }

    public string? Description { get; set; }

    public decimal? ProcedureCost { get; set; }

    public virtual Staff PerformedByStaff { get; set; } = null!;

    public virtual EmergencyVisit Visit { get; set; } = null!;
}
