using DataAccessLayer.Data;

namespace Infrastructure.Services;

public class BaseService(HospitalContext dBContext) : IBaseService
{
    public async Task<bool> SaveAsync(bool save)
    {
        if (!save) return true;
        try
        {
            await dBContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
