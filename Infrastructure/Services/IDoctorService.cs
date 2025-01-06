using DataAccessLayer.Models;

namespace Infrastructure.Services;

public interface IDoctorService: IBaseService
{
    public Task<List<string?> > GetDoctorSpecializationsAsync();
    public Task<List<Staff>> GetDoctorsAsync(string? type = null, bool available = false);
    public Task<bool> DeleteDoctorByIdAsync(int id);
}