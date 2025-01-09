using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Infrastructure.InputModels;
using Microsoft.EntityFrameworkCore;
using reception.OutputModels;
using reception.QueryParameters;

namespace Infrastructure.Services;

public class PatientService(HospitalContext dBContext) : BaseService(dBContext), IPatientService
{
    private readonly HospitalContext _dBContext = dBContext;

    public async Task<List<OutputPatient>> GetPatientsAsync(PatientGetFilterQueryParameters filter)
    {
        PatientStatusEnum? statusEnum = null;

        if (!string.IsNullOrEmpty(filter.Status))
        {
            statusEnum = Enum.Parse<PatientStatusEnum>(filter.Status, true); // Case-insensitive parsing
        }

        var patients = await _dBContext.Patients
            .Where(p => filter.Id == null || filter.Id == p.PatientId)
            .Where(p => filter.Surname == null || p.LastName.ToLower().Contains(filter.Surname.ToLower()))

            .Select(patient => new
            {
                Patient = patient,
                LatestEmergencyVisit = _dBContext.EmergencyVisits
                    .Where(ev => ev.PatientId == patient.PatientId)
                    .OrderByDescending(ev => ev.DischargeTimestamp)
                    .ThenByDescending(ev => ev.AdmissionTimestamp)
                    .FirstOrDefault()
            })
            .Where(p => filter.Status == null || (p.LatestEmergencyVisit != null &&
                                                  p.LatestEmergencyVisit.CurrentStatus.ToString().Equals(filter.Status, StringComparison.OrdinalIgnoreCase)))
            .Where(p => filter.Priority == null || (p.LatestEmergencyVisit != null &&
                                                    p.LatestEmergencyVisit.PriorityLevel.ToUpper() == filter.Priority.ToUpper()))
            .OrderBy(p=>p.Patient.PatientId)
            .Skip(filter.Number * (filter.Page - 1))
            .Take(filter.Number)
            .ToListAsync();

        return patients.Select(p =>
            new OutputPatient(
                p.Patient,
                p.LatestEmergencyVisit?.PriorityLevel,
                p.LatestEmergencyVisit?.CurrentStatus,
                p.LatestEmergencyVisit?.TriageNotes
            )).ToList();
    }

    public Task<Patient?> GetPatientByIdAsync(int id)
    {
        return _dBContext.Patients.FirstOrDefaultAsync(patient => patient.PatientId == id);
    }

    public Task<bool> DoesPatientExist(int id)
    {
        return _dBContext.Patients.AnyAsync(patient => patient.PatientId == id);
    }

    public Task<int> GetTotalPatientsCountAsync()
    {
        return _dBContext.Patients.CountAsync();
    }

    public async Task<bool> DeletePatientByIdAsync(int id)
    {
        var patient = await _dBContext.Patients.FirstOrDefaultAsync(patient => patient.PatientId == id);
        if (patient == null) return false;
        _dBContext.Patients.Remove(patient);
        return await SaveAsync(true);
    }

    public async Task<bool> AddPatientAsync(RegistrationPatient patient)
    {
        _dBContext.Patients.Add(new Patient
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Address = patient.Address,
            Email = patient.Email,
            Gender = patient.Gender,
            ContactNumber = patient.ContactNumber,
            EmergencyContactName = patient.EmergencyContactName,
            EmergencyContactNumber = patient.EmergencyContactNumber,
            InsuranceProvider = patient.InsuranceProvider,
            InsurancePolicyNumber = patient.InsurancePolicyNumber
        });
        return await SaveAsync(true);
    }

    public async Task<bool> UpdatePatientAsync(RegistrationPatient patient)
    {
        var existingPatient = await _dBContext.Patients.FirstOrDefaultAsync(p => p.PatientId == patient.PatientId);
        if (existingPatient == null) return false;

        existingPatient.FirstName = patient.FirstName;
        existingPatient.LastName = patient.LastName;
        existingPatient.DateOfBirth = patient.DateOfBirth;
        existingPatient.Address = patient.Address;
        existingPatient.Email = patient.Email;
        existingPatient.Gender = patient.Gender;
        existingPatient.ContactNumber = patient.ContactNumber;
        existingPatient.EmergencyContactName = patient.EmergencyContactName;
        existingPatient.EmergencyContactNumber = patient.EmergencyContactNumber;
        existingPatient.InsuranceProvider = patient.InsuranceProvider;
        existingPatient.InsurancePolicyNumber = patient.InsurancePolicyNumber;

        _dBContext.Patients.Update(existingPatient);

        return await SaveAsync(true);
    }
    
}