namespace Infrastructure.InputModels;

public class EmergencyVisitInput
{
    public string? Status { get; set; }
    public string? PriorityLevel { get; set; }
    public string? TriageNotes { get; set; }
    public int PatientId { get; set; }
    public int? DoctorId { get; set; }
}