using DataAccessLayer.Models;

namespace reception.OutputModels;

public class OutputPatient : Patient
{
    public OutputPatient(Patient patient, string? priorityLevel, PatientStatusEnum? currentStatus, string? triageNotes)
    {
        PatientId = patient.PatientId;
        FirstName = patient.FirstName;
        LastName = patient.LastName;
        DateOfBirth = patient.DateOfBirth;
        Gender = patient.Gender;
        Address = patient.Address;
        Email = patient.Email;
        EmergencyContactName = patient.EmergencyContactName;
        EmergencyContactNumber = patient.EmergencyContactNumber;
        InsuranceProvider = patient.InsuranceProvider;
        InsurancePolicyNumber = patient.InsurancePolicyNumber;
        ContactNumber = patient.ContactNumber;
        CreatedAt = patient.CreatedAt;
        // Add the extra properties
        PriorityLevel = priorityLevel?.ToUpper();
        CurrentStatus = currentStatus.ToString();
        TriageNotes = triageNotes;
    }

    public string? PriorityLevel { get; set; }
    public string? CurrentStatus { get; set; }
    public string? TriageNotes { get; set; } 
        
}