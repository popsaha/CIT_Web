namespace CIT_Web.Services.IServices
{
    public interface IVehicleService
    {
        Task<T> GetAllVehicleAsync<T>();
    }
}
