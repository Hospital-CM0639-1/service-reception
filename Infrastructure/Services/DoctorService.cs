using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DoctorService(HospitalContext dbContext): BaseService(dbContext), IDoctorService
{
    private readonly HospitalContext _dbContext = dbContext;

    public async Task<List<string?>> GetDoctorSpecializationsAsync()
    {
        var xx = await _dbContext.Staff
            .Where(staff => staff.RoleEnum == StaffRoleEnum.Doctor)
            .Select(staff => staff.Specialization)
            .Distinct()
            .ToListAsync();
        return xx;
    }

    public Task<List<Staff>> GetDoctorsAsync(string? type = null, bool available = false)
    {
        var select = _dbContext.Staff.Where(staff => staff.RoleEnum == StaffRoleEnum.Doctor);
        if (type != null)
        {
            select = select.Where(staff => staff.Specialization == type);
        }
        if (available)
        {
            select = select.Where(staff => staff.IsActive == true);
        }
        return select.ToListAsync();
    }

    public Task<bool> DeleteDoctorByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}