
using CIT_Web.Models.Dto.OrderType;

namespace CIT_Web.Services.IServices
{
    public interface IOrderTypeService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(OrderTypeCreateDTO dto);
        Task<T> UpdateAsync<T>(OrderTypeUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
