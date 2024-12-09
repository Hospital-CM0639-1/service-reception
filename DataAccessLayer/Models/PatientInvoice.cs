using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class PatientInvoice
{
    public int InvoiceId { get; set; }

    public int VisitId { get; set; }

    public int CreatedByStaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? InvoiceTimestamp { get; set; }

    public string? PaymentStatus { get; set; }

    public decimal? PaymentReceivedAmount { get; set; }

    public DateTime? PaymentReceivedTimestamp { get; set; }

    public virtual Staff CreatedByStaff { get; set; } = null!;

    public virtual EmergencyVisit Visit { get; set; } = null!;
}
