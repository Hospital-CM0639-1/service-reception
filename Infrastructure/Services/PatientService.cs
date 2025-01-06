using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PatientService(HospitalContext dBContext) : BaseService(dBContext), IPatientService
{
    private readonly HospitalContext _dBContext = dBContext;

    public Task<List<Patient>> GetPatientsAsync(int number, int page)
    {
        return _dBContext.Patients.Skip(number * page).Take(number).ToListAsync();
    }

    public Task<Patient?> GetPatientByIdAsync(int id)
    {
        return _dBContext.Patients.FirstOrDefaultAsync(patient => patient.PatientId == id);
    }

    public Task<bool> DoesPatientExist(int id)
    {
        Console.WriteLine("-- does patient  exists endpoint");
        return _dBContext.Patients.AnyAsync(patient => patient.PatientId == id);
    }

    public async Task<bool> DeletePatientByIdAsync(int id)
    {
        var patient = await _dBContext.Patients.FirstOrDefaultAsync(patient => patient.PatientId == id);
        if (patient == null) return false;
        _dBContext.Patients.Remove(patient);
        return await SaveAsync(true);
    }
}