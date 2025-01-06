using System.Runtime.InteropServices.JavaScript;
using DataAccessLayer.Models;

namespace Infrastructure.Services;

public interface IPatientService : IBaseService
{
    public Task<List<Patient>> GetPatientsAsync(int number, int page); // pagination
    public Task<Patient?> GetPatientByIdAsync(int id);
    public Task<bool> DoesPatientExist(int id);
    public Task<bool> DeletePatientByIdAsync(int id);
}