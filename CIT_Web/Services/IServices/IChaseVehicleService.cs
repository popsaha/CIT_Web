using CIT_Web.Models.Dto.ChaseVehicle;


namespace CIT_Web.Services.IServices
{
    public interface IChaseVehicleService
    {
        Task<T> GetAllVehicleAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ChaseVehicleCreateDTO dto);
        Task<T> UpdateAsync<T>(ChaseVehicleUpdateDTO dto);
        Task<T> DeleteAsync<T>(int vehicleId, int userId);
    }
}
