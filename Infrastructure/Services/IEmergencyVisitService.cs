using Infrastructure.InputModels;

namespace Infrastructure.Services;

public interface IEmergencyVisitService: IBaseService
{
    public Task<bool> AddEmergencyVisitAsync(EmergencyVisit emergencyVisit);
}