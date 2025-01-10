using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.OrderRoute;

namespace CIT_Web.Services.IServices
{
    public interface IOrderRouteService
    {
        Task<T> GetAllOrderRouteList<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(OrderRouteCreateDTO dto);
        Task<T> UpdateAsync<T>(OrderRouteUpdateDTOs dto);
        Task<T> DeleteAsync<T>(int id, int deletedBy);
    }
}
