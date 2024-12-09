using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class EmergencyVisitStaff
{
    public StaffRoleEnum StaffRoleEnum { get; set; } // ag
    public DateTime? AssignedAt { get; set; }

    public int VisitId { get; set; }

    public int StaffId { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual EmergencyVisit Visit { get; set; } = null!;
}
