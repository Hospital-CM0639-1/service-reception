using DataAccessLayer.Data;

namespace Infrastructure.Services;

public interface IBaseService
{
    public Task<bool> SaveAsync(bool save);
}