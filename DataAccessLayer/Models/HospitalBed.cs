using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class HospitalBed
{
    public int BedId { get; set; }

    public int? CurrentVisitId { get; set; }

    public string WardSection { get; set; } = null!;

    public string BedNumber { get; set; } = null!;
    
    public HospitalBedStatus BedStatus { get; set; } // ag

    public DateTime? LastCleanedTimestamp { get; set; }

    public virtual EmergencyVisit? CurrentVisit { get; set; }
}
