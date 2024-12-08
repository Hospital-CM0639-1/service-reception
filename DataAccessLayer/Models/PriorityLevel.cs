using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class PriorityLevel
{
    public string PriorityCode { get; set; } = null!;

    public string PriorityName { get; set; } = null!;

    public string ColorCode { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<EmergencyVisit> EmergencyVisits { get; set; } = new List<EmergencyVisit>();
}
