using CIT_Web.Models.Dto.Police;
using CIT_Web.Models.Dto.Vehicle;

namespace CIT_Web.Services.IServices
{
    public interface IPoliceService
    {
        Task<T> GetAllPoliceAsync<T>();
        Task<T> CreateAsync<T>(PoliceCreateDTO dto);
        Task<T> UpdateAsync<T>(PoliceUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
