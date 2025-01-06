using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ManagerService(HospitalContext dbContext) : BaseService(dbContext), IManagerService
{
    private readonly HospitalContext _dbContext = dbContext;

    public async Task<bool> HasFreeBedAsync()
    {
        return await _dbContext.HospitalBeds.AnyAsync(b => b.BedStatusEnum == HospitalBedStatusEnum.Available);
    }
}