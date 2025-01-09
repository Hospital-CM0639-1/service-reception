using DataAccessLayer.Models;

namespace Infrastructure.InputModels;

public class EmergencyVisit
{
    public string Status { get; set; }
    public string PriorityLevel { get; set; }
    public string TriageNotes { get; set; }
    public int PatientId { get; set; }
}