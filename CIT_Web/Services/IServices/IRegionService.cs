
using CIT_Web.Models.Dto.Region;

namespace CIT_Web.Services.IServices
{
    public interface IRegionService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(RegionCreateDTO dto);
        Task<T> UpdateAsync<T>(RegionUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
