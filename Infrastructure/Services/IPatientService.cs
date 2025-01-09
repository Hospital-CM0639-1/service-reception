using System.Runtime.InteropServices.JavaScript;
using DataAccessLayer.Models;
using Infrastructure.InputModels;
using reception.OutputModels;
using reception.QueryParameters;

namespace Infrastructure.Services;

public interface IPatientService : IBaseService
{
    public Task<List<OutputPatient>> GetPatientsAsync(PatientGetFilterQueryParameters p); // pagination
    public Task<Patient?> GetPatientByIdAsync(int id);
    public Task<bool> DoesPatientExist(int id);
    public Task<int> GetTotalPatientsCountAsync();
    public Task<bool> DeletePatientByIdAsync(int id);
    public Task<bool>  AddPatientAsync(RegistrationPatient patient);
    Task<bool> UpdatePatientAsync(RegistrationPatient patient);
}