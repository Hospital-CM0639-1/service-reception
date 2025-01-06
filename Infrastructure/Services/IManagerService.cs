namespace Infrastructure.Services;

public interface IManagerService : IBaseService
{
    public Task<bool> HasFreeBedAsync();
}