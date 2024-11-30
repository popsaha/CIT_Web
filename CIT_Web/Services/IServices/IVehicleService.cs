
using CIT_Web.Models.Dto.Vehicle;

namespace CIT_Web.Services.IServices
{
    public interface IVehicleService
    {
        Task<T> GetAllVehicleAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VehicleCreateDTO dto);
        Task<T> UpdateAsync<T>(VehicleUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
